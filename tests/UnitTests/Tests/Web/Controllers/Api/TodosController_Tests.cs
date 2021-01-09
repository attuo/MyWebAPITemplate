using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MyWebAPITemplate.Controllers.Api;
using MyWebAPITemplate.Models.ResponseModels;
using MyWebAPITemplate.Source.Core.Dtos;
using MyWebAPITemplate.Source.Core.Interfaces.InternalServices;
using MyWebAPITemplate.Source.Web.Interfaces.Mappers;
using MyWebAPITemplate.Source.Web.Models.RequestModels;
using MyWebAPITemplate.Tests.UnitTests.Utils;
using Xunit;

namespace MyWebAPITemplate.Tests.UnitTests.Web.Controllers.Api
{
    /// <summary>
    /// All the TodosController tests
    /// </summary>
    public class TodosController_Tests
    {
        #region Get all

        [Fact]
        public async Task Get_All_Is_Ok()
        {
            // Arrange
            var mockTodoService = new Mock<ITodoService>();
            var mockTodoMapper = new Mock<ITodoModelDtoMapper>();

            mockTodoService.Setup(s => s.GetTodos())
                .ReturnsAsync(new List<TodoDto> { new TodoDto() }.AsEnumerable());
            mockTodoMapper.Setup(s => s.Map(It.IsAny<List<TodoDto>>()))
                .Returns(new List<TodoResponseModel> { new TodoResponseModel() }.AsEnumerable());

            var controller = new TodosController(mockTodoService.Object, mockTodoMapper.Object);

            // Act
            var result = await controller.Get();

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var resultObject = result.GetObjectResult();
            resultObject.Should().BeOfType<List<TodoResponseModel>>();
            resultObject.Should().HaveCount(1);

            mockTodoService.Verify(c => c.GetTodos(), Times.Once);
            mockTodoMapper.Verify(c => c.Map(It.IsAny<List<TodoDto>>()), Times.Once);
        }

        #endregion

        #region Get

        [Fact]
        public async Task Get_Is_Ok()
        {
            // Arrange
            var mockTodoService = new Mock<ITodoService>();
            var mockTodoMapper = new Mock<ITodoModelDtoMapper>();

            mockTodoService.Setup(s => s.GetTodo(It.IsAny<Guid>()))
                .ReturnsAsync(new TodoDto());
            mockTodoMapper.Setup(s => s.Map(It.IsAny<TodoDto>()))
                .Returns(new TodoResponseModel());

            var controller = new TodosController(mockTodoService.Object, mockTodoMapper.Object);

            // Act
            var result = await controller.Get(It.IsAny<Guid>());

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var resultObject = result.GetObjectResult();
            resultObject.Should().BeOfType<TodoResponseModel>();
            resultObject.Should().NotBeNull();
            mockTodoService.Verify(c => c.GetTodo(It.IsAny<Guid>()), Times.Once);
            mockTodoMapper.Verify(c => c.Map(It.IsAny<TodoDto>()), Times.Once);
        }

        [Fact]
        public async Task Get_Is_NotFound()
        {
            // Arrange
            var mockTodoService = new Mock<ITodoService>();
            var mockTodoMapper = new Mock<ITodoModelDtoMapper>();

            mockTodoService.Setup(s => s.GetTodo(It.IsAny<Guid>()))
                .ReturnsAsync((TodoDto)null);
            mockTodoMapper.Setup(s => s.Map(It.IsAny<TodoDto>()))
                .Returns(new TodoResponseModel());

            var controller = new TodosController(mockTodoService.Object, mockTodoMapper.Object);

            // Act
            var result = await controller.Get(It.IsAny<Guid>());

            // Assert
            result.Result.Should().BeOfType<NotFoundObjectResult>();
            mockTodoService.Verify(c => c.GetTodo(It.IsAny<Guid>()), Times.Once);
            mockTodoMapper.Verify(c => c.Map(It.IsAny<TodoDto>()), Times.Never);
        }

        #endregion

        #region Create

        [Fact]
        public async Task Post_Is_Ok()
        {
            // Arrange
            var mockTodoService = new Mock<ITodoService>();
            var mockTodoMapper = new Mock<ITodoModelDtoMapper>();

            mockTodoService.Setup(s => s.CreateTodo(It.IsAny<TodoDto>()))
                .ReturnsAsync(new TodoDto());
            mockTodoMapper.Setup(s => s.Map(It.IsAny<TodoRequestModel>()))
                .Returns(new TodoDto());
            mockTodoMapper.Setup(s => s.Map(It.IsAny<TodoDto>()))
                .Returns(new TodoResponseModel());

            var controller = new TodosController(mockTodoService.Object, mockTodoMapper.Object);

            // Act
            var result = await controller.Post(It.IsAny<TodoRequestModel>());

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var resultObject = result.GetObjectResult();
            resultObject.Should().BeOfType<TodoResponseModel>();
            resultObject.Should().NotBeNull();
            mockTodoMapper.Verify(c => c.Map(It.IsAny<TodoRequestModel>()), Times.Once);
            mockTodoService.Verify(c => c.CreateTodo(It.IsAny<TodoDto>()), Times.Once);
            mockTodoMapper.Verify(c => c.Map(It.IsAny<TodoDto>()), Times.Once);
        }

