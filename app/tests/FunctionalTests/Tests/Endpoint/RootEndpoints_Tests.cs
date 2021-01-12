using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using MyWebAPITemplate.Source.Web;
using MyWebAPITemplate.Tests.FunctionalTests.Utils;
using Xunit;

namespace MyWebAPITemplate.Tests.FunctionalTests.EndpointTests
{
    /// <summary>
    /// All the endpoint tests for root
    /// </summary>
    public class RootEndpoints_Tests : IClassFixture<TestFixture<Startup>>
    {
        private const string BASE_ADDRESS = "https://localhost:5001/"; // TODO: This should not be hard coded
        private readonly HttpClient _client;

        public RootEndpoints_Tests(TestFixture<Startup> factory)
        {
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri(BASE_ADDRESS)
            });
        }

        [Fact]
        public async Task Get_Root_Successful()
        {
            // Arrange

            // Act
            var response = await _client.GetAsync("/");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
