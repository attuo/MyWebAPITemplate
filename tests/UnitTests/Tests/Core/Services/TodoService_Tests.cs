using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using MyWebAPITemplate.Source.Core.Dtos;
using MyWebAPITemplate.Source.Core.Entities;
using MyWebAPITemplate.Source.Core.Interfaces.Converters;
using MyWebAPITemplate.Source.Core.Interfaces.Database;
using MyWebAPITemplate.Source.Core.Services;
using Xunit;

namespace MyWebAPITemplate.Tests.UnitTests.Core.Services
{
    public class TodoService_Tests
    {

        #region Get All

        [Fact]
        public async Task GetTodos_Is_Ok()
        {
            // Arrange
            var mockTodoRepository = new Mock<ITodoRepository>();
            var mockTodoConverter = new Mock<ITodoDtoEntityConverter>();

            mockTodoRepository.Setup(s => s.ListAllAsync())
                .ReturnsAsync(new List<TodoEntity> { new TodoEntity() });
            mockTodoConverter.Setup(s => s.Convert(It.IsAny<List<TodoEntity>>()))
                .Returns(new List<TodoDto> { new TodoDto() });

            var service = new TodoService(mockTodoRepository.Object, mockTodoConverter.Object);

            // Act
            var result = await service.GetTodos();

            // Assert
            result.Should().BeOfType<List<TodoDto>>();
            result.Should().HaveCount(1);
            mockTodoRepository.Verify(c => c.ListAllAsync(), Times.Once);
            mockTodoConverter.Verify(c => c.Convert(It.IsAny<List<TodoEntity>>()), Times.Once);
        }

        #endregion

        #region Get

        [Fact]
        public async Task GetTodo_Is_Ok()
        {
            // Arrange
            var mockTodoRepository = new Mock<ITodoRepository>();
            var mockTodoConverter = new Mock<ITodoDtoEntityConverter>();

            mockTodoRepository.Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new TodoEntity());
            mockTodoConverter.Setup(s => s.Convert(It.IsAny<TodoEntity>()))
                .Returns(new TodoDto());

            var service = new TodoService(mockTodoRepository.Object, mockTodoConverter.Object);

            // Act
            var result = await service.GetTodo(It.IsAny<Guid>());

            // Assert
            result.Should().BeOfType<TodoDto>();
            mockTodoRepository.Verify(c => c.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            mockTodoConverter.Verify(c => c.Convert(It.IsAny<TodoEntity>()), Times.Once);
        }

