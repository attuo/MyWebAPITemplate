using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyWebAPITemplate.Source.Infrastructure.Database;
using MyWebAPITemplate.Source.Web.Extensions;
using Serilog;

namespace MyWebAPITemplate.Source.Web
{
    /// <summary>
    /// 
    /// </summary>
    public class Program
    {
        private static readonly string[] _requiredEnvironmentVariables = { "ASPNETCORE_ENVIRONMENT" };

        /// <summary>
        /// Starts the system and seeds the database.
        /// Runs first when the system launches.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            try
            {
                Log.Logger = CreateInitialLogger();

                CheckRequiredEnvVariables();
                CheckRunningEnvironment();


                Log.Information("Application starting");

                var host = CreateHostBuilder(args).Build();
                using var scope = host.Services.CreateScope();
                var services = scope.ServiceProvider;

                MigrateDatabase(services);

                Log.Information("Application starting to run");
                host.Run();
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
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
            => Host
                .CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webHostBuilder
                    => webHostBuilder
                        .UseStartup<Startup>()
                        .CaptureStartupErrors(true)
                        .ConfigureAppConfiguration(configBuilder
                            => configBuilder.SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", false, false)
                                .AddJsonFile("appsettings.Logs.json", false, false)
                                .AddJsonFile($"appsettings.{GetRunningEnvironment()}.json", false, false)
                                .AddEnvironmentVariables())
                        .UseSerilog((hostingContext, loggerConfiguration)
                            => {
                                    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration)
                                   .Enrich.FromLogContext()
                                   .Enrich.WithProperty("ApplicationName", typeof(Program).Assembly.GetName().Name)
                                   .Enrich.WithProperty("Environment", GetRunningEnvironment())
#if DEBUG
                                    // Used to filter out potentially bad data due debugging.
                                    .Enrich.WithProperty("DebuggerAttached", Debugger.IsAttached);
#endif
                               }));

        /// <summary>
        /// 
        /// </summary>
        private static void CheckRequiredEnvVariables()
        {
            Log.Information("Checking required environment variables starting");
            foreach (var environmentVariable in _requiredEnvironmentVariables)
            {
                if (string.IsNullOrWhiteSpace(environmentVariable))
                {
                    Log.Error("Required environment variable is missing: {envVar}", environmentVariable);
                    Environment.Exit(1);
                }
                Log.Information("Required environment variable found: {envVar}", environmentVariable);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static Serilog.ILogger CreateInitialLogger()
            => new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Initial Logger", true)
                .Enrich.WithProperty("ApplicationName", typeof(Program).Assembly.GetName().Name)
                .Enrich.WithProperty("Environment", "Development")
#if DEBUG
                // Used to filter out potentially bad data due debugging.
                .Enrich.WithProperty("DebuggerAttached", Debugger.IsAttached)
#endif
                .CreateLogger();

        /// <summary>
        /// 
        /// </summary>
        private static void CheckRunningEnvironment()
        {
            Log.Information("Checking running environment starting");
            var env = GetRunningEnvironment();
            if (!RunningEnvironment.Exists(env))
            {
                Log.Fatal($"Application environment '{env}' is not supported");
                Environment.Exit(1);
            }
            Log.Information("Checking running environment successful: {env}", env);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        private static void MigrateDatabase(IServiceProvider serviceProvider)
        {
            // TODO: Make environment specific database migrating
            Log.Information("Database migrating starting");
            using var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            try
            {
                dbContext.Database.Migrate();
                Log.Information("Database migrating successful");
            }
            catch (Exception ex)
            {
                Log.Error("Database migrating errored", ex);
                throw;
            }
        }

        private static string GetRunningEnvironment()
            => Environment.GetEnvironmentVariable(_requiredEnvironmentVariables[0]);
    }
}
