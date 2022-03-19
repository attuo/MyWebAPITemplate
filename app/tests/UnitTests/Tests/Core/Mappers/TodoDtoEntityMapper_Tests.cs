using FluentAssertions;
using MyWebAPITemplate.Source.Core.Dtos;
using MyWebAPITemplate.Source.Core.Entities;
using MyWebAPITemplate.Source.Core.Mappers;
using MyWebAPITemplate.Tests.SharedComponents.Builders.Dtos;
using MyWebAPITemplate.Tests.SharedComponents.Builders.Entities;
using MyWebAPITemplate.Tests.SharedComponents.Ids;
using Xunit;

namespace MyWebAPITemplate.Tests.UnitTests.Tests.Core.Mappers;

/// <summary>
/// All the TodoDtoEntityMapper tests.
/// </summary>
public class TodoDtoEntityMapper_Tests
{
    #region Dto -> Entity

    /// <summary>
    /// Test for mapping the valid case.
    /// </summary>
    [Fact]
    public void Map_DtoToEntity_Is_Valid()
    {
        // Arrange
        var mapper = new TodoDtoEntityMapper();
        TodoDto dto = TodoDtoBuilder.CreateValid(TestIds.NormalUsageId);

        // Act
        TodoEntity result = mapper.Map(dto);

        // Assert
        _ = result.Should().BeEquivalentTo(dto, options => options.Excluding(o => o.Id));
        _ = result.Id.Should().Be(Guid.Empty);
    }

    #endregion Dto -> Entity

    #region Dto, Entity -> Entity

    /// <summary>
    /// Test for mapping the valid case.
    /// </summary>
    [Fact]
    public void Map_EntityDtoToEntity_Is_Valid()
    {
        // Arrange
        var mapper = new TodoDtoEntityMapper();
        TodoEntity entity = TodoEntityBuilder.CreateValid(TestIds.NormalUsageId);
        TodoDto dto = TodoDtoBuilder.CreateValid(TestIds.OtherUsageId);

        // Act
        TodoEntity result = mapper.Map(dto, entity);

        // Assert
        _ = result.Should().BeEquivalentTo(dto, options => options.Excluding(o => o.Id));
        _ = result.Id.Should().Be(entity.Id);
    }

    #endregion Dto, Entity -> Entity

    #region Entity -> Dto

    /// <summary>
    /// Test for mapping the valid case.
    /// </summary>
    [Fact]
    public void Map_EntityToDto_Is_Valid()
    {
        // Arrange
        var mapper = new TodoDtoEntityMapper();
        TodoEntity entity = TodoEntityBuilder.CreateValid(TestIds.NormalUsageId);

        // Act
        TodoDto result = mapper.Map(entity);

        // Assert
        _ = result.Should().BeEquivalentTo(entity);
    }

    #endregion Entity -> Dto

    #region List - Dto -> ResponseModel

    /// <summary>
    /// Test for mapping the valid case.
    /// </summary>
    [Fact]
    public void Map_List_EntityToDto_Is_Valid()
    {
        // Arrange
        var mapper = new TodoDtoEntityMapper();
        IReadOnlyList<TodoEntity> entities = new List<TodoEntity> { TodoEntityBuilder.CreateValid(TestIds.NormalUsageId) };

        // Act
        IEnumerable<TodoDto> result = mapper.Map(entities);

        // Assert
        _ = result.Should().BeEquivalentTo(result);
    }

    #endregion List - Dto -> ResponseModel
}