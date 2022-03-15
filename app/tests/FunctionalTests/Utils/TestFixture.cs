using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyWebAPITemplate.Source.Infrastructure.Database;

namespace MyWebAPITemplate.Tests.FunctionalTests.Utils;

/// <summary>
/// Setups the test environment.
/// </summary>
public class TestFixture : WebApplicationFactory<Program>
{
    private readonly string _environment = "Testing";

    /// <summary>
    /// Overrides Host creating to initialize the db context and other usefull stuff.
    /// </summary>
    /// <param name="builder">See <see cref="IHostBuilder"/>.</param>
    /// <returns>New instance of <see cref="IHost"/>.</returns>
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
            if (descriptor != null)
                services.Remove(descriptor);

            services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("InMemoryDbForTesting"));

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
                logger.LogError(
                    ex,
                    "An error occurred seeding the database with test messages. Error: {Message}",
                    ex.Message);
            }
        });

        return base.CreateHost(builder);
    }

    /// <summary>
    /// Overrides Web Host building for testing purposes.
    /// </summary>
    /// <param name="builder">See <see cref="IWebHostBuilder"/>.</param>
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
                services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("InMemoryDB-Functional-Tests"));
            });
    }
}