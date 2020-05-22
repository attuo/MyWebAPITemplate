using System;
using AspNetCoreWebApiTemplate.Infrastructure.Database;
using AspNetCoreWebApiTemplate.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AspNetCoreWebApiTemplate
{
    public class Program
    {
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
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        
    }
}
