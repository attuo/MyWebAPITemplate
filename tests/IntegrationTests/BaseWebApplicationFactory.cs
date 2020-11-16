using System;
using AspNetCoreWebApiTemplate.Infrastructure.Database;
using AspNetCoreWebApiTemplate.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AspNetCoreWebApiTemplate.IntegrationTests
{
    public class BaseWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");

            builder.ConfigureTestServices(services =>
            {

                // Create a new service provider.
                var provider = services
                    .BuildServiceProvider();

                // Add a database context (ApplicationDbContext) using an in-memory 
                // database for testing.
                services
                .AddEntityFrameworkSqlite()
                .AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseSqlite("Data Source=:memory:");
                    options.UseInternalServiceProvider(provider);
                });


                // Build the service provider.
                var sp = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database
                // context (ApplicationDbContext).
                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<ApplicationDbContext>();
                var loggerFactory = scopedServices.GetRequiredService<ILoggerFactory>();

                var logger = scopedServices
                    .GetRequiredService<ILogger<BaseWebApplicationFactory>>();

                // Ensure the database is created.
                db.Database.EnsureCreated();

                try
                {
                    // Seed the database with test data.
                    Utilities.ReinitializeDbForTests(db); // TODO: Not working, fix.
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"An error occurred seeding the " +
                        "database with test messages. Error: {ex.Message}");
                }
            });
        }
    }
}
