using FluentAssertions;
using MyWebAPITemplate.Source.Core.Exceptions;
using MyWebAPITemplate.Tests.IntegrationTests.Utils;
using MyWebAPITemplate.Tests.SharedComponents.Builders.Entities;
using MyWebAPITemplate.Tests.SharedComponents.Ids;
using Xunit;

namespace MyWebAPITemplate.Tests.IntegrationTests.Tests.Infrastructure.Database;

/// <summary>
/// All TodoRepository tests.
/// </summary>
public class TodoRepository_Tests : TestFixture
{
    // TODO: Split these into separate classes to test each method in one file with happy and unhappy cases.
    // TODO: Share the repository instance in multiple tests.

    /// <summary>
    /// Tests happy path for listing all the todos from database.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task ListAllAsync_Ok()
    {
        // Arrange
        var repository = GetTodoRepository();
        var todo = TodoEntityBuilder.CreateValid(TestIds.NormalUsageId);
        _ = await repository.AddAsync(todo);

        // Act
        var items = await repository.ListAllAsync();

        // Assert
        _ = items.Should().HaveCount(1);
    }

    /// <summary>
    /// Tests happy path for getting the existing todo from database.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task GetByIdAsync_Ok()
    {
        // Arrange
        var repository = GetTodoRepository();
        var todo = TodoEntityBuilder.CreateValid(TestIds.NormalUsageId);
        _ = await repository.AddAsync(todo);

        // Act
        var item = await repository.GetByIdAsync(todo.Id);

        // Assert
        _ = item.Should().NotBeNull();
        _ = item.Id.Should().Be(todo.Id);
    }

    /// <summary>
    /// Tests unhappy path for getting the existing todo from database.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task GetByIdAsync_Throws_EntityNotFoundException()
    {
        // Arrange
        var repository = GetTodoRepository();
        var notFoundId = TestIds.NonUsageId;

        // Act
        Func<Task> act = async () => await repository.GetByIdAsync(notFoundId);

        // Assert
        _ = await act.Should().ThrowAsync<EntityNotFoundException>().WithMessage($"*{notFoundId}*");
    }

    /// <summary>
    /// Tests happy path for adding the todo to database.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task AddAsync_Ok()
    {
        // Arrange
        var repository = GetTodoRepository();
        var todo = TodoEntityBuilder.CreateValid(TestIds.NormalUsageId);
        _ = await repository.AddAsync(todo);

        // Act
        var item = await repository.GetByIdAsync(todo.Id);

        // Assert
        _ = item.Should().NotBeNull();
        _ = item.Id.Should().Be(todo.Id);
    }

    /// <summary>
    /// Tests happy path for updating the todo in the database.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task UpdateAsync_Ok()
    {
        // Arrange
        var repository = GetTodoRepository();
        var todo = TodoEntityBuilder.CreateValid(TestIds.NormalUsageId);
        _ = await repository.AddAsync(todo);

        // Act
        todo.Description = "test1";
        await repository.UpdateAsync(todo);
        var result = await repository.GetByIdAsync(todo.Id);

        // Assert
        _ = result.Should().NotBeNull();
        _ = result.Id.Should().Be(todo.Id);
        _ = result.Description.Should().Be(todo.Description);
    }

    /// <summary>
    /// Tests happy path for deleting the todo from database.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task DeleteAsync_Ok()
    {
        // Arrange
        var repository = GetTodoRepository();
        var todo = TodoEntityBuilder.CreateValid(TestIds.NormalUsageId);
        _ = await repository.AddAsync(todo);

        // Act
        var firstResult = await repository.GetByIdAsync(todo.Id);
        await repository.DeleteAsync(todo);
        Func<Task> secondResult = async () => await repository.GetByIdAsync(todo.Id);

        // Assert
        _ = firstResult.Should().NotBeNull();
        _ = await secondResult.Should().ThrowAsync<EntityNotFoundException>().WithMessage($"*{todo.Id}*");
    }

    // TODO: Add tests for non existing entities for updating and deleting.
}