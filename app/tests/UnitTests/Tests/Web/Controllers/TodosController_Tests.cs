using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MyWebAPITemplate.Source.Core.Dtos;
using MyWebAPITemplate.Source.Core.Interfaces.InternalServices;
using MyWebAPITemplate.Source.Web.Controllers;
using MyWebAPITemplate.Source.Web.Interfaces.Mappers;
using MyWebAPITemplate.Source.Web.Models.RequestModels;
using MyWebAPITemplate.Source.Web.Models.ResponseModels;
using MyWebAPITemplate.Tests.UnitTests.Utils;
using Xunit;

namespace MyWebAPITemplate.Tests.UnitTests.Tests.Web.Controllers;

/// <summary>
/// All the TodosController tests.
/// </summary>
public class TodosController_Tests
{
    #region Get all

    /// <summary>
    /// Test for happy case.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task Get_All_Is_Ok()
    {
        // Arrange
        var mockTodoService = new Mock<ITodoService>();
        var mockTodoMapper = new Mock<ITodoModelDtoMapper>();

        _ = mockTodoService.Setup(s => s.GetTodos())
            .ReturnsAsync(new List<TodoDto> { new TodoDto() }.AsEnumerable());
        _ = mockTodoMapper.Setup(s => s.Map(It.IsAny<List<TodoDto>>()))
            .Returns(new List<TodoResponseModel> { new TodoResponseModel() }.AsEnumerable());

        var controller = new TodosController(mockTodoService.Object, mockTodoMapper.Object);

        // Act
        var result = await controller.Get();

        // Assert
        _ = result.Result.Should().BeOfType<OkObjectResult>();
        var resultObject = result.GetObjectResult();
        _ = resultObject.Should().BeOfType<List<TodoResponseModel>>();
        _ = resultObject.Should().HaveCount(1);

        mockTodoService.Verify(c => c.GetTodos(), Times.Once);
        mockTodoMapper.Verify(c => c.Map(It.IsAny<List<TodoDto>>()), Times.Once);
    }

    #endregion Get all

    #region Get

    /// <summary>
    /// Test for happy case.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task Get_Is_Ok()
    {
        // Arrange
        var mockTodoService = new Mock<ITodoService>();
        var mockTodoMapper = new Mock<ITodoModelDtoMapper>();

        _ = mockTodoService.Setup(s => s.GetTodo(It.IsAny<Guid>()))
            .ReturnsAsync(new TodoDto());
        _ = mockTodoMapper.Setup(s => s.Map(It.IsAny<TodoDto>()))
            .Returns(new TodoResponseModel());

        var controller = new TodosController(mockTodoService.Object, mockTodoMapper.Object);

        // Act
        var result = await controller.Get(It.IsAny<Guid>());

        // Assert
        _ = result.Result.Should().BeOfType<OkObjectResult>();
        var resultObject = result.GetObjectResult();
        _ = resultObject.Should().BeOfType<TodoResponseModel>();
        _ = resultObject.Should().NotBeNull();
        mockTodoService.Verify(c => c.GetTodo(It.IsAny<Guid>()), Times.Once);
        mockTodoMapper.Verify(c => c.Map(It.IsAny<TodoDto>()), Times.Once);
    }

    #endregion Get

    #region Create

    /// <summary>
    /// Test for happy case.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task Create_Is_Ok()
    {
        // Arrange
        var mockTodoService = new Mock<ITodoService>();
        var mockTodoMapper = new Mock<ITodoModelDtoMapper>();

        _ = mockTodoService.Setup(s => s.CreateTodo(It.IsAny<TodoDto>()))
            .ReturnsAsync(new TodoDto());
        _ = mockTodoMapper.Setup(s => s.Map(It.IsAny<TodoRequestModel>()))
            .Returns(new TodoDto());
        _ = mockTodoMapper.Setup(s => s.Map(It.IsAny<TodoDto>()))
            .Returns(new TodoResponseModel());

        var controller = new TodosController(mockTodoService.Object, mockTodoMapper.Object);

        // Act
        var result = await controller.Create(It.IsAny<TodoRequestModel>());

        // Assert
        _ = result.Result.Should().BeOfType<OkObjectResult>();
        var resultObject = result.GetObjectResult();
        _ = resultObject.Should().BeOfType<TodoResponseModel>();
        _ = resultObject.Should().NotBeNull();
        mockTodoMapper.Verify(c => c.Map(It.IsAny<TodoRequestModel>()), Times.Once);
        mockTodoService.Verify(c => c.CreateTodo(It.IsAny<TodoDto>()), Times.Once);
        mockTodoMapper.Verify(c => c.Map(It.IsAny<TodoDto>()), Times.Once);
    }

    #endregion Create

    #region Update

    /// <summary>
    /// Test for happy case.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task Update_Is_Ok()
    {
        // Arrange
        var mockTodoService = new Mock<ITodoService>();
        var mockTodoMapper = new Mock<ITodoModelDtoMapper>();

        _ = mockTodoService.Setup(s => s.UpdateTodo(It.IsAny<Guid>(), It.IsAny<TodoDto>()))
            .ReturnsAsync(new TodoDto());
        _ = mockTodoMapper.Setup(s => s.Map(It.IsAny<TodoRequestModel>()))
            .Returns(new TodoDto());
        _ = mockTodoMapper.Setup(s => s.Map(It.IsAny<TodoDto>()))
            .Returns(new TodoResponseModel());

        var controller = new TodosController(mockTodoService.Object, mockTodoMapper.Object);

        // Act
        var result = await controller.Update(It.IsAny<Guid>(), It.IsAny<TodoRequestModel>());

        // Assert
        _ = result.Result.Should().BeOfType<OkObjectResult>();
        var resultObject = result.GetObjectResult();
        _ = resultObject.Should().BeOfType<TodoResponseModel>();
        _ = resultObject.Should().NotBeNull();
        mockTodoMapper.Verify(c => c.Map(It.IsAny<TodoRequestModel>()), Times.Once);
        mockTodoService.Verify(c => c.UpdateTodo(It.IsAny<Guid>(), It.IsAny<TodoDto>()), Times.Once);
        mockTodoMapper.Verify(c => c.Map(It.IsAny<TodoDto>()), Times.Once);
    }

    #endregion Update

    #region Delete

    /// <summary>
    /// Test for happy case.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task Delete_Is_Ok()
    {
        // Arrange
        var mockTodoService = new Mock<ITodoService>();
        var mockTodoMapper = new Mock<ITodoModelDtoMapper>();

        _ = mockTodoService.Setup(s => s.DeleteTodo(It.IsAny<Guid>())).Returns(Task.CompletedTask);

        var controller = new TodosController(mockTodoService.Object, mockTodoMapper.Object);

        // Act
        var result = await controller.Delete(It.IsAny<Guid>());

        // Assert
        _ = result.Should().BeOfType<NoContentResult>();
        mockTodoService.Verify(c => c.DeleteTodo(It.IsAny<Guid>()), Times.Once);
    }

    #endregion Delete
}