        [Fact]
        public async Task GetTodo_Is_Null()
        {
            // Arrange
            var mockTodoRepository = new Mock<ITodoRepository>();
            var mockTodoConverter = new Mock<ITodoDtoEntityConverter>();

            mockTodoRepository.Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((TodoEntity)null);
            mockTodoConverter.Setup(s => s.Convert(It.IsAny<TodoEntity>()))
                .Returns(new TodoDto());

            var service = new TodoService(mockTodoRepository.Object, mockTodoConverter.Object);

            // Act
            var result = await service.GetTodo(It.IsAny<Guid>());

            // Assert
            result.Should().BeNull();
            mockTodoRepository.Verify(c => c.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            mockTodoConverter.Verify(c => c.Convert(It.IsAny<TodoEntity>()), Times.Never);
        }

        #endregion

        #region Create

        [Fact]
        public async Task CreateTodo_Is_Ok()
        {
            // Arrange
            var mockTodoRepository = new Mock<ITodoRepository>();
            var mockTodoConverter = new Mock<ITodoDtoEntityConverter>();

            mockTodoConverter.Setup(s => s.Convert(It.IsAny<TodoDto>()))
                .Returns(new TodoEntity());
            mockTodoRepository.Setup(s => s.AddAsync(It.IsAny<TodoEntity>()))
                .ReturnsAsync(new TodoEntity());
            mockTodoConverter.Setup(s => s.Convert(It.IsAny<TodoEntity>()))
                .Returns(new TodoDto());

            var service = new TodoService(mockTodoRepository.Object, mockTodoConverter.Object);

            // Act
            var result = await service.CreateTodo(It.IsAny<TodoDto>());

            // Assert
            result.Should().BeOfType<TodoDto>();
            mockTodoRepository.Verify(c => c.AddAsync(It.IsAny<TodoEntity>()), Times.Once);
            mockTodoConverter.Verify(c => c.Convert(It.IsAny<TodoDto>()), Times.Once);
            mockTodoConverter.Verify(c => c.Convert(It.IsAny<TodoEntity>()), Times.Once);
        }

        #endregion

        #region Update

        [Fact]
        public async Task UpdateTodo_Is_Ok()
        {
            // Arrange
            var mockTodoRepository = new Mock<ITodoRepository>();
            var mockTodoConverter = new Mock<ITodoDtoEntityConverter>();

            mockTodoRepository.Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new TodoEntity());
            mockTodoRepository.Setup(s => s.UpdateAsync(It.IsAny<TodoEntity>()));
            mockTodoConverter.Setup(s => s.Convert(It.IsAny<TodoDto>(), It.IsAny<TodoEntity>()))
                .Returns(new TodoEntity());
            mockTodoConverter.Setup(s => s.Convert(It.IsAny<TodoEntity>()))
                .Returns(new TodoDto());

            var service = new TodoService(mockTodoRepository.Object, mockTodoConverter.Object);

            // Act
            var result = await service.UpdateTodo(It.IsAny<Guid>(), It.IsAny<TodoDto>());

            // Assert
            result.Should().BeOfType<TodoDto>();
            mockTodoRepository.Verify(c => c.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            mockTodoConverter.Verify(c => c.Convert(It.IsAny<TodoDto>(), It.IsAny<TodoEntity>()), Times.Once);
            mockTodoRepository.Verify(c => c.UpdateAsync(It.IsAny<TodoEntity>()), Times.Once);
            mockTodoConverter.Verify(c => c.Convert(It.IsAny<TodoEntity>()), Times.Once);
        }

        [Fact]
        public async Task UpdateTodo_Is_Null()
        {
            // Arrange
            var mockTodoRepository = new Mock<ITodoRepository>();
            var mockTodoConverter = new Mock<ITodoDtoEntityConverter>();

            mockTodoRepository.Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((TodoEntity)null);
            mockTodoConverter.Setup(s => s.Convert(It.IsAny<TodoDto>()))
                .Returns(new TodoEntity());
            mockTodoConverter.Setup(s => s.Convert(It.IsAny<TodoEntity>()))
                .Returns(new TodoDto());

            var service = new TodoService(mockTodoRepository.Object, mockTodoConverter.Object);

            // Act
            var result = await service.UpdateTodo(It.IsAny<Guid>(), It.IsAny<TodoDto>());

            // Assert
            result.Should().BeNull();
            mockTodoRepository.Verify(c => c.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            mockTodoConverter.Verify(c => c.Convert(It.IsAny<TodoDto>()), Times.Never);
            mockTodoRepository.Verify(c => c.UpdateAsync(It.IsAny<TodoEntity>()), Times.Never);
            mockTodoConverter.Verify(c => c.Convert(It.IsAny<TodoEntity>()), Times.Never);
            // TODO: Add Times.Never asserts for other mocks
        }

        #endregion

        #region Delete

        [Fact]
        public async Task DeleteTodo_Is_Ok()
        {
            // Arrange
            var mockTodoRepository = new Mock<ITodoRepository>();
            var mockTodoConverter = new Mock<ITodoDtoEntityConverter>();

            mockTodoRepository.Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new TodoEntity());
            mockTodoRepository.Setup(s => s.DeleteAsync(It.IsAny<TodoEntity>()));

            var service = new TodoService(mockTodoRepository.Object, mockTodoConverter.Object);

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
            var mockTodoConverter = new Mock<ITodoDtoEntityConverter>();

            mockTodoRepository.Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((TodoEntity)null);
            mockTodoRepository.Setup(s => s.DeleteAsync(It.IsAny<TodoEntity>()));

            var service = new TodoService(mockTodoRepository.Object, mockTodoConverter.Object);

            // Act
            var result = await service.DeleteTodo(It.IsAny<Guid>());

            // Assert
            result.Should().BeNull();
            mockTodoRepository.Verify(c => c.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            mockTodoRepository.Verify(c => c.DeleteAsync(It.IsAny<TodoEntity>()), Times.Never);
        }

        #endregion
    }
}
