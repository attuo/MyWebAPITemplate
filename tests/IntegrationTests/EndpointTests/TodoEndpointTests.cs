using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MyWebAPITemplate.IntegrationTests.Builders.Models;
using MyWebAPITemplate.Models.ResponseModels;
using MyWebAPITemplate.Web.Models.RequestModels;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace MyWebAPITemplate.IntegrationTests.EndpointTests
{
    public class TodoEndpointTests : IClassFixture<BaseWebApplicationFactory>
    {
        private const string BASE_ADDRESS = "https://localhost:5001/";
        private const string ENDPOINT_NAME = "api/Todos";
        private readonly HttpClient _client;

        public TodoEndpointTests(BaseWebApplicationFactory factory)
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
            responseTodos.Should().HaveCountGreaterThan(1).And.HaveCountLessThan(3);

        }

        [Fact]
        public async Task Get_One_OK()
        {
            // Arrange
            int todoId = 1;

            // Act
            var response = await _client.GetAsync(ENDPOINT_NAME + "/" + todoId);
            var responseBody = await response.Content.ReadAsStringAsync();
            var responseTodo = JsonConvert.DeserializeObject<TodoResponseModel>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            responseTodo.Should().NotBeNull();
            responseTodo.Id.Should().Be(todoId);
        }

        [Fact]
        public async Task Get_One_NotFound()
        {
            // Arrange
            int todoId = 1000;

            // Act
            var response = await _client.GetAsync(ENDPOINT_NAME + "/" + todoId);

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
            responseTodo.Id.Should().BeGreaterThan(1);
        }

        [Fact]
        public async Task Update_One_OK()
        {
            // Arrange
            int todoId = 1;
            var model = new TodoRequestModel
            {
                Description = "Changed description",
                IsDone = true
            };
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync(ENDPOINT_NAME + "/" + todoId, content);
            var responseBody = await response.Content.ReadAsStringAsync();
            var responseTodo = JsonConvert.DeserializeObject<TodoResponseModel>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            responseTodo.Should().NotBeNull();
            responseTodo.Id.Should().Be(todoId);
            responseTodo.Description.Should().Be(model.Description);
            responseTodo.IsDone.Should().Be(model.IsDone);
        }

        [Fact]
        public async Task Update_One_NotFound()
        {
            // Arrange
            int todoId = 1000;
            var model = new TodoRequestModel
            {
                Description = "Changed description",
                IsDone = true
            };
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync(ENDPOINT_NAME + "/" + todoId, content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Delete_One_OK()
        {
            // Arrange
            int todoId = 2;

            // Act
            var response = await _client.DeleteAsync(ENDPOINT_NAME + "/" + todoId);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Delete_One_NotFound()
        {
            // Arrange
            int todoId = 1000;

            // Act
            var response = await _client.DeleteAsync(ENDPOINT_NAME + "/" + todoId);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}