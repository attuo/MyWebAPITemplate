using FluentAssertions;
using MyWebAPITemplate.Source.Core.Dtos;
using MyWebAPITemplate.Source.Web.Mappers;
using MyWebAPITemplate.Source.Web.Models.RequestModels;
using MyWebAPITemplate.Source.Web.Models.ResponseModels;
using MyWebAPITemplate.Tests.SharedComponents.Builders.Dtos;
using MyWebAPITemplate.Tests.SharedComponents.Builders.Models;
using MyWebAPITemplate.Tests.SharedComponents.Ids;
using Xunit;

namespace MyWebAPITemplate.Tests.UnitTests.Tests.Web.Mappers;

/// <summary>
/// All the TodoModelDtoMapper tests.
/// </summary>
public class TodoModelDtoMapper_Tests
{
    #region RequestModel -> TodoDto

    /// <summary>
    /// Test for mapping the valid case.
    /// </summary>
    [Fact]
    public void Map_RequestModelToDto_Is_Valid()
    {
        // Arrange
        var mapper = new TodoModelDtoMapper();
        TodoRequestModel model = TodoRequestModelBuilder.CreateValid();

        // Act
        TodoDto result = mapper.Map(model);

        // Assert
        _ = result.Should().BeEquivalentTo(model);
        _ = result.Id.Should().BeNull();
    }

    #endregion RequestModel -> TodoDto

    #region Dto -> ResponseModel

    /// <summary>
    /// Test for mapping the valid case.
    /// </summary>
    [Fact]
    public void Map_DtoToResponseModel_Is_Valid()
    {
        // Arrange
        var mapper = new TodoModelDtoMapper();
        TodoDto dto = TodoDtoBuilder.CreateValid(TestIds.NormalUsageId);

        // Act
        TodoResponseModel result = mapper.Map(dto);

        // Assert
        _ = result.Should().BeEquivalentTo(dto);
    }

    #endregion Dto -> ResponseModel

    #region List - Dto -> ResponseModel

    /// <summary>
    /// Test for mapping the valid case.
    /// </summary>
    [Fact]
    public void Map_List_DtoToResponseModel_Is_Valid()
    {
        // Arrange
        var mapper = new TodoModelDtoMapper();
        IEnumerable<TodoDto> dtos = new List<TodoDto> { TodoDtoBuilder.CreateValid(TestIds.NormalUsageId) };

        // Act
        IEnumerable<TodoResponseModel> result = mapper.Map(dtos);

        // Assert
        _ = result.Should().BeEquivalentTo(dtos);
    }

    #endregion List - Dto -> ResponseModel
}