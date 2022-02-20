using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
/// All the TodoService tests
/// </summary>
public class TodoService_Tests
{
    #region Get All

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

    [Fact]
    public async Task GetTodo_Is_Null()
    {
        // Arrange
        var mockTodoRepository = new Mock<ITodoRepository>();
        var mockTodoMapper = new Mock<ITodoDtoEntityMapper>();

        mockTodoRepository.Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((TodoEntity)null);
        mockTodoMapper.Setup(s => s.Map(It.IsAny<TodoEntity>()))
            .Returns(new TodoDto());

        var service = new TodoService(mockTodoRepository.Object, mockTodoMapper.Object);

        // Act
        var result = await service.GetTodo(It.IsAny<Guid>());

        // Assert
        result.Should().BeNull();
        mockTodoRepository.Verify(c => c.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        mockTodoMapper.Verify(c => c.Map(It.IsAny<TodoEntity>()), Times.Never);
    }

    #endregion Get

    #region Create

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

    [Fact]
    public async Task UpdateTodo_Is_Null()
    {
        // Arrange
        var mockTodoRepository = new Mock<ITodoRepository>();
        var mockTodoMapper = new Mock<ITodoDtoEntityMapper>();

        mockTodoRepository.Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((TodoEntity)null);
        mockTodoMapper.Setup(s => s.Map(It.IsAny<TodoDto>()))
            .Returns(new TodoEntity());
        mockTodoMapper.Setup(s => s.Map(It.IsAny<TodoEntity>()))
            .Returns(new TodoDto());

        var service = new TodoService(mockTodoRepository.Object, mockTodoMapper.Object);

        // Act
        var result = await service.UpdateTodo(It.IsAny<Guid>(), It.IsAny<TodoDto>());

        // Assert
        result.Should().BeNull();
        mockTodoRepository.Verify(c => c.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        mockTodoMapper.Verify(c => c.Map(It.IsAny<TodoDto>()), Times.Never);
        mockTodoRepository.Verify(c => c.UpdateAsync(It.IsAny<TodoEntity>()), Times.Never);
        mockTodoMapper.Verify(c => c.Map(It.IsAny<TodoEntity>()), Times.Never);
        // TODO: Add Times.Never asserts for other mocks
    }

    #endregion Update

    #region Delete

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
        var result = await service.DeleteTodo(It.IsAny<Guid>());

        // Assert
        result.Should().Be(true);
        mockTodoRepository.Verify(c => c.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        mockTodoRepository.Verify(c => c.DeleteAsync(It.IsAny<TodoEntity>()), Times.Once);
    }

    [Fact]
    public async Task DeleteTodo_Is_Null()
    {
        // Arrange
        var mockTodoRepository = new Mock<ITodoRepository>();
        var mockTodoMapper = new Mock<ITodoDtoEntityMapper>();

        mockTodoRepository.Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((TodoEntity)null);
        mockTodoRepository.Setup(s => s.DeleteAsync(It.IsAny<TodoEntity>()));

        var service = new TodoService(mockTodoRepository.Object, mockTodoMapper.Object);

        // Act
        var result = await service.DeleteTodo(It.IsAny<Guid>());

        // Assert
        result.Should().BeNull();
        mockTodoRepository.Verify(c => c.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        mockTodoRepository.Verify(c => c.DeleteAsync(It.IsAny<TodoEntity>()), Times.Never);
    }

    #endregion Delete
}