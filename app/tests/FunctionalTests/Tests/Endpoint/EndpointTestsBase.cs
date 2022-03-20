using Microsoft.AspNetCore.Mvc.Testing;
using MyWebAPITemplate.Tests.FunctionalTests.Utils;
using Xunit;

namespace MyWebAPITemplate.Tests.FunctionalTests.Tests.Endpoint;

/// <summary>
/// Base class for all the endpoint tests of the system.
/// </summary>
public abstract class EndpointTestsBase : IClassFixture<TestFixture>
{
    private const string BaseAddressUrl = "https://localhost:5001/"; // TODO: This should not be hard coded

    /// <summary>
    /// Initializes a new instance of the <see cref="EndpointTestsBase"/> class.
    /// </summary>
    /// <param name="fixture">See <see cref="TestFixture"/>.</param>
    protected EndpointTestsBase(TestFixture fixture)
    {
        _ = fixture ?? throw new ArgumentNullException(nameof(fixture));

        Client = fixture

            .CreateClient(new WebApplicationFactoryClientOptions { BaseAddress = new Uri(BaseAddressUrl) });
    }

    /// <summary>
    /// Gets shared HttpClient for the test class to use.
    /// </summary>
    public HttpClient Client { get; init; }
}