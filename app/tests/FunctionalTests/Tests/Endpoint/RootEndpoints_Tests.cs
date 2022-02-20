using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using MyWebAPITemplate.Tests.FunctionalTests.Utils;
using Xunit;

namespace MyWebAPITemplate.Tests.FunctionalTests.Tests.Endpoint;

/// <summary>
/// All the endpoint tests for root
/// </summary>
//[Collection("Sequential")]
public class RootEndpoints_Tests : EndpointTestsBase
{
    public RootEndpoints_Tests(TestFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task Get_Root_Successful()
    {
        // Arrange

        // Act
        var response = await Client.GetAsync("/");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}