using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using MyWebAPITemplate.Source.Web;
using MyWebAPITemplate.Source.Web.Models.ResponseModels;
using MyWebAPITemplate.Tests.FunctionalTests.Utils;
using MyWebAPITemplate.Tests.Shared.Builders.Models;
using MyWebAPITemplate.Tests.Shared.Ids;
using Newtonsoft.Json;
using Xunit;

namespace MyWebAPITemplate.Tests.FunctionalTests.Tests.Endpoint;

/// <summary>
/// All the endpoint tests for Todos
/// </summary>
//[Collection("Sequential")]
public class TodoEndpoints_Tests : EndpointTestsBase
{
    private const string ENDPOINT_NAME = "api/Todos/";

    public TodoEndpoints_Tests(TestFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task Get_All_OK()
    {
        // Arrange

        // Act
        var response = await Client.GetAsync(ENDPOINT_NAME);
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
        var response = await Client.GetAsync(ENDPOINT_NAME + todoId);
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
        var response = await Client.GetAsync(ENDPOINT_NAME + todoId);

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
        var response = await Client.PostAsync(ENDPOINT_NAME, content);
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
        var response = await Client.PutAsync(ENDPOINT_NAME + todoId, content);
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
        var response = await Client.PutAsync(ENDPOINT_NAME + todoId, content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Delete_One_OK()
    {
        // Arrange
        Guid todoId = TestIds.OtherUsageId;

        // Act
        var response = await Client.DeleteAsync(ENDPOINT_NAME + todoId);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Delete_One_NotFound()
    {
        // Arrange
        Guid todoId = TestIds.NonUsageId;

        // Act
        var response = await Client.DeleteAsync(ENDPOINT_NAME + todoId);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}