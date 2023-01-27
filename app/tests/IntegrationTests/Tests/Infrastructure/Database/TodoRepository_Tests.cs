using AutoFixture;
using FluentAssertions;
using MyWebAPITemplate.Source.Core.Entities;
using MyWebAPITemplate.Source.Core.Exceptions;
using MyWebAPITemplate.Source.Infrastructure.Database;
using MyWebAPITemplate.Source.Infrastructure.Database.Repositories;
using MyWebAPITemplate.Tests.FunctionalTests.Tests.Endpoint;
using MyWebAPITemplate.Tests.SharedComponents.Builders.Entities;
using MyWebAPITemplate.Tests.SharedComponents.Factories;
using MyWebAPITemplate.Tests.SharedComponents.Ids;
using Xunit;

namespace MyWebAPITemplate.Tests.IntegrationTests.Tests.Infrastructure.Database;

/// <summary>
/// All TodoRepository tests.
/// </summary>
[Collection("Test collection")]
public class TodoRepository_Tests : DatabaseTestsBase
{
    private readonly Fixture _fixture = new();
    private readonly TodoRepository _sut;

    /// <summary>
    /// Initializes a new instance of the <see cref="TodoRepository_Tests"/> class.
    /// </summary>
    /// <param name="factory">See <see cref="InitializationFactory"/>.</param>
    public TodoRepository_Tests(InitializationFactory factory) : base(factory)
    {
        _sut = new TodoRepository(DbContext);
    }

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
        var entities = _fixture.CreateMany<TodoEntity>(2);
        await DbContext.Todos.AddRangeAsync(entities);
        await DbContext.SaveChangesAsync();

        // Act
        var items = await _sut.ListAllAsync();

        // Assert
        _ = items.Should().HaveCount(2);
    }

    /// <summary>
    /// Tests happy path for getting the existing todo from database.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task GetByIdAsync_Ok()
    {
        // Arrange
        var entity = _fixture.Create<TodoEntity>();
        await DbContext.Todos.AddRangeAsync(entity);
        await DbContext.SaveChangesAsync();

        // Act
        var item = await _sut.GetByIdAsync(entity.Id);

        // Assert
        _ = item.Should().NotBeNull();
        _ = item.Id.Should().Be(entity.Id);
    }

    /// <summary>
    /// Tests unhappy path for getting the existing todo from database.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task GetByIdAsync_Throws_EntityNotFoundException()
    {
        // Arrange
        var notFoundId = TestIds.NonUsageId;

        // Act
        Func<Task> act = async () => await _sut.GetByIdAsync(notFoundId);

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
        var entity = _fixture.Create<TodoEntity>();

        // Act
        var item = await _sut.AddAsync(entity);

        // Assert
        _ = item.Should().NotBeNull();
        _ = item.Id.Should().Be(item.Id);
        var itemResult = DbContext.Todos.FirstOrDefault(c => c.Id == item.Id);
        _ = itemResult.Should().NotBeNull();
    }

    /// <summary>
    /// Tests happy path for updating the todo in the database.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task UpdateAsync_Ok()
    {
        // Arrange
        var entity = _fixture.Create<TodoEntity>();
        await DbContext.Todos.AddRangeAsync(entity);
        await DbContext.SaveChangesAsync();

        // Act
        entity.Description = _fixture.Create<string>();
        await _sut.UpdateAsync(entity);
        var result = await _sut.GetByIdAsync(entity.Id);

        // Assert
        _ = result.Should().NotBeNull();
        _ = result.Id.Should().Be(entity.Id);
        _ = result.Description.Should().Be(entity.Description);
    }

    /// <summary>
    /// Tests happy path for deleting the todo from database.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task DeleteAsync_Ok()
    {
        // Arrange
        var entity = _fixture.Create<TodoEntity>();
        await DbContext.Todos.AddRangeAsync(entity);
        await DbContext.SaveChangesAsync();

        // Act
        var firstResult = await _sut.GetByIdAsync(entity.Id);
        await _sut.DeleteAsync(entity);
        Func<Task> secondResult = async () => await _sut.GetByIdAsync(entity.Id);

        // Assert
        _ = firstResult.Should().NotBeNull();
        _ = await secondResult.Should().ThrowAsync<EntityNotFoundException>().WithMessage($"*{entity.Id}*");
    }

    // TODO: Add tests for non existing entities for updating and deleting.
}