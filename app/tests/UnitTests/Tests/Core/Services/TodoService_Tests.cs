using FluentAssertions;
using Moq;
using MyWebAPITemplate.Source.Core.Dtos;
using MyWebAPITemplate.Source.Core.Entities;
using MyWebAPITemplate.Source.Core.Interfaces.Database;
using MyWebAPITemplate.Source.Core.Interfaces.Mappers;
using MyWebAPITemplate.Source.Core.Services;
using Xunit;

namespace MyWebAPITemplate.Tests.UnitTests.Tests.Core.Services;

/// <summary>
/// All the TodoService tests.
/// </summary>
public class TodoService_Tests
{
    // TODO: Share mocking between the tests

    #region Get All

    /// <summary>
    /// Tests happy case for service method on getting all the todos.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task GetTodos_Is_Ok()
    {
        // Arrange
        var mockTodoRepository = new Mock<ITodoRepository>();
        var mockTodoMapper = new Mock<ITodoDtoEntityMapper>();

        mockTodoRepository.Setup(s => s.ListAllAsync())
            .ReturnsAsync(new List<TodoEntity> { new TodoEntity() });
        mockTodoMapper.Setup(s => s.Map(It.IsAny<List<TodoEntity>>()))
            .Returns(new List<TodoDto> { new TodoDto() });

        var service = new TodoService(mockTodoRepository.Object, mockTodoMapper.Object);

        // Act
        var result = await service.GetTodos();

        // Assert
        result.Should().BeOfType<List<TodoDto>>();
        result.Should().HaveCount(1);
        mockTodoRepository.Verify(c => c.ListAllAsync(), Times.Once);
        mockTodoMapper.Verify(c => c.Map(It.IsAny<List<TodoEntity>>()), Times.Once);
    }

    #endregion Get All

    #region Get

    /// <summary>
    /// Tests happy case for service method on getting the todo.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task GetTodo_Is_Ok()
    {
        // Arrange
        var mockTodoRepository = new Mock<ITodoRepository>();
        var mockTodoMapper = new Mock<ITodoDtoEntityMapper>();

        mockTodoRepository.Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(new TodoEntity());
        mockTodoMapper.Setup(s => s.Map(It.IsAny<TodoEntity>()))
            .Returns(new TodoDto());

        var service = new TodoService(mockTodoRepository.Object, mockTodoMapper.Object);

        // Act
        var result = await service.GetTodo(It.IsAny<Guid>());

        // Assert
        result.Should().BeOfType<TodoDto>();
        mockTodoRepository.Verify(c => c.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        mockTodoMapper.Verify(c => c.Map(It.IsAny<TodoEntity>()), Times.Once);
    }

    #endregion Get

    #region Create

    /// <summary>
    /// Tests happy case for service method on creating a todo.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task CreateTodo_Is_Ok()
    {
        // Arrange
        var mockTodoRepository = new Mock<ITodoRepository>();
        var mockTodoMapper = new Mock<ITodoDtoEntityMapper>();

        mockTodoMapper.Setup(s => s.Map(It.IsAny<TodoDto>()))
            .Returns(new TodoEntity());
        mockTodoRepository.Setup(s => s.AddAsync(It.IsAny<TodoEntity>()))
            .ReturnsAsync(new TodoEntity());
        mockTodoMapper.Setup(s => s.Map(It.IsAny<TodoEntity>()))
            .Returns(new TodoDto());

        var service = new TodoService(mockTodoRepository.Object, mockTodoMapper.Object);

        // Act
        var result = await service.CreateTodo(It.IsAny<TodoDto>());

        // Assert
        result.Should().BeOfType<TodoDto>();
        mockTodoRepository.Verify(c => c.AddAsync(It.IsAny<TodoEntity>()), Times.Once);
        mockTodoMapper.Verify(c => c.Map(It.IsAny<TodoDto>()), Times.Once);
        mockTodoMapper.Verify(c => c.Map(It.IsAny<TodoEntity>()), Times.Once);
    }

    #endregion Create

    #region Update

    /// <summary>
    /// Tests happy case for service method on updating a todo.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task UpdateTodo_Is_Ok()
    {
        // Arrange
        var mockTodoRepository = new Mock<ITodoRepository>();
        var mockTodoMapper = new Mock<ITodoDtoEntityMapper>();

        mockTodoRepository.Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(new TodoEntity());
        mockTodoRepository.Setup(s => s.UpdateAsync(It.IsAny<TodoEntity>()));
        mockTodoMapper.Setup(s => s.Map(It.IsAny<TodoDto>(), It.IsAny<TodoEntity>()))
            .Returns(new TodoEntity());
        mockTodoMapper.Setup(s => s.Map(It.IsAny<TodoEntity>()))
            .Returns(new TodoDto());

        var service = new TodoService(mockTodoRepository.Object, mockTodoMapper.Object);

        // Act
        var result = await service.UpdateTodo(It.IsAny<Guid>(), It.IsAny<TodoDto>());

        // Assert
        result.Should().BeOfType<TodoDto>();
        mockTodoRepository.Verify(c => c.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        mockTodoMapper.Verify(c => c.Map(It.IsAny<TodoDto>(), It.IsAny<TodoEntity>()), Times.Once);
        mockTodoRepository.Verify(c => c.UpdateAsync(It.IsAny<TodoEntity>()), Times.Once);
        mockTodoMapper.Verify(c => c.Map(It.IsAny<TodoEntity>()), Times.Once);
    }

    #endregion Update

    #region Delete

    /// <summary>
    /// Tests happy case for service method on deleting a todo.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task DeleteTodo_Is_Ok()
    {
        // Arrange
        var mockTodoRepository = new Mock<ITodoRepository>();
        var mockTodoMapper = new Mock<ITodoDtoEntityMapper>();

        mockTodoRepository.Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(new TodoEntity());
        mockTodoRepository.Setup(s => s.DeleteAsync(It.IsAny<TodoEntity>()));

        var service = new TodoService(mockTodoRepository.Object, mockTodoMapper.Object);

        // Act
        await service.DeleteTodo(It.IsAny<Guid>());

        // Assert
        mockTodoRepository.Verify(c => c.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        mockTodoRepository.Verify(c => c.DeleteAsync(It.IsAny<TodoEntity>()), Times.Once);
    }

    #endregion Delete
}