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
using MyWebAPITemplate.Source.Infrastructure.Database;
using MyWebAPITemplate.Source.Web.Extensions;
using Respawn;
using Xunit;

namespace MyWebAPITemplate.Tests.SharedComponents.Factories;

public class InitializationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly TestcontainerDatabase _dbContainer = CreateTestContainerDatabase();
    private string _connectionString;
    private DbConnection _dbConnection;
    private Respawner _respawner;

    public InitializationFactory()
    {
        _dbContainer = CreateTestContainerDatabase();
        _dbConnection = default!;
        _respawner = default!;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", RunningEnvironment.Testing.Name);

        builder.ConfigureTestServices(services =>
        {
            services.RemoveDbContext<ApplicationDbContext>();
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(_connectionString));
            services.EnsureDbCreated<ApplicationDbContext>();
        });
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        _connectionString = CreateConnectionString(_dbContainer);

        await CreateDbContext().Database.EnsureCreatedAsync(); // Needs to happen before Respawner.CreateAsync()

        _dbConnection = new SqlConnection(_connectionString);
        await _dbConnection.OpenAsync();
        _respawner = await Respawner.CreateAsync(
            _dbConnection,
            new RespawnerOptions { DbAdapter = DbAdapter.SqlServer });
    }

    public new async Task DisposeAsync() => await _dbContainer.DisposeAsync();

    public async Task ResetDatabaseAsync() => await _respawner.ResetAsync(_dbConnection);

    public ApplicationDbContext CreateDbContext()
        => new(new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(_connectionString).Options);

    private static TestcontainerDatabase CreateTestContainerDatabase()
        => new TestcontainersBuilder<MsSqlTestcontainer>()
            .WithDatabase(new MsSqlTestcontainerConfiguration() { Password = "TestPassword1!" })
            .Build();

    private static string CreateConnectionString(TestcontainerDatabase dbContainer)
        => new SqlConnectionStringBuilder(dbContainer.ConnectionString) { TrustServerCertificate = true }
            .ConnectionString;
}