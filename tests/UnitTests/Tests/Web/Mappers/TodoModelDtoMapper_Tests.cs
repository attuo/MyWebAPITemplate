using System.Collections.Generic;
using FluentAssertions;
using MyWebAPITemplate.Models.ResponseModels;
using MyWebAPITemplate.Source.Core.Dtos;
using MyWebAPITemplate.Source.Web.Mappers;
using MyWebAPITemplate.Source.Web.Models.RequestModels;
using MyWebAPITemplate.Tests.Shared.Builders.Dtos;
using MyWebAPITemplate.Tests.Shared.Builders.Models;
using MyWebAPITemplate.Tests.UnitTests.Shared.Ids;
using Xunit;

namespace MyWebAPITemplate.Tests.UnitTests.Web.Mappers
{
    /// <summary>
    /// All the TodoModelDtoMapper tests
    /// </summary>
    public class TodoModelDtoMapper_Tests
    {

        #region RequestModel -> TodoDto

        [Fact]
        public void Map_RequestModelToDto_Is_Valid()
        {
            // Arrange
            var mapper = new TodoModelDtoMapper();
            TodoRequestModel model = TodoRequestModelBuilder.CreateValid();

            // Act
            TodoDto result = mapper.Map(model);

            // Assert
            result.Should().BeEquivalentTo(model);
            result.Id.Should().BeNull();
        }

        [Fact]
        public void Map_RequestModelToDto_Is_Null()
        {
            // Arrange
            var mapper = new TodoModelDtoMapper();
            TodoRequestModel model = null;

            // Act
            TodoDto result = mapper.Map(model);

            // Assert
            result.Should().BeNull();
        }

        #endregion

        #region Dto -> ResponseModel

        [Fact]
        public void Map_DtoToResponseModel_Is_Valid()
        {
            // Arrange
            var mapper = new TodoModelDtoMapper();
            TodoDto dto = TodoDtoBuilder.CreateValid(TestIds.NormalUsageId);

            // Act
            TodoResponseModel result = mapper.Map(dto);

            // Assert
            result.Should().BeEquivalentTo(dto);
        }

        [Fact]
        public void Map_DtoToResponseModel_Is_Null()
        {
            // Arrange
            var mapper = new TodoModelDtoMapper();
            TodoDto dto = null;

            // Act
            TodoResponseModel result = mapper.Map(dto);

            // Assert
            result.Should().BeNull();
        }

        #endregion

        #region List - Dto -> ResponseModel

        [Fact]
        public void Map_List_DtoToResponseModel_Is_Valid()
        {
            // Arrange
            var mapper = new TodoModelDtoMapper();
            IEnumerable<TodoDto> dtos = new List<TodoDto> { TodoDtoBuilder.CreateValid(TestIds.NormalUsageId) };

            // Act
            IEnumerable<TodoResponseModel> result = mapper.Map(dtos);

            // Assert
            result.Should().BeEquivalentTo(dtos);
        }

        [Fact]
        public void Map_List_DtoToResponseModel_Is_Null()
        {
            // Arrange
            var mapper = new TodoModelDtoMapper();
            IEnumerable<TodoDto> dtos = null;

            // Act
            IEnumerable<TodoResponseModel> result = mapper.Map(dtos);

            // Assert
            result.Should().BeNull();
        }

        #endregion

    }
}
