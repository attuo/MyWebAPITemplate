using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using MyWebAPITemplate.Source.Core.Converters;
using MyWebAPITemplate.Source.Core.Dtos;
using MyWebAPITemplate.Source.Core.Entities;
using MyWebAPITemplate.Tests.Shared.Builders.Dtos;
using MyWebAPITemplate.Tests.Shared.Builders.Entities;
using MyWebAPITemplate.Tests.UnitTests.Shared.Ids;
using Xunit;

namespace MyWebAPITemplate.Tests.UnitTests.Core.Converters
{
    public class TodoDtoEntityConverter_Tests
    {
        #region Dto -> Entity

        [Fact]
        public void Convert_DtoToEntity_Is_Valid()
        {
            // 1.
            var converter = new TodoDtoEntityConverter();
            TodoDto dto = TodoDtoBuilder.CreateValid(TestIds.NormalUsageId);

            // 2.
            TodoEntity result = converter.Convert(dto);

            // 3.
            result.Should().BeEquivalentTo(dto, options => options.Excluding(o => o.Id));
            result.Id.Should().Be(Guid.Empty);
        }

        [Fact]
        public void Convert_DtoToEntity_Is_Null()
        {
            // 1.
            var converter = new TodoDtoEntityConverter();
            TodoDto dto = null;

            // 2.
            TodoEntity result = converter.Convert(dto);

            // 3.
            result.Should().BeNull();
        }

        #endregion

        #region Dto, Entity -> Entity

        [Fact]
        public void Convert_EntityDtoToEntity_Is_Valid()
        {
            // 1.
            var converter = new TodoDtoEntityConverter();
            TodoEntity entity = TodoEntityBuilder.CreateValid(TestIds.NormalUsageId);
            TodoDto dto = TodoDtoBuilder.CreateValid(TestIds.OtherUsageId);

            // 2.
            TodoEntity result = converter.Convert(dto, entity);

            // 3.
            result.Should().BeEquivalentTo(dto, options => options.Excluding(o => o.Id));
            result.Id.Should().Be(entity.Id);
        }

        [Theory]
        [MemberData(nameof(NullParameterData))]
        public void Convert_EntityDtoToEntity_Is_Null(TodoDto dto, TodoEntity entity)
        {
            // 1.
            var converter = new TodoDtoEntityConverter();

            // 2.
            TodoEntity result = converter.Convert(dto, entity);

            // 3.
            result.Should().BeNull();
        }

        #endregion

        #region Entity -> Dto

        [Fact]
        public void Convert_EntityToDto_Is_Valid()
        {
            // 1.
            var converter = new TodoDtoEntityConverter();
            TodoEntity entity = TodoEntityBuilder.CreateValid(TestIds.NormalUsageId);

            // 2.
            TodoDto result = converter.Convert(entity);

            // 3.
            result.Should().BeEquivalentTo(entity);
        }

        [Fact]
        public void Convert_EntityToDto_Is_Null()
        {
            // 1.
            var converter = new TodoDtoEntityConverter();
            TodoEntity entity = null;

            // 2.
            TodoDto result = converter.Convert(entity);

            // 3.
            result.Should().BeNull();
        }

        #endregion

        #region List - Dto -> ResponseModel

        [Fact]
        public void Convert_List_EntityToDto_Is_Valid()
        {
            // 1.
            var converter = new TodoDtoEntityConverter();
            IReadOnlyList<TodoEntity> entities = new List<TodoEntity> { TodoEntityBuilder.CreateValid(TestIds.NormalUsageId) };

            // 2.
            IEnumerable<TodoDto> result = converter.Convert(entities);

            // 3.
            result.Should().BeEquivalentTo(result);
        }

        [Fact]
        public void Convert_List_EntityToDto_Is_Null()
        {
            // 1.
            var converter = new TodoDtoEntityConverter();
            IReadOnlyList<TodoEntity> entities = null;

            // 2.
            IEnumerable<TodoDto> result = converter.Convert(entities);

            // 3.
            result.Should().BeNull();
        }

        #endregion

        #region Parameter MemberDatas

        public static IEnumerable<object[]> NullParameterData => new List<object[]>
        {
            new object[] { TodoDtoBuilder.CreateValid(TestIds.NormalUsageId), null },
            new object[] { null, TodoEntityBuilder.CreateValid(TestIds.NormalUsageId) } ,
            new object[] { null, null },
        };

        #endregion

    }
}
