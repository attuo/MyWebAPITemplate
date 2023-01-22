using System.Net;
using System.Text;
using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MyWebAPITemplate.Source.Core.Entities;
using MyWebAPITemplate.Source.Web.Models.ResponseModels;
using MyWebAPITemplate.Tests.FunctionalTests.Utils;
using MyWebAPITemplate.Tests.SharedComponents.Builders.Models;
using MyWebAPITemplate.Tests.SharedComponents.Ids;
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
    private readonly Fixture _fixture = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="TodoEndpoints_Tests"/> class.
    /// </summary>
    /// <param name="factory">See <see cref="CustomFactory"/>.</param>
    public TodoEndpoints_Tests(CustomFactory factory) : base(factory)
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
        var entities = _fixture.CreateMany<TodoEntity>(2);
        await DbContext.Todos.AddRangeAsync(entities);
        await DbContext.SaveChangesAsync();

        // Act
        var response = await Client.GetAsync(EndpointName);
        var responseBody = await response.Content.ReadAsStringAsync();
        var responseTodos = JsonConvert.DeserializeObject<List<TodoResponseModel>>(responseBody);

        // Assert
        _ = response.StatusCode.Should().Be(HttpStatusCode.OK);
        // TODO: Make the tests to be independent. Currently this test also get affected by the create todo test
        _ = responseTodos.Should().HaveCount(2);
    }

    /// <summary>
    /// Happy case test for endpoint on getting one Todo.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task Get_One_OK()
    {
        // Arrange
        var entity = _fixture.Create<TodoEntity>();
        await DbContext.Todos.AddRangeAsync(entity);
        await DbContext.SaveChangesAsync();

        // Act
        var response = await Client.GetAsync(EndpointName + entity.Id);
        var responseBody = await response.Content.ReadAsStringAsync();
        var responseTodo = JsonConvert.DeserializeObject<TodoResponseModel>(responseBody);

        // Assert
        _ = response.StatusCode.Should().Be(HttpStatusCode.OK);
        _ = responseTodo.Should().NotBeNull();
        _ = responseTodo.Id.Should().Be(entity.Id);
    }

    /// <summary>
    /// Unhappy case test for endpoint on getting one Todo.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task Get_One_NotFound()
    {
        // Arrange
        var entity = _fixture.Create<TodoEntity>();
        await DbContext.Todos.AddRangeAsync(entity);
        await DbContext.SaveChangesAsync();
        Guid todoId = TestIds.NonUsageId;

        // Act
        var response = await Client.GetAsync(EndpointName + todoId);

        // Assert
        _ = response.StatusCode.Should().Be(HttpStatusCode.NotFound);
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
        _ = response.StatusCode.Should().Be(HttpStatusCode.OK);
        _ = responseTodo.Should().NotBeNull();
        _ = responseTodo!.Id.Should().NotBeEmpty();
    }

    /// <summary>
    /// Happy case test for endpoint on updating one Todo.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task Update_One_OK()
    {
        // Arrange
        var entity = _fixture.Create<TodoEntity>();
        await DbContext.Todos.AddRangeAsync(entity);
        await DbContext.SaveChangesAsync();

        var model = TodoRequestModelBuilder.CreateValid();
        model.Description = "Changed description";
        model.IsDone = !entity.IsDone;
        var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

        // Act
        var response = await Client.PutAsync(EndpointName + entity.Id, content);
        var responseBody = await response.Content.ReadAsStringAsync();
        var responseTodo = JsonConvert.DeserializeObject<TodoResponseModel>(responseBody);

        // Assert
        _ = response.StatusCode.Should().Be(HttpStatusCode.OK);
        _ = responseTodo.Should().NotBeNull();

        _ = responseTodo!.Description.Should().Be(model.Description);
        _ = responseTodo!.IsDone.Should().Be(model.IsDone);
    }

    /// <summary>
    /// Unhappy case test for endpoint on updating one Todo.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task Update_One_NotFound()
    {
        // Arrange
        var entity = _fixture.Create<TodoEntity>();
        await DbContext.Todos.AddRangeAsync(entity);
        await DbContext.SaveChangesAsync();

        Guid todoId = TestIds.NonUsageId;
        var model = TodoRequestModelBuilder.CreateValid();
        var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

        // Act
        var response = await Client.PutAsync(EndpointName + todoId, content);

        // Assert
        _ = response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    /// <summary>
    /// Happy case test for endpoint on deleting one Todo.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task Delete_One_OK()
    {
        // Arrange
        var entity = _fixture.Create<TodoEntity>();
        await DbContext.Todos.AddRangeAsync(entity);
        await DbContext.SaveChangesAsync();

        // Act
        var response = await Client.DeleteAsync(EndpointName + entity.Id);

        // Assert
        _ = response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        var result = await DbContext.Todos.FirstOrDefaultAsync(c => c.Id == entity.Id);
        _ = result.Should().BeNull();
    }

    /// <summary>
    /// Unhappy case test for endpoint on deleting one Todo.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task Delete_One_NotFound()
    {
        // Arrange
        var entity = _fixture.Create<TodoEntity>();
        await DbContext.Todos.AddRangeAsync(entity);
        await DbContext.SaveChangesAsync();
        Guid todoId = TestIds.NonUsageId;

        // Act
        var response = await Client.DeleteAsync(EndpointName + todoId);

        // Assert
        _ = response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}