using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyWebAPITemplate.Source.Infrastructure.Database;
using MyWebAPITemplate.Source.Infrastructure.Database.Repositories;

namespace MyWebAPITemplate.Tests.IntegrationTests.Utils;

/// <summary>
/// Sets the test environment.
/// </summary>
public abstract class TestFixture
{
    private readonly ApplicationDbContext _dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="TestFixture"/> class.
    /// </summary>
    protected TestFixture()
    {
        var options = CreateNewContextOptions();
        _dbContext = new ApplicationDbContext(options);
    }

    /// <summary>
    /// Sets a new context options for the existing context.
    /// </summary>
    /// <returns><see cref="DbContextOptions"/>.</returns>
    protected static DbContextOptions<ApplicationDbContext> CreateNewContextOptions()
    {
        // Create a fresh service provider, and therefore a fresh
        // InMemory database instance.
        var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .BuildServiceProvider();

        // Create a new options instance telling the context to use an
        // InMemory database and the new service provider.
        var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
        _ = builder.UseInMemoryDatabase("InMemoryDB-Integration-Tests") // TODO: Config to use real database
               .UseInternalServiceProvider(serviceProvider);

        return builder.Options;
    }

    /// <summary>
    /// Initializes a TodoRepository for the tests.
    /// </summary>
    /// <returns>Created instance.</returns>
    protected TodoRepository GetTodoRepository() => new(_dbContext);
}