using System.Collections.Generic;
using FluentAssertions;
using MyWebAPITemplate.Models.ResponseModels;
using MyWebAPITemplate.Source.Core.Dtos;
using MyWebAPITemplate.Source.Web.Converters;
using MyWebAPITemplate.Source.Web.Models.RequestModels;
using MyWebAPITemplate.Tests.Shared.Builders.Dtos;
using MyWebAPITemplate.Tests.Shared.Builders.Models;
using MyWebAPITemplate.Tests.UnitTests.Shared.Ids;
using Xunit;

namespace MyWebAPITemplate.Tests.UnitTests.Web.Converters
{
    public class TodoModelDtoConverter_Tests
    {

        #region RequestModel -> TodoDto

        [Fact]
        public void Convert_RequestModelToDto_Is_Valid()
        {
            // 1.
            var converter = new TodoModelDtoConverter();
            TodoRequestModel model = TodoRequestModelBuilder.CreateValid();

            // 2.
            TodoDto result = converter.Convert(model);

            // 3.
            result.Should().BeEquivalentTo(model);
            result.Id.Should().BeNull();
        }

        [Fact]
        public void Convert_RequestModelToDto_Is_Null()
        {
            // 1.
            var converter = new TodoModelDtoConverter();
            TodoRequestModel model = null;

            // 2.
            TodoDto result = converter.Convert(model);

            // 3.
            result.Should().BeNull();
        }

        #endregion

        #region Dto -> ResponseModel

        [Fact]
        public void Convert_DtoToResponseModel_Is_Valid()
        {
            // 1.
            var converter = new TodoModelDtoConverter();
            TodoDto dto = TodoDtoBuilder.CreateValid(TestIds.NormalUsageId);

            // 2.
            TodoResponseModel result = converter.Convert(dto);

            // 3.
            result.Should().BeEquivalentTo(dto);
        }

        [Fact]
        public void Convert_DtoToResponseModel_Is_Null()
        {
            // 1.
            var converter = new TodoModelDtoConverter();
            TodoDto dto = null;

            // 2.
            TodoResponseModel result = converter.Convert(dto);

            // 3.
            result.Should().BeNull();
        }

        #endregion

        #region List - Dto -> ResponseModel

        [Fact]
        public void Convert_List_DtoToResponseModel_Is_Valid()
        {
            // 1.
            var converter = new TodoModelDtoConverter();
            IEnumerable<TodoDto> dtos = new List<TodoDto> { TodoDtoBuilder.CreateValid(TestIds.NormalUsageId) };

            // 2.
            IEnumerable<TodoResponseModel> result = converter.Convert(dtos);

            // 3.
            result.Should().BeEquivalentTo(dtos);
        }

        [Fact]
        public void Convert_List_DtoToResponseModel_Is_Null()
        {
            // 1.
            var converter = new TodoModelDtoConverter();
            IEnumerable<TodoDto> dtos = null;

            // 2.
            IEnumerable<TodoResponseModel> result = converter.Convert(dtos);

            // 3.
            result.Should().BeNull();
        }

        #endregion

    }
}
