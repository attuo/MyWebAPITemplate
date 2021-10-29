using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyWebAPITemplate.Source.Infrastructure.Database;
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
        public static async Task Main(string[] args)
        {
            try
            {
                CheckRequiredEnvVariables();

                var configuration = GetConfiguration();
                Log.Logger = CreateLogger(configuration);

                Log.Information("Application starting");

                var host = BuildWebHost(configuration, args);
                using var scope = host.Services.CreateScope();
                var services = scope.ServiceProvider;

                CheckEnvironment(services);

                await SeedDatabase(services);

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHost BuildWebHost(IConfiguration configuration, string[] args)
            => WebHost
                .CreateDefaultBuilder(args)
                .CaptureStartupErrors(true)
                .ConfigureAppConfiguration(configBuilder 
                    => configBuilder.AddConfiguration(configuration))
                .UseStartup<Startup>()
                .UseSerilog()
                .Build();

        /// <summary>
        /// 
        /// </summary>
        private static void CheckRequiredEnvVariables()
        {
            // TODO: Create some kind of error logging here, since the Serilog is not yet initialized
            foreach (var environmentVariable in _requiredEnvironmentVariables)
                if (string.IsNullOrWhiteSpace(environmentVariable))
                    Environment.Exit(1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static IConfiguration GetConfiguration()
        {
            var env = Environment.GetEnvironmentVariable(_requiredEnvironmentVariables[0]);
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, false)
                .AddJsonFile("appsettings.Logs.json", false, false)
                .AddJsonFile($"appsettings.{env}.json", false, false)
                .AddEnvironmentVariables();
            return builder.Build();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        private static Serilog.ILogger CreateLogger(IConfiguration configuration) 
            => new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
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
        /// <param name="serviceProvider"></param>
        private static void CheckEnvironment(IServiceProvider serviceProvider)
        {
            // Checking environments
            var env = serviceProvider.GetRequiredService<IWebHostEnvironment>();
            if (!env.IsDevelopment() && !env.IsStaging() && !env.IsProduction())
            {
                Log.Error($"Application environment '{env.EnvironmentName}' is not supported");
                Environment.Exit(1);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        private static async Task SeedDatabase(IServiceProvider serviceProvider)
        {
            // TODO: Make environment specific database seeding
            Log.Information("Database seeding starting");
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            await ApplicationDbContextSeed.SeedAsync(context);
            Log.Information("Database seeding ended");
        }
    }
}
