using System.Threading.Tasks;
using FluentAssertions;
using MyWebAPITemplate.Tests.IntegrationTests.Utils;
using MyWebAPITemplate.Tests.Shared.Builders.Entities;
using MyWebAPITemplate.Tests.UnitTests.Shared.Ids;
using Xunit;

namespace MyWebAPITemplate.Tests.IntegrationTests.Tests.Infrastructure.Database;

/// <summary>
/// All EfRepository tests
/// </summary>
public class EfRepository_Tests : TestFixture
{
    [Fact]
    public async Task ListAllAsync_Ok()
    {
        // Arrange
        var repository = GetTodoRepository();
        var todo = TodoEntityBuilder.CreateValid(TestIds.NormalUsageId);
        await repository.AddAsync(todo);

        // Act
        var items = await repository.ListAllAsync();

        // Assert
        items.Should().HaveCount(1);
    }

    [Fact]
    public async Task GetByIdAsync_Ok()
    {
        // Arrange
        var repository = GetTodoRepository();
        var todo = TodoEntityBuilder.CreateValid(TestIds.NormalUsageId);
        await repository.AddAsync(todo);

        // Act
        var item = await repository.GetByIdAsync(todo.Id);

        // Assert
        item.Should().NotBeNull();
        item.Id.Should().Be(todo.Id);
    }

    [Fact]
    public async Task AddAsync_Ok()
    {
        // Arrange
        var repository = GetTodoRepository();
        var todo = TodoEntityBuilder.CreateValid(TestIds.NormalUsageId);
        await repository.AddAsync(todo);

        // Act
        var item = await repository.GetByIdAsync(todo.Id);

        // Assert
        item.Should().NotBeNull();
        item.Id.Should().Be(todo.Id);
    }

    [Fact]
    public async Task UpdateAsync_Ok()
    {
        // Arrange
        var repository = GetTodoRepository();
        var todo = TodoEntityBuilder.CreateValid(TestIds.NormalUsageId);
        await repository.AddAsync(todo);

        // Act
        todo.Description = "test1";
        await repository.UpdateAsync(todo);
        var result = await repository.GetByIdAsync(todo.Id);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(todo.Id);
        result.Description.Should().Be(todo.Description);
    }

    [Fact]
    public async Task DeleteAsync_Ok()
    {
        // Arrange
        var repository = GetTodoRepository();
        var todo = TodoEntityBuilder.CreateValid(TestIds.NormalUsageId);
        await repository.AddAsync(todo);

        // Act
        var firstResult = await repository.GetByIdAsync(todo.Id);
        await repository.DeleteAsync(todo);
        var secondResult = await repository.GetByIdAsync(todo.Id);

        // Assert
        firstResult.Should().NotBeNull();
        secondResult.Should().BeNull();
    }
}
