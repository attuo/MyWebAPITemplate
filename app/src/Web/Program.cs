using FluentValidation.AspNetCore;
using MyWebAPITemplate.Source.Infrastructure.Database;
using MyWebAPITemplate.Source.Web.Extensions;
using MyWebAPITemplate.Source.Web.Interfaces;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

var startupLogger = CreateInitialLogger();

await Start();

// Method that will start first when the system launches.
async Task Start()
{
    string[] requiredEnvironmentVariables = { "ASPNETCORE_ENVIRONMENT" };
    try
    {
        CheckRequiredEnvVariables(requiredEnvironmentVariables);
        var env = GetCurrentEnvironment(requiredEnvironmentVariables);

        startupLogger.Information("Application starting.");
        var builder = WebApplication.CreateBuilder(args);
        SetConfiguration(builder.Configuration, env);
        SetHost(builder.Host, env);
        SetWebHost(builder.WebHost);
        SetServices(builder.Services, builder.Configuration, env);

        var app = builder.Build();
        SetApplications(app, env);

        await SeedDatabase(app.Services, env);

        startupLogger.Information("Application starting to run.");
        app.Run();
    }
    catch (Exception ex)
    {
        // https://github.com/dotnet/runtime/issues/60600
        string type = ex.GetType().Name;
        if (type.Equals("StopTheHostException", StringComparison.Ordinal))
            throw;
        startupLogger.Fatal(ex, "Application did not start successfully.");
    }
    // {
    //    // This is commented out because the EF migration stops working with it
    //    // finally
    //    startupLogger.Information("Application is closing");
    //    Environment.Exit(1);
    // }
}

static void SetConfiguration(IConfigurationBuilder configBuilder, IRunningEnvironment env)
    => configBuilder
        // .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", false, false)
        .AddJsonFile("appsettings.Logs.json", false, false)
        .AddJsonFile($"appsettings.{env.Name}.json", false, false)
        .AddEnvironmentVariables();

static void SetHost(ConfigureHostBuilder configHostBuilder, IRunningEnvironment env)
    => configHostBuilder
        .UseConfiguredSerilog(env);

static void SetWebHost(ConfigureWebHostBuilder configWebHostBuilder)
    => configWebHostBuilder
        .CaptureStartupErrors(true);

static void SetServices(IServiceCollection services, IConfiguration configuration, IRunningEnvironment env)
    => services
        .ConfigureDatabase(configuration)
        .ConfigureDevelopmentSettings()
        .ConfigureCors()
        .ConfigureSwagger()
        .ConfigureHealthChecks(configuration, env)
        .ConfigureOptions(configuration)
        .AddApplicationServices()
        .AddApplicationMappers()
        .AddApplicationRepositories()
        .AddModelValidators()
        .AddControllers()
        .AddFluentValidation(); // this must be called directly after AddControllers

static void SetApplications(IApplicationBuilder app, IRunningEnvironment env)
    => app
        .ConfigureDevelopmentSettings(env)
        .ConfigureSwagger()
        .ConfigureLogger()
        .UseCustomMiddlewares()
        .ConfigureRouting(); // This usually must be called the last

void CheckRequiredEnvVariables(string[] requiredEnvVars)
{
    startupLogger.Information("Checking required environment variables starting.");
    var allEnvVars = Environment.GetEnvironmentVariables();
    foreach (var reqEnvVar in requiredEnvVars)
    {
        if (!allEnvVars.Contains(reqEnvVar))
        {
            startupLogger.Error("Required environment variable is missing: {RequiredEnvironmentVariable}.", reqEnvVar);
            throw new ArgumentNullException(reqEnvVar);
        }

        startupLogger.Information("Required environment variable found: {RequiredEnvironmentVariable}.", reqEnvVar);
    }
}

IRunningEnvironment GetCurrentEnvironment(string[] envVars)
{
    startupLogger.Information("Checking running environment starting.");

    var envs = Environment.GetEnvironmentVariables();

    var env = GetRunningEnvironment(envVars);
    if (env is null || !RunningEnvironment.Exists(env))
    {
        startupLogger.Fatal($"Application environment '{env}' is not supported.");
        throw new ArgumentNullException(env);
    }

    startupLogger.Information("Checking running environment successful: {Environment}.", env);
    return RunningEnvironment.Get(env)!;
}

async Task SeedDatabase(IServiceProvider serviceProvider, IRunningEnvironment env)
{
    startupLogger.Information("Database seeding starting.");
    using var scope = serviceProvider.CreateScope();
    var scopedProvider = scope.ServiceProvider;
    try
    {
        if (env.IsLocalDevelopment())
        {
            var dbContext = scopedProvider.GetRequiredService<ApplicationDbContext>();

            await ApplicationDbContextSeed.SeedDevelopAsync(dbContext);
            startupLogger.Information("Database seeding successful.");
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

static Serilog.ILogger CreateInitialLogger()
    // Only used for logging purposes that happen before the application starts.
    => new LoggerConfiguration()
        .MinimumLevel.Debug()
        .WriteTo.Console(
            outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}",
            theme: AnsiConsoleTheme.Code)
        .Enrich.FromLogContext()
        .Enrich.WithProperty("Initial Logger", true)
        .Enrich.WithProperty("ApplicationName", typeof(Program).Assembly.GetName().Name)
        .Enrich.WithProperty("Environment", "Development")
        .CreateBootstrapLogger();

/// <summary>
/// Set implicitly for the tests.
/// </summary>
#pragma warning disable CA1050 // Declare types in namespaces
public partial class Program
{
}
#pragma warning restore CA1050 // Declare types in namespaces