using System.Data.Common;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyWebAPITemplate.Source.Infrastructure.Database;
using Respawn;
using Xunit;

namespace MyWebAPITemplate.Tests.FunctionalTests.Utils;

public class CustomFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly TestcontainerDatabase _dbContainer = CreateTestContainerDatabase();
    private string _connectionString;
    private DbConnection _dbConnection;
    private Respawner _respawner;

    public CustomFactory()
    {
        _dbContainer = CreateTestContainerDatabase();
        _dbConnection = default!;
        _respawner = default!;
    }

    public ApplicationDbContext? DbContext { get; set; }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        _ = builder.UseEnvironment("Testing");
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Testing");

        builder.ConfigureTestServices(services =>
        {
            services.RemoveDbContext<ApplicationDbContext>();
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(_connectionString));
            services.EnsureDbCreated<ApplicationDbContext>();

            // Build the service provider.
            var serviceProvider = services.BuildServiceProvider();
            // Create a scope to obtain a reference to the database
            // context (ApplicationDbContext).
            using var scope = serviceProvider.CreateScope();
            DbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            builder.UseEnvironment(Environments.Development);
        });
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        _connectionString = CreateConnectionString(_dbContainer);

        _dbConnection = new SqlConnection(_connectionString);
        await _dbConnection.OpenAsync();
        _respawner = await Respawner.CreateAsync(_dbConnection, new RespawnerOptions { DbAdapter = DbAdapter.SqlServer });
    }

    public new async Task DisposeAsync() => await _dbContainer.DisposeAsync();

    public async Task ResetDatabaseAsync()
    {
        await _respawner.ResetAsync(_dbConnection);
    }

    public static TestcontainerDatabase CreateTestContainerDatabase()
        => new TestcontainersBuilder<MsSqlTestcontainer>()
            .WithDatabase(new MsSqlTestcontainerConfiguration() { Password = "TestPassword1!" })
            .Build();


    public static string CreateConnectionString(TestcontainerDatabase dbContainer)
        => new SqlConnectionStringBuilder(dbContainer.ConnectionString) { TrustServerCertificate = true }
            .ConnectionString;
}

/// <summary>
/// Setups the test environment.
/// </summary>
//public class CustomFactory : WebApplicationFactory<Program>, IAsyncLifetime
//{
//    private readonly string _environment = "Testing";
//    private readonly MsSqlTestcontainer _dbContainer;

//    public CustomFactory()
//    {
//        _dbContainer = new TestcontainersBuilder<MsSqlTestcontainer>()
//        .WithDatabase(new MsSqlTestcontainerConfiguration()
//        {
//            Database = "mydb",
//            Username = "Jee",
//            Password = "Jee"
//        })
//        .Build();
//    }

//    /// <summary>
//    /// Overrides Host creating to initialize the db context and other usefull stuff.
//    /// </summary>
//    /// <param name="builder">See <see cref="IHostBuilder"/>.</param>
//    /// <returns>New instance of <see cref="IHost"/>.</returns>
//    protected override IHost CreateHost(IHostBuilder builder)
//    {
//        _ = builder.UseEnvironment(_environment);
//        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", _environment);

//        // Add mock/test services to the builder here
//        _ = builder.ConfigureServices(services =>
//        {
//            var descriptor = services.SingleOrDefault(
//                    d => d.ServiceType ==
//                        typeof(DbContextOptions<ApplicationDbContext>));
//            if (descriptor != null)
//                _ = services.Remove(descriptor);

//            _ = services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("InMemoryDbForTesting"));

//            var sp = services.BuildServiceProvider();

//            using var scope = sp.CreateScope();
//            var scopedServices = scope.ServiceProvider;
//            var db = scopedServices.GetRequiredService<ApplicationDbContext>();
//            var logger = scopedServices.GetService<Serilog.ILogger>();

//            _ = db.Database.EnsureCreated();

//            try
//            {
//                TestDatabaseSeed.ReinitializeDbForTests(db).Wait();
//            }
//            catch (Exception ex)
//            {
//                logger?.Error(
//                    ex,
//                    "An error occurred seeding the database with test messages. Error: {Message}",
//                    ex.Message);
//            }
//        });

//        return base.CreateHost(builder);
//    }

//    /// <summary>
//    /// Overrides Web Host building for testing purposes.
//    /// </summary>
//    /// <param name="builder">See <see cref="IWebHostBuilder"/>.</param>
//    protected override void ConfigureWebHost(IWebHostBuilder builder)
//    {
//        _ = builder
//            .UseSolutionRelativeContentRoot("src/Web")
//            .ConfigureServices(services =>
//            {
//                // Remove the app's ApplicationDbContext registration.
//                var descriptor = services.SingleOrDefault(
//                d => d.ServiceType ==
//                    typeof(DbContextOptions<ApplicationDbContext>));

//                if (descriptor != null)
//                {
//                    _ = services.Remove(descriptor);
//                }

//                // Add ApplicationDbContext using an in-memory database for testing.
//                _ = services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("InMemoryDB-Functional-Tests"));
//            });
//    }

//    public async Task InitializeAsync() => await _dbContainer.StartAsync();

//    public new async Task DisposeAsync() => await _dbContainer.DisposeAsync();