//using System;
//using System.Collections.Generic;
//using System.Net;
//using System.Text;
//using System.Threading.Tasks;
//using MyWebAPITemplate.Web;
//using FluentAssertions;
//using Microsoft.AspNetCore.Mvc.Testing;
//using Xunit;

//namespace MyWebAPITemplate.IntegrationTests.EndpointTests
//{
//    public class EndpointTests : IClassFixture<WebApplicationFactory<Startup>>
//    {
//        private readonly WebApplicationFactory<Startup> _factory;

//        public EndpointTests(WebApplicationFactory<Startup> factory)
//        {
//            _factory = factory;
//        }

//        [Fact]
//        public async Task Get_Root_Successful()
//        {
//            // Arrange
//            var client = _factory.CreateClient();

//            // Act
//            var response = await client.GetAsync("/");

//            // Assert
//            response.StatusCode.Should().Be(HttpStatusCode.OK);
//        }
//    }
//}
