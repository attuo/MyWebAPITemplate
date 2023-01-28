using System.Net;
using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MyWebAPITemplate.Source.Core.Entities;
using MyWebAPITemplate.Source.Web.Models.RequestModels;
using MyWebAPITemplate.Source.Web.Models.ResponseModels;
using MyWebAPITemplate.Tests.SharedComponents.Factories;
using MyWebAPITemplate.Tests.SharedComponents.Ids;
using Xunit;

namespace MyWebAPITemplate.Tests.FunctionalTests.Tests.Endpoint;

/// <summary>
/// All the endpoint tests for Todos.
/// </summary>
public class TodoEndpoints_Tests : EndpointTestsBase
{
    private readonly Fixture _fixture = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="TodoEndpoints_Tests"/> class.
    /// </summary>
    /// <param name="factory">See <see cref="InitializationFactory"/>.</param>
    public TodoEndpoints_Tests(InitializationFactory factory) : base(factory)
    {
        HttpApiClient.ResourceName = "api/Todos";
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
        var response = await HttpApiClient.Get<List<TodoResponseModel>>();

        // Assert
        _ = response.StatusCode.Should().Be(HttpStatusCode.OK);
        // TODO: Make the tests to be independent. Currently this test also get affected by the create todo test
        _ = response.ResponseModel.Should().HaveCount(2);
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
        var response = await HttpApiClient.Get<TodoResponseModel>(entity.Id.ToString());

        // Assert
        _ = response.StatusCode.Should().Be(HttpStatusCode.OK);
        _ = response.ResponseModel.Should().NotBeNull();
        _ = response.ResponseModel!.Id.Should().Be(entity.Id);
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
        string nonExistingTodoId = TestIds.NonUsageId.ToString();

        // Act
        var response = await HttpApiClient.Get<TodoResponseModel>(nonExistingTodoId);

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
        var requestModel = _fixture.Create<TodoRequestModel>();

        // Act
        var response = await HttpApiClient.Post<TodoRequestModel, TodoResponseModel>(requestModel);

        // Assert
        _ = response.StatusCode.Should().Be(HttpStatusCode.OK);
        _ = response.ResponseModel.Should().NotBeNull();
        _ = response.ResponseModel!.Id.Should().NotBeEmpty();
        var result = await DbContext.Todos.FirstOrDefaultAsync(c => c.Id == response.ResponseModel.Id);
        _ = result.Should().NotBeNull();
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
        var requestModel = _fixture.Create<TodoRequestModel>();

        // Act
        var response = await HttpApiClient.Put<TodoRequestModel, TodoResponseModel>(requestModel, entity.Id.ToString());

        // Assert
        _ = response.StatusCode.Should().Be(HttpStatusCode.OK);
        _ = response.ResponseModel.Should().NotBeNull();
        _ = response.ResponseModel!.Description.Should().Be(requestModel.Description);
        _ = response.ResponseModel!.IsDone.Should().Be(requestModel.IsDone);
        var result = await DbContext.Todos.FirstOrDefaultAsync(c => c.Id == response.ResponseModel.Id);
        _ = result.Should().NotBeNull();
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
        string nonExistingTodoId = TestIds.NonUsageId.ToString();
        var requestModel = _fixture.Create<TodoRequestModel>();

        // Act
        var response = await HttpApiClient.Put<TodoRequestModel, TodoResponseModel>(requestModel, nonExistingTodoId);

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
        var response = await HttpApiClient.Delete(entity.Id.ToString());

        // Assert
        _ = response.Should().Be(HttpStatusCode.NoContent);
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
        string nonExistingTodoId = TestIds.NonUsageId.ToString();

        // Act
        var response = await HttpApiClient.Delete(nonExistingTodoId);

        // Assert
        _ = response.Should().Be(HttpStatusCode.NotFound);
    }
}