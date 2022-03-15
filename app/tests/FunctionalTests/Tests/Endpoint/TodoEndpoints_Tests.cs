using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using MyWebAPITemplate.Source.Web.Models.ResponseModels;
using MyWebAPITemplate.Tests.FunctionalTests.Utils;
using MyWebAPITemplate.Tests.Shared.Builders.Models;
using MyWebAPITemplate.Tests.Shared.Ids;
using Newtonsoft.Json;
using Xunit;

namespace MyWebAPITemplate.Tests.FunctionalTests.Tests.Endpoint;

/// <summary>
/// All the endpoint tests for Todos.
/// </summary>
public class TodoEndpoints_Tests : EndpointTestsBase
{
    // TODO: Make the sequential and have no side effects from each other.

    private const string EndpointName = "api/Todos/";

    /// <summary>
    /// Initializes a new instance of the <see cref="TodoEndpoints_Tests"/> class.
    /// </summary>
    /// <param name="fixture">See <see cref="TestFixture"/>.</param>
    public TodoEndpoints_Tests(TestFixture fixture) : base(fixture)
    {
    }

    /// <summary>
    /// Happy case test for endpoint on getting all Todos.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task Get_All_OK()
    {
        // Arrange

        // Act
        var response = await Client.GetAsync(EndpointName);
        var responseBody = await response.Content.ReadAsStringAsync();
        var responseTodos = JsonConvert.DeserializeObject<List<TodoResponseModel>>(responseBody);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        // TODO: Make the tests to be independent. Currently this test also get affected by the create todo test
        responseTodos.Should().HaveCount(2);
    }

    /// <summary>
    /// Happy case test for endpoint on getting one Todo.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task Get_One_OK()
    {
        // Arrange
        Guid todoId = TestIds.NormalUsageId;

        // Act
        var response = await Client.GetAsync(EndpointName + todoId);
        var responseBody = await response.Content.ReadAsStringAsync();
        var responseTodo = JsonConvert.DeserializeObject<TodoResponseModel>(responseBody);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        responseTodo.Should().NotBeNull();
    }

    /// <summary>
    /// Unhappy case test for endpoint on getting one Todo.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task Get_One_NotFound()
    {
        // Arrange
        Guid todoId = TestIds.NonUsageId;

        // Act
        var response = await Client.GetAsync(EndpointName + todoId);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    /// <summary>
    /// Happy case test for endpoint on creating one Todo.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task Create_One_OK()
    {
        // Arrange
        var model = TodoRequestModelBuilder.CreateValid();
        var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

        // Act
        var response = await Client.PostAsync(EndpointName, content);
        var responseBody = await response.Content.ReadAsStringAsync();
        var responseTodo = JsonConvert.DeserializeObject<TodoResponseModel>(responseBody);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        responseTodo.Should().NotBeNull();
        responseTodo!.Id.Should().NotBeEmpty();
    }

    /// <summary>
    /// Happy case test for endpoint on updating one Todo.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
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
        var response = await Client.PutAsync(EndpointName + todoId, content);
        var responseBody = await response.Content.ReadAsStringAsync();
        var responseTodo = JsonConvert.DeserializeObject<TodoResponseModel>(responseBody);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        responseTodo.Should().NotBeNull();

        responseTodo!.Description.Should().Be(model.Description);
        responseTodo!.IsDone.Should().Be(model.IsDone);
    }

    /// <summary>
    /// Unhappy case test for endpoint on updating one Todo.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task Update_One_NotFound()
    {
        // Arrange
        Guid todoId = TestIds.NonUsageId;
        var model = TodoRequestModelBuilder.CreateValid();
        var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

        // Act
        var response = await Client.PutAsync(EndpointName + todoId, content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    /// <summary>
    /// Happy case test for endpoint on deleting one Todo.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task Delete_One_OK()
    {
        // Arrange
        Guid todoId = TestIds.OtherUsageId;

        // Act
        var response = await Client.DeleteAsync(EndpointName + todoId);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    /// <summary>
    /// Unhappy case test for endpoint on deleting one Todo.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task Delete_One_NotFound()
    {
        // Arrange
        Guid todoId = TestIds.NonUsageId;

        // Act
        var response = await Client.DeleteAsync(EndpointName + todoId);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}