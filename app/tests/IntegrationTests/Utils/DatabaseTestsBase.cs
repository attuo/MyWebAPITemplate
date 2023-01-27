using MyWebAPITemplate.Source.Infrastructure.Database;
using MyWebAPITemplate.Tests.SharedComponents.Factories;
using Xunit;

namespace MyWebAPITemplate.Tests.FunctionalTests.Tests.Endpoint;

/// <summary>
/// Base class for all the endpoint tests of the system.
/// </summary>
[Collection("Test collection")]
public abstract class DatabaseTestsBase : IAsyncLifetime
{
    private readonly InitializationFactory _factory;

    /// <summary>
    /// Initializes a new instance of the <see cref="DatabaseTestsBase"/> class.
    /// </summary>
    /// <param name="factory">See <see cref="InitializationFactory"/>.</param>
    protected DatabaseTestsBase(InitializationFactory factory)
    {
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        DbContext = _factory.CreateDbContext();
    }

    public ApplicationDbContext DbContext { get; init; }

    public Task DisposeAsync() => Task.CompletedTask;

    public async Task InitializeAsync() => await _factory.ResetDatabaseAsync();
}