using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyWebAPITemplate.Source.Infrastructure.Database;
using MyWebAPITemplate.Source.Web;

namespace MyWebAPITemplate
{
    public class Program
    {
        /// <summary>
        /// Starts the system and seeds the database
        /// Runs first when the system launches
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            // Checking environments
            var env = services.GetRequiredService<IWebHostEnvironment>();
            if (!env.IsDevelopment() && !env.IsStaging() && !env.IsProduction())
            {
                // Not any of the supported environments
                Environment.Exit(1);
            }

            // TODO: Make enviroment specific database seeding
            // Database seeding
            try
            {
                var context = services.GetRequiredService<ApplicationDbContext>();
                ApplicationDbContextSeed.SeedAsync(context).Wait();
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred seeding the DB.");
            }

            host.Run();
        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, configuration) =>
                {
                    configuration.Sources.Clear();

                    IHostEnvironment env = hostingContext.HostingEnvironment;

                    // TODO: Configs for test environment etc.
                    configuration
                        .AddJsonFile("appsettings.json", false, false)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", false, false)
                        .AddEnvironmentVariables();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });


    }
}
