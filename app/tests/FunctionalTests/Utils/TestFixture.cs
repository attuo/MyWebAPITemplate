using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyWebAPITemplate.Source.Infrastructure.Database;
using MyWebAPITemplate.Source.Web;

namespace MyWebAPITemplate.Tests.FunctionalTests.Utils;

/// <summary>
/// Sets the test environment
/// </summary>
public class TestFixture : WebApplicationFactory<Program>
{

    private readonly string _environment = "Testing";

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.UseEnvironment(_environment);
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", _environment);

        // Add mock/test services to the builder here
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<ApplicationDbContext>));

            services.Remove(descriptor);

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting");
            });

            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<ApplicationDbContext>();
            var logger = scopedServices
                .GetRequiredService<ILogger<TestFixture>>();

            db.Database.EnsureCreated();

            try
            {
                TestDatabaseSeed.ReinitializeDbForTests(db).Wait();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred seeding the " +
                    "database with test messages. Error: {Message}", ex.Message);
            }
        });

        return base.CreateHost(builder);
    }

    //protected override IHost CreateHost(IHostBuilder builder)
    //{
    //    Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
    //    var host = builder.Build();
    //    var serviceProvider = host.Services;
    //    // Create a scope to obtain a reference to the database context
    //    using (var scope = serviceProvider.CreateScope())
    //    {
    //        var scopedServices = scope.ServiceProvider;
    //        var db = scopedServices.GetRequiredService<ApplicationDbContext>();

    //        var logger = scopedServices
    //            .GetRequiredService<ILogger<TestFixture<TStartup>>>();

    //        // Ensure the database is created.
    //        db.Database.EnsureCreated();

    //        try
    //        {
    //            // Seed the database with test data.
    //            //TestDatabaseSeed.ReinitializeDbForTests(db).Wait();
    //        }
    //        catch (Exception ex)
    //        {
    //            logger.LogError(ex, "An error occurred seeding the " +
    //                                $"database with test messages. Error: {ex.Message}");
    //        }
    //    }

    //    host.Start();
    //    return host;
    //}

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder
            .UseSolutionRelativeContentRoot("src/Web")
            .ConfigureServices(services =>
            {
                // Remove the app's ApplicationDbContext registration.
                var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(DbContextOptions<ApplicationDbContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Add ApplicationDbContext using an in-memory database for testing.
                services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDB-Functional-Tests"); // TODO: Config to use real database
            });
            });
    }
}
