using Microsoft.AspNetCore.Mvc.Testing;
using MyWebAPITemplate.Source.Infrastructure.Database;
using MyWebAPITemplate.Tests.FunctionalTests.Utils;
using Xunit;

namespace MyWebAPITemplate.Tests.FunctionalTests.Tests.Endpoint;

/// <summary>
/// Base class for all the endpoint tests of the system.
/// </summary>
[Collection("Test collection")]
public abstract class EndpointTestsBase : IAsyncLifetime
{
    private const string BASE_ADDRESS_URL = "https://localhost:5001/"; // TODO: This should not be hard coded
    private readonly CustomFactory _factory;

    /// <summary>
    /// Initializes a new instance of the <see cref="EndpointTestsBase"/> class.
    /// </summary>
    /// <param name="factory">See <see cref="CustomFactory"/>.</param>
    protected EndpointTestsBase(CustomFactory factory)
    {
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        DbContext = factory.DbContext;
        Client = _factory.CreateClient(new WebApplicationFactoryClientOptions { BaseAddress = new Uri(BASE_ADDRESS_URL) });
    }

    public ApplicationDbContext DbContext { get; init; }

    /// <summary>
    /// Gets shared HttpClient for the test class to use.
    /// </summary>
    public HttpClient Client { get; init; }

    public Task DisposeAsync() => Task.CompletedTask;

    public Task InitializeAsync() => _factory.ResetDatabaseAsync();
}