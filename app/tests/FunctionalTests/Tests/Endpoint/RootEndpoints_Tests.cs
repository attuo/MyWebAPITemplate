using System.Net;
using FluentAssertions;
using MyWebAPITemplate.Tests.SharedComponents.Factories;
using Xunit;

namespace MyWebAPITemplate.Tests.FunctionalTests.Tests.Endpoint;

/// <summary>
/// All the endpoint tests for root.
/// </summary>
public class RootEndpoints_Tests : EndpointTestsBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RootEndpoints_Tests"/> class.
    /// </summary>
    /// <param name="fixture">See <see cref="InitializationFactory"/>.</param>
    public RootEndpoints_Tests(InitializationFactory fixture) : base(fixture)
    {
    }

    /// <summary>
    /// Happy case for calling root endpoint.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task Get_Root_Successful()
    {
        // Arrange

        // Act
        var response = await HttpApiClient.Get();

        // Assert
        _ = response.Should().Be(HttpStatusCode.NotFound);
    }
}