        #endregion

        #region Update

        [Fact]
        public async Task Put_Is_Ok()
        {
            // Arrange
            var mockTodoService = new Mock<ITodoService>();
            var mockTodoMapper = new Mock<ITodoModelDtoMapper>();

            mockTodoService.Setup(s => s.UpdateTodo(It.IsAny<Guid>(), It.IsAny<TodoDto>()))
                .ReturnsAsync(new TodoDto());
            mockTodoMapper.Setup(s => s.Map(It.IsAny<TodoRequestModel>()))
                .Returns(new TodoDto());
            mockTodoMapper.Setup(s => s.Map(It.IsAny<TodoDto>()))
                .Returns(new TodoResponseModel());

            var controller = new TodosController(mockTodoService.Object, mockTodoMapper.Object);

            // Act
            var result = await controller.Put(It.IsAny<Guid>(), It.IsAny<TodoRequestModel>());

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var resultObject = result.GetObjectResult();
            resultObject.Should().BeOfType<TodoResponseModel>();
            resultObject.Should().NotBeNull();
            mockTodoMapper.Verify(c => c.Map(It.IsAny<TodoRequestModel>()), Times.Once);
            mockTodoService.Verify(c => c.UpdateTodo(It.IsAny<Guid>(), It.IsAny<TodoDto>()), Times.Once); ;
            mockTodoMapper.Verify(c => c.Map(It.IsAny<TodoDto>()), Times.Once);
        }

        [Fact]
        public async Task Put_Is_NotFound()
        {
            // Arrange
            var mockTodoService = new Mock<ITodoService>();
            var mockTodoMapper = new Mock<ITodoModelDtoMapper>();

            mockTodoService.Setup(s => s.UpdateTodo(It.IsAny<Guid>(), It.IsAny<TodoDto>()))
                .ReturnsAsync((TodoDto)null);
            mockTodoMapper.Setup(s => s.Map(It.IsAny<TodoRequestModel>()))
                .Returns(new TodoDto());
            mockTodoMapper.Setup(s => s.Map(It.IsAny<TodoDto>()))
                .Returns(new TodoResponseModel());

            var controller = new TodosController(mockTodoService.Object, mockTodoMapper.Object);

            // Act
            var result = await controller.Put(It.IsAny<Guid>(), It.IsAny<TodoRequestModel>());

            // Assert
            result.Result.Should().BeOfType<NotFoundObjectResult>();
            mockTodoMapper.Verify(c => c.Map(It.IsAny<TodoRequestModel>()), Times.Once);
            mockTodoService.Verify(c => c.UpdateTodo(It.IsAny<Guid>(), It.IsAny<TodoDto>()), Times.Once); ;
            mockTodoMapper.Verify(c => c.Map(It.IsAny<TodoDto>()), Times.Never);
        }

        #endregion

        #region Delete

        [Fact]
        public async Task Delete_Is_Ok()
        {
            // Arrange
            var mockTodoService = new Mock<ITodoService>();
            var mockTodoMapper = new Mock<ITodoModelDtoMapper>();

            mockTodoService.Setup(s => s.DeleteTodo(It.IsAny<Guid>()))
                .ReturnsAsync(true);

            var controller = new TodosController(mockTodoService.Object, mockTodoMapper.Object);

            // Act
            var result = await controller.Delete(It.IsAny<Guid>());

            // Assert
            result.Should().BeOfType<NoContentResult>();
            mockTodoService.Verify(c => c.DeleteTodo(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task Delete_Is_NotFound()
        {
            // Arrange
            var mockTodoService = new Mock<ITodoService>();
            var mockTodoMapper = new Mock<ITodoModelDtoMapper>();

            mockTodoService.Setup(s => s.UpdateTodo(It.IsAny<Guid>(), It.IsAny<TodoDto>()))
                .ReturnsAsync(() => null);

            var controller = new TodosController(mockTodoService.Object, mockTodoMapper.Object);

            // Act
            var result = await controller.Delete(It.IsAny<Guid>());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            mockTodoService.Verify(c => c.DeleteTodo(It.IsAny<Guid>()), Times.Once);
        }

        #endregion
    }
}
