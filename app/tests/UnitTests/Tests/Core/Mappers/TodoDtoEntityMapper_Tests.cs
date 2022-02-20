using System;
using System.Collections.Generic;
using FluentAssertions;
using MyWebAPITemplate.Source.Core.Dtos;
using MyWebAPITemplate.Source.Core.Entities;
using MyWebAPITemplate.Source.Core.Mappers;
using MyWebAPITemplate.Tests.Shared.Builders.Dtos;
using MyWebAPITemplate.Tests.Shared.Builders.Entities;
using MyWebAPITemplate.Tests.Shared.Ids;
using Xunit;

namespace MyWebAPITemplate.Tests.UnitTests.Tests.Core.Mappers;

/// <summary>
/// All the TodoDtoEntityMapper tests
/// </summary>
public class TodoDtoEntityMapper_Tests
{
    #region Dto -> Entity

    [Fact]
    public void Map_DtoToEntity_Is_Valid()
    {
        // Arrange
        var mapper = new TodoDtoEntityMapper();
        TodoDto dto = TodoDtoBuilder.CreateValid(TestIds.NormalUsageId);

        // Act
        TodoEntity result = mapper.Map(dto);

        // Assert
        result.Should().BeEquivalentTo(dto, options => options.Excluding(o => o.Id));
        result.Id.Should().Be(Guid.Empty);
    }

    [Fact]
    public void Map_DtoToEntity_Is_Null()
    {
        // Arrange
        var mapper = new TodoDtoEntityMapper();
        TodoDto dto = null;

        // Act
        TodoEntity result = mapper.Map(dto);

        // Assert
        result.Should().BeNull();
    }

    #endregion Dto -> Entity

    #region Dto, Entity -> Entity

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
        result.Should().BeEquivalentTo(dto, options => options.Excluding(o => o.Id));
        result.Id.Should().Be(entity.Id);
    }

    [Theory]
    [MemberData(nameof(NullParameterData))]
    public void Map_EntityDtoToEntity_Is_Null(TodoDto dto, TodoEntity entity)
    {
        // Arrange
        var mapper = new TodoDtoEntityMapper();

        // Act
        TodoEntity result = mapper.Map(dto, entity);

        // Assert
        result.Should().BeNull();
    }

    #endregion Dto, Entity -> Entity

    #region Entity -> Dto

    [Fact]
    public void Map_EntityToDto_Is_Valid()
    {
        // Arrange
        var mapper = new TodoDtoEntityMapper();
        TodoEntity entity = TodoEntityBuilder.CreateValid(TestIds.NormalUsageId);

        // Act
        TodoDto result = mapper.Map(entity);

        // Assert
        result.Should().BeEquivalentTo(entity);
    }

    [Fact]
    public void Map_EntityToDto_Is_Null()
    {
        // Arrange
        var mapper = new TodoDtoEntityMapper();
        TodoEntity entity = null;

        // Act
        TodoDto result = mapper.Map(entity);

        // Assert
        result.Should().BeNull();
    }

    #endregion Entity -> Dto

    #region List - Dto -> ResponseModel

    [Fact]
    public void Map_List_EntityToDto_Is_Valid()
    {
        // Arrange
        var mapper = new TodoDtoEntityMapper();
        IReadOnlyList<TodoEntity> entities = new List<TodoEntity> { TodoEntityBuilder.CreateValid(TestIds.NormalUsageId) };

        // Act
        IEnumerable<TodoDto> result = mapper.Map(entities);

        // Assert
        result.Should().BeEquivalentTo(result);
    }

    [Fact]
    public void Map_List_EntityToDto_Is_Null()
    {
        // Arrange
        var mapper = new TodoDtoEntityMapper();
        IReadOnlyList<TodoEntity> entities = null;

        // Act
        IEnumerable<TodoDto> result = mapper.Map(entities);

        // Assert
        result.Should().BeNull();
    }

    #endregion List - Dto -> ResponseModel

    #region Parameter MemberDatas

    public static IEnumerable<object[]> NullParameterData => new List<object[]>
        {
            new object[] { TodoDtoBuilder.CreateValid(TestIds.NormalUsageId), null },
            new object[] { null, TodoEntityBuilder.CreateValid(TestIds.NormalUsageId) } ,
            new object[] { null, null },
        };

    #endregion Parameter MemberDatas
}