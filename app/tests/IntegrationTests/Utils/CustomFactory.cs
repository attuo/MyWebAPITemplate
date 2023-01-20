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

namespace MyWebAPITemplate.Tests.IntegrationTests.Utils;

/// <summary>
/// Sets the test environment
/// </summary>
public class CustomFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly TestcontainerDatabase _dbContainer = new TestcontainersBuilder<MsSqlTestcontainer>()
            .WithDatabase(new MsSqlTestcontainerConfiguration()
            {
                Password = "#testingDockerPassword#",
            })
            .WithName($"IntegrationTesting_RecipeManagement_{Guid.NewGuid()}")
            //.WithImage("mcr.microsoft.com/azure-sql-edge:latest")
            .Build();

    public ApplicationDbContext DbContext { get; set; }

    private DbConnection _dbConnection = default!;
    private Respawner _respawner = default!;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {

            SqlConnectionStringBuilder sqlConnectionStringBuilder =
    new SqlConnectionStringBuilder(_dbContainer.ConnectionString) { TrustServerCertificate = true };
            //sqlConnectionStringBuilder.InitialCatalog = "my stuff";

            string connectionString = sqlConnectionStringBuilder.ConnectionString;

            services.RemoveDbContext<ApplicationDbContext>();
            services.AddDbContext<ApplicationDbContext>(options => { options.UseSqlServer(connectionString); });
            services.EnsureDbCreated<ApplicationDbContext>();
            //services.AddTransient<ArtworkCreator>();

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
        
        SqlConnectionStringBuilder sqlConnectionStringBuilder =
            new SqlConnectionStringBuilder(_dbContainer.ConnectionString)
            { TrustServerCertificate = true };
        //sqlConnectionStringBuilder.InitialCatalog = "my stuff";

        string connectionString = sqlConnectionStringBuilder.ConnectionString;
        _dbConnection = new SqlConnection(connectionString);
        await _dbConnection.OpenAsync();
        _respawner = await Respawner.CreateAsync(_dbConnection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.SqlServer
        });
    }

    public new async Task DisposeAsync() => await _dbContainer.DisposeAsync();

    public async Task ResetDatabaseAsync()
    {
        await _respawner.ResetAsync(_dbConnection);
    }
}

    //protected static DbContextOptions<ApplicationDbContext> CreateNewContextOptions()
    //{
    //    // Create a fresh service provider, and therefore a fresh
    //    // InMemory database instance.
    //    var serviceProvider = new ServiceCollection()
    //        .AddEntityFrameworkInMemoryDatabase()
    //        .BuildServiceProvider();

    //    // Create a new options instance telling the context to use an
    //    // InMemory database and the new service provider.
    //    var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
    //    builder.UseInMemoryDatabase("InMemoryDB-Integration-Tests") // TODO: Config to use real database
    //           .UseInternalServiceProvider(serviceProvider);

    //    return builder.Options;
    //}

    //// TODO: Make this a generic method
    //protected EfRepository<TodoEntity> GetTodoRepository()
    //{
    //    var options = CreateNewContextOptions();

    //    _dbContext = new ApplicationDbContext(options);
    //    return new EfRepository<TodoEntity>(_dbContext);
    //}