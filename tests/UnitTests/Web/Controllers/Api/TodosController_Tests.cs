using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MyWebAPITemplate.Controllers.Api;
using MyWebAPITemplate.Core.Dtos;
using MyWebAPITemplate.Core.Interfaces.InternalServices;
using MyWebAPITemplate.Models.ResponseModels;
using MyWebAPITemplate.Web.Interfaces;
using Xunit;
using MyWebAPITemplate.UnitTests.Utils;
using MyWebAPITemplate.Web.Models.RequestModels;

namespace MyWebAPITemplate.UnitTests.Web.Controllers.Api
{
    public class TodosController_Tests
    {
        #region Get all

        [Fact]
        public async Task Get_All_Is_Ok()
        {
            // Arrange
            var mockTodoService = new Mock<ITodoService>();
            var mockTodoConverter = new Mock<ITodoModelDtoConverter>();

            mockTodoService.Setup(s => s.GetTodos())
                .ReturnsAsync(new List<TodoDto> { new TodoDto() }.AsEnumerable());
            mockTodoConverter.Setup(s => s.Convert(It.IsAny<List<TodoDto>>()))
                .Returns(new List<TodoResponseModel> { new TodoResponseModel() }.AsEnumerable());

            var controller = new TodosController(mockTodoService.Object, mockTodoConverter.Object);

            // Act
            var result = await controller.Get();

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var resultObject = result.GetObjectResult();
            resultObject.Should().BeOfType<List<TodoResponseModel>>();
            resultObject.Should().HaveCount(1);

            mockTodoService.Verify(c => c.GetTodos(), Times.Once);
            mockTodoConverter.Verify(c => c.Convert(It.IsAny<List<TodoDto>>()), Times.Once);
        }

        #endregion

        #region Get

        [Fact]
        public async Task Get_Is_Ok()
        {
            // Arrange
            var mockTodoService = new Mock<ITodoService>();
            var mockTodoConverter = new Mock<ITodoModelDtoConverter>();

            mockTodoService.Setup(s => s.GetTodo(It.IsAny<Guid>()))
                .ReturnsAsync(new TodoDto());
            mockTodoConverter.Setup(s => s.Convert(It.IsAny<TodoDto>()))
                .Returns(new TodoResponseModel());

            var controller = new TodosController(mockTodoService.Object, mockTodoConverter.Object);

            // Act
            var result = await controller.Get(It.IsAny<Guid>());

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var resultObject = result.GetObjectResult();
            resultObject.Should().BeOfType<TodoResponseModel>();
            resultObject.Should().NotBeNull();
            mockTodoService.Verify(c => c.GetTodo(It.IsAny<Guid>()), Times.Once);
            mockTodoConverter.Verify(c => c.Convert(It.IsAny<TodoDto>()), Times.Once);
        }

        [Fact]
        public async Task Get_Is_NotFound()
        {
            // Arrange
            var mockTodoService = new Mock<ITodoService>();
            var mockTodoConverter = new Mock<ITodoModelDtoConverter>();

            mockTodoService.Setup(s => s.GetTodo(It.IsAny<Guid>()))
                .ReturnsAsync((TodoDto)null);
            mockTodoConverter.Setup(s => s.Convert(It.IsAny<TodoDto>()))
                .Returns(new TodoResponseModel());

            var controller = new TodosController(mockTodoService.Object, mockTodoConverter.Object);

            // Act
            var result = await controller.Get(It.IsAny<Guid>());

            // Assert
            result.Result.Should().BeOfType<NotFoundObjectResult>();
            mockTodoService.Verify(c => c.GetTodo(It.IsAny<Guid>()), Times.Once);
            mockTodoConverter.Verify(c => c.Convert(It.IsAny<TodoDto>()), Times.Never);
        }

        #endregion

        #region Create

        [Fact]
        public async Task Post_Is_Ok()
        {
            // Arrange
            var mockTodoService = new Mock<ITodoService>();
            var mockTodoConverter = new Mock<ITodoModelDtoConverter>();

            mockTodoService.Setup(s => s.CreateTodo(It.IsAny<TodoDto>()))
                .ReturnsAsync(new TodoDto());
            mockTodoConverter.Setup(s => s.Convert(It.IsAny<TodoRequestModel>()))
                .Returns(new TodoDto());
            mockTodoConverter.Setup(s => s.Convert(It.IsAny<TodoDto>()))
                .Returns(new TodoResponseModel());

            var controller = new TodosController(mockTodoService.Object, mockTodoConverter.Object);

            // Act
            var result = await controller.Post(It.IsAny<TodoRequestModel>());

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var resultObject = result.GetObjectResult();
            resultObject.Should().BeOfType<TodoResponseModel>();
            resultObject.Should().NotBeNull();
            mockTodoConverter.Verify(c => c.Convert(It.IsAny<TodoRequestModel>()), Times.Once);
            mockTodoService.Verify(c => c.CreateTodo(It.IsAny<TodoDto>()), Times.Once);
            mockTodoConverter.Verify(c => c.Convert(It.IsAny<TodoDto>()), Times.Once);
        }

