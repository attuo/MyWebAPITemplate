using System;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyWebAPITemplate.Source.Extensions;
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
    SetServices(builder.Services, builder.Configuration);

    var app = builder.Build();
    SetApplications(app, builder.Environment);

    await SeedDatabase(app.Services, env);

    startupLogger.Information("Application starting to run");
    app.Run();
}
catch (Exception ex)
{
    startupLogger.Error(ex, "Application did not start successfully");
}
finally
{
    startupLogger.Information("Application is closing");
    Environment.Exit(1);
}

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

static void SetServices(IServiceCollection services, IConfiguration configuration)
    => services
        .ConfigureDatabase(configuration)
        .ConfigureDevelopmentSettings()
        .ConfigureCors()
        .ConfigureSwagger()
        .ConfigureHealthChecks(configuration)
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

void CheckRequiredEnvVariables(string[] envVars)
{
    startupLogger.Information("Checking required environment variables starting");
    foreach (var envVar in envVars)
    {
        if (string.IsNullOrWhiteSpace(envVar))
        {
            startupLogger.Error("Required environment variable is missing: {envVar}", envVar);
            Environment.Exit(1);
        }
        startupLogger.Information("Required environment variable found: {envVar}", envVar);
    }
}

RunningEnvironment GetCurrentEnvironment(string[] envVars)
{
    startupLogger.Information("Checking running environment starting");
    var env = GetRunningEnvironment(envVars);
    if (env is null || !RunningEnvironment.Exists(env))
    {
        startupLogger.Fatal($"Application environment '{env}' is not supported");
        Environment.Exit(1);
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