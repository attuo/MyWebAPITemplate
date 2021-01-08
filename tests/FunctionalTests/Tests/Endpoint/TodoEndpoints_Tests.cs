using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MyWebAPITemplate.Tests.Shared.Builders.Models;
using MyWebAPITemplate.Models.ResponseModels;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;
using MyWebAPITemplate.Source.Web;
using MyWebAPITemplate.Tests.UnitTests.Shared.Ids;
using MyWebAPITemplate.Tests.FunctionalTests.Utils;

namespace MyWebAPITemplate.Tests.FunctionalTests.Endpoint
{
    [Collection("Sequential")]
    public class TodoEndpoints_Tests : IClassFixture<TestFixture<Startup>>
    {
        private const string BASE_ADDRESS = "https://localhost:5001/"; // TODO: This should not be hard coded
        private const string ENDPOINT_NAME = "api/Todos/";
        private readonly HttpClient _client;

        public TodoEndpoints_Tests(TestFixture<Startup> factory)
        {
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri(BASE_ADDRESS)
            });
        }

        [Fact]
        public async Task Get_All_OK()
        {
            // Arrange


            // Act
            var response = await _client.GetAsync(ENDPOINT_NAME);
            var responseBody = await response.Content.ReadAsStringAsync();
            var responseTodos = JsonConvert.DeserializeObject<List<TodoResponseModel>>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            // TODO: Make the tests to be independent. Currently this test also get affected by the create todo test
            responseTodos.Should().HaveCount(2); 

        }

        [Fact]
        public async Task Get_One_OK()
        {
            // Arrange
            Guid todoId = TestIds.NormalUsageId;

            // Act
            var response = await _client.GetAsync(ENDPOINT_NAME + todoId);
            var responseBody = await response.Content.ReadAsStringAsync();
            var responseTodo = JsonConvert.DeserializeObject<TodoResponseModel>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            responseTodo.Should().NotBeNull();
            
        }

        [Fact]
        public async Task Get_One_NotFound()
        {
            // Arrange
            Guid todoId = TestIds.NonUsageId;

            // Act
            var response = await _client.GetAsync(ENDPOINT_NAME + todoId);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }


        [Fact]
        public async Task Create_One_OK()
        {
            // Arrange
            var model = TodoRequestModelBuilder.CreateValid();
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync(ENDPOINT_NAME, content);
            var responseBody = await response.Content.ReadAsStringAsync();
            var responseTodo = JsonConvert.DeserializeObject<TodoResponseModel>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            responseTodo.Should().NotBeNull();
            responseTodo.Id.Should().NotBeEmpty();
            
        }

        [Fact]
        public async Task Update_One_OK()
        {
            // Arrange
            Guid todoId = TestIds.NormalUsageId;
            var model = TodoRequestModelBuilder.CreateValid();
            model.Description = "Changed description";
            model.IsDone = true;
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync(ENDPOINT_NAME + todoId, content);
            var responseBody = await response.Content.ReadAsStringAsync();
            var responseTodo = JsonConvert.DeserializeObject<TodoResponseModel>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            responseTodo.Should().NotBeNull();
            
            responseTodo.Description.Should().Be(model.Description);
            responseTodo.IsDone.Should().Be(model.IsDone);
        }

        [Fact]
        public async Task Update_One_NotFound()
        {
            // Arrange
            Guid todoId = TestIds.NonUsageId;
            var model = TodoRequestModelBuilder.CreateValid();
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync(ENDPOINT_NAME + todoId, content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Delete_One_OK()
        {
            // Arrange
            Guid todoId = TestIds.OtherUsageId;

            // Act
            var response = await _client.DeleteAsync(ENDPOINT_NAME + todoId);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Delete_One_NotFound()
        {
            // Arrange
            Guid todoId = TestIds.NonUsageId;

            // Act
            var response = await _client.DeleteAsync(ENDPOINT_NAME + todoId);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}