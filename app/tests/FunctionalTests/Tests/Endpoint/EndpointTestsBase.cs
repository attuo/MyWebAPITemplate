using Microsoft.AspNetCore.Mvc.Testing;
using MyWebAPITemplate.Source.Infrastructure.Database;
using MyWebAPITemplate.Tests.FunctionalTests.Utils;
using MyWebAPITemplate.Tests.SharedComponents.Factories;
using Xunit;

namespace MyWebAPITemplate.Tests.FunctionalTests.Tests.Endpoint;

/// <summary>
/// Base class for all the endpoint tests of the system.
/// </summary>
[Collection("Test collection")]
public abstract class EndpointTestsBase : IAsyncLifetime
{
    private const string BASE_ADDRESS_URL = "https://localhost:5001/"; // TODO: This should not be hard coded
    private readonly InitializationFactory _factory;

    /// <summary>
    /// Initializes a new instance of the <see cref="EndpointTestsBase"/> class.
    /// </summary>
    /// <param name="factory">See <see cref="InitializationFactory"/>.</param>
    protected EndpointTestsBase(InitializationFactory factory)
    {
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        HttpApiClient = new HttpApiClient(_factory.CreateClient(new WebApplicationFactoryClientOptions { BaseAddress = new Uri(BASE_ADDRESS_URL) }));
        DbContext = _factory.CreateDbContext();
    }

    /// <summary>
    /// Gets shared HttpClient for the test class to use.
    /// </summary>
    public HttpApiClient HttpApiClient { get; init; }

    public ApplicationDbContext DbContext { get; init; }

    public async Task InitializeAsync() => await _factory.ResetDatabaseAsync();

    public Task DisposeAsync() => Task.CompletedTask;
}