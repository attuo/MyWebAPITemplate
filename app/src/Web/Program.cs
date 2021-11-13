using System;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyWebAPITemplate.Source.Extensions;
using MyWebAPITemplate.Source.Infrastructure.Database;
using MyWebAPITemplate.Source.Web.Extensions;
using Serilog;

string[] _requiredEnvironmentVariables = { "ASPNETCORE_ENVIRONMENT" };

try
{
    Log.Logger = CreateInitialLogger();
    CheckRequiredEnvVariables(_requiredEnvironmentVariables);
    var env = GetCurrentEnvironment(_requiredEnvironmentVariables);

    Log.Information("Application starting");
    var builder = WebApplication.CreateBuilder(args);
    SetConfiguration(builder.Configuration, env);
    SetWebHost(builder.WebHost, env);
    SetServices(builder.Services, builder.Configuration);

    var app = builder.Build();
    SetApplications(app, builder.Environment);

    MigrateDatabase(app.Services);

    Log.Information("Application starting to run");
    app.Run();
}
catch (Exception ex)
{
    Log.Error(ex, "Application did not start successfully");
}
finally
{
    Log.Information("Application is closing");
    Log.CloseAndFlush();
}

static void SetConfiguration(IConfigurationBuilder configBuilder, RunningEnvironment env)
{
    configBuilder
        //.SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", false, false)
        .AddJsonFile("appsettings.Logs.json", false, false)
        .AddJsonFile($"appsettings.{env.Name}.json", false, false)
        .AddEnvironmentVariables();
}

static void SetWebHost(ConfigureWebHostBuilder configWebHostBuilder, RunningEnvironment env)
{
    configWebHostBuilder
        .CaptureStartupErrors(true)
        .UseSerilog((hostingContext, loggerConfiguration)
            => loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("ApplicationName", typeof(Program).Assembly.GetName().Name)
                .Enrich.WithProperty("Environment", env.Name));
}

static void SetServices(IServiceCollection services, IConfiguration configuration)
{
    services
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
}

static void SetApplications(IApplicationBuilder app, IWebHostEnvironment env)
{
    app
    .ConfigureDevelopmentSettings(env)
    .ConfigureSwagger()
    .ConfigureLogger()
    .ConfigureRouting(); // This usually must be called the last
}

static void CheckRequiredEnvVariables(string[] envVars)
{
    Log.Information("Checking required environment variables starting");
    foreach (var envVar in envVars)
    {
        if (string.IsNullOrWhiteSpace(envVar))
        {
            Log.Error("Required environment variable is missing: {envVar}", envVar);
            Environment.Exit(1);
        }
        Log.Information("Required environment variable found: {envVar}", envVar);
    }
}

static ILogger CreateInitialLogger()
    => new LoggerConfiguration()
        .MinimumLevel.Debug()
        .WriteTo.Console()
        .Enrich.FromLogContext()
        .Enrich.WithProperty("Initial Logger", true)
        .Enrich.WithProperty("ApplicationName", typeof(Program).Assembly.GetName().Name)
        .Enrich.WithProperty("Environment", "Development")
        .CreateLogger();

static RunningEnvironment GetCurrentEnvironment(string[] envVars)
{
    Log.Information("Checking running environment starting");
    var env = GetRunningEnvironment(envVars);
    if (env is null || !RunningEnvironment.Exists(env))
    {
        Log.Fatal($"Application environment '{env}' is not supported");
        Environment.Exit(1);
    }
    Log.Information("Checking running environment successful: {env}", env);
    return RunningEnvironment.Get(env)!;
}

static void MigrateDatabase(IServiceProvider serviceProvider)
{
    Log.Information("Database migrating starting");
    using var scope = serviceProvider.CreateScope();
    var services = scope.ServiceProvider;
    using var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
    try
    {
        // TODO: Make environment specific database migrating
        dbContext.Database.Migrate();
        Log.Information("Database migrating successful");

    }
    catch (Exception ex)
    {
        Log.Error("Database migrating errored", ex);
        throw;
    }
}

static string? GetRunningEnvironment(string[] envVars) => Environment.GetEnvironmentVariable(envVars[0]);

