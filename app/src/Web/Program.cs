using System;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyWebAPITemplate.Source.Infrastructure.Database;
using MyWebAPITemplate.Source.Web.Extensions;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

string[] _requiredEnvironmentVariables = { "ASPNETCORE_ENVIRONMENT" };
var startupLogger = CreateInitialLogger();

try
{
    CheckRequiredEnvVariables(_requiredEnvironmentVariables);
    var env = GetCurrentEnvironment(_requiredEnvironmentVariables);

    startupLogger.Information("Application starting");
    var builder = WebApplication.CreateBuilder(args);
    SetConfiguration(builder.Configuration, env);
    SetWebHost(builder.WebHost, env);
    SetServices(builder.Services, builder.Configuration, env);

    var app = builder.Build();
    SetApplications(app, builder.Environment);

    await SeedDatabase(app.Services, env);

    startupLogger.Information("Application starting to run");
    app.Run();
}
catch (Exception ex)
{
    // https://github.com/dotnet/runtime/issues/60600
    string type = ex.GetType().Name;
    if (type.Equals("StopTheHostException", StringComparison.Ordinal))
        throw;
    
    startupLogger.Fatal(ex, "Application did not start successfully");
}
// This is commented out because the EF migration stops working with it
//finally
//{
//    startupLogger.Information("Application is closing");
//    Environment.Exit(1);
//}

static void SetConfiguration(IConfigurationBuilder configBuilder, RunningEnvironment env)
    => configBuilder
        //.SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", false, false)
        .AddJsonFile("appsettings.Logs.json", false, false)
        .AddJsonFile($"appsettings.{env.Name}.json", false, false)
        .AddEnvironmentVariables();

static void SetWebHost(ConfigureWebHostBuilder configWebHostBuilder, RunningEnvironment env)
    => configWebHostBuilder
        .CaptureStartupErrors(true)
        .UseConfiguredSerilog(env);

static void SetServices(IServiceCollection services, IConfiguration configuration, RunningEnvironment env)
    => services
        .ConfigureDatabase(configuration)
        .ConfigureDevelopmentSettings()
        .ConfigureCors()
        .ConfigureSwagger()
        .ConfigureHealthChecks(configuration, env)
        .ConfigureSettings(configuration)
        .AddApplicationServices()
        .AddApplicationMappers()
        .AddApplicationRepositories()
        .AddModelValidators()
        .AddControllers()
        .AddFluentValidation(); // this must be called directly after AddControllers

static void SetApplications(IApplicationBuilder app, IWebHostEnvironment env)
    => app
        .ConfigureDevelopmentSettings(env)
        .ConfigureSwagger()
        .ConfigureLogger()
        .ConfigureRouting(); // This usually must be called the last

void CheckRequiredEnvVariables(string[] requiredEnvVars)
{
    startupLogger.Information("Checking required environment variables starting");
    var allEnvVars = Environment.GetEnvironmentVariables();
    foreach (var reqEnvVar in requiredEnvVars)
    {
        if (!allEnvVars.Contains(reqEnvVar))
        {
            startupLogger.Error("Required environment variable is missing: {envVar}", reqEnvVar);
            throw new ArgumentNullException(reqEnvVar);
        }
        startupLogger.Information("Required environment variable found: {envVar}", reqEnvVar);
    }
}

RunningEnvironment GetCurrentEnvironment(string[] envVars)
{
    startupLogger.Information("Checking running environment starting");

    var envs = Environment.GetEnvironmentVariables();

    var env = GetRunningEnvironment(envVars);
    if (env is null || !RunningEnvironment.Exists(env))
    {
        startupLogger.Fatal($"Application environment '{env}' is not supported");
        throw new ArgumentNullException(env);
    }
    startupLogger.Information("Checking running environment successful: {env}", env);
    return RunningEnvironment.Get(env)!;
}

async Task SeedDatabase(IServiceProvider serviceProvider, RunningEnvironment env)
{
    startupLogger.Information("Database seeding starting");
    using var scope = serviceProvider.CreateScope();
    var scopedProvider = scope.ServiceProvider;
    try
    {
        if (env.IsDevelopment())
        {
            var dbContext = scopedProvider.GetRequiredService<ApplicationDbContext>();
            await ApplicationDbContextSeed.SeedDevelopAsync(dbContext);
            startupLogger.Information("Database seeding successful");
        }
    }
    catch (Exception ex)
    {
        startupLogger.Error(ex, "Database seeding errored");
        throw;
    }
}

static string? GetRunningEnvironment(string[] envVars)
    => Environment.GetEnvironmentVariable(envVars[0]);

static ILogger CreateInitialLogger()
    => new LoggerConfiguration()
        .MinimumLevel.Debug()
        .WriteTo.Console(
            outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}",
            theme: AnsiConsoleTheme.Code
        )
        .Enrich.FromLogContext()
        .Enrich.WithProperty("Initial Logger", true)
        .Enrich.WithProperty("ApplicationName", typeof(Program).Assembly.GetName().Name)
        .Enrich.WithProperty("Environment", "Development")
        .CreateLogger();


public partial class Program { } // This is for the tests