        #endregion

        #region Update

        [Fact]
        public async Task Put_Is_Ok()
        {
            // Arrange
            var mockTodoService = new Mock<ITodoService>();
            var mockTodoConverter = new Mock<ITodoModelDtoConverter>();

            mockTodoService.Setup(s => s.UpdateTodo(It.IsAny<Guid>(), It.IsAny<TodoDto>()))
                .ReturnsAsync(new TodoDto());
            mockTodoConverter.Setup(s => s.Convert(It.IsAny<TodoRequestModel>()))
                .Returns(new TodoDto());
            mockTodoConverter.Setup(s => s.Convert(It.IsAny<TodoDto>()))
                .Returns(new TodoResponseModel());

            var controller = new TodosController(mockTodoService.Object, mockTodoConverter.Object);

            // Act
            var result = await controller.Put(It.IsAny<Guid>(), It.IsAny<TodoRequestModel>());

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var resultObject = result.GetObjectResult();
            resultObject.Should().BeOfType<TodoResponseModel>();
            resultObject.Should().NotBeNull();
            mockTodoConverter.Verify(c => c.Convert(It.IsAny<TodoRequestModel>()), Times.Once);
            mockTodoService.Verify(c => c.UpdateTodo(It.IsAny<Guid>(), It.IsAny<TodoDto>()), Times.Once); ;
            mockTodoConverter.Verify(c => c.Convert(It.IsAny<TodoDto>()), Times.Once);
        }

        [Fact]
        public async Task Put_Is_NotFound()
        {
            // Arrange
            var mockTodoService = new Mock<ITodoService>();
            var mockTodoConverter = new Mock<ITodoModelDtoConverter>();

            mockTodoService.Setup(s => s.UpdateTodo(It.IsAny<Guid>(), It.IsAny<TodoDto>()))
                .ReturnsAsync((TodoDto)null);
            mockTodoConverter.Setup(s => s.Convert(It.IsAny<TodoRequestModel>()))
                .Returns(new TodoDto());
            mockTodoConverter.Setup(s => s.Convert(It.IsAny<TodoDto>()))
                .Returns(new TodoResponseModel());

            var controller = new TodosController(mockTodoService.Object, mockTodoConverter.Object);

            // Act
            var result = await controller.Put(It.IsAny<Guid>(), It.IsAny<TodoRequestModel>());

            // Assert
            result.Result.Should().BeOfType<NotFoundObjectResult>();
            mockTodoConverter.Verify(c => c.Convert(It.IsAny<TodoRequestModel>()), Times.Once);
            mockTodoService.Verify(c => c.UpdateTodo(It.IsAny<Guid>(), It.IsAny<TodoDto>()), Times.Once); ;
            mockTodoConverter.Verify(c => c.Convert(It.IsAny<TodoDto>()), Times.Never);
        }

        #endregion

        #region Delete

        [Fact]
        public async Task Delete_Is_Ok()
        {
            // Arrange
            var mockTodoService = new Mock<ITodoService>();
            var mockTodoConverter = new Mock<ITodoModelDtoConverter>();

            mockTodoService.Setup(s => s.DeleteTodo(It.IsAny<Guid>()))
                .ReturnsAsync(true);

            var controller = new TodosController(mockTodoService.Object, mockTodoConverter.Object);

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
            var mockTodoConverter = new Mock<ITodoModelDtoConverter>();

            mockTodoService.Setup(s => s.UpdateTodo(It.IsAny<Guid>(), It.IsAny<TodoDto>()))
                .ReturnsAsync(() => null);

            var controller = new TodosController(mockTodoService.Object, mockTodoConverter.Object);

            // Act
            var result = await controller.Delete(It.IsAny<Guid>());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            mockTodoService.Verify(c => c.DeleteTodo(It.IsAny<Guid>()), Times.Once);
        }

        #endregion
    }
}
