using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using MyWebAPITemplate.Core.Dtos;
using MyWebAPITemplate.UnitTests.Builders.Dtos;
using MyWebAPITemplate.UnitTests.Builders.Models;
using MyWebAPITemplate.Web.Converters;
using MyWebAPITemplate.Web.Models.RequestModels;
using Xunit;

namespace MyWebAPITemplate.UnitTests.Web.Converters
{
    public class TodoModelDtoConverter_Tests
    {

        [Fact]
        public void ModelToDto_Valid()
        {
            // 1.
            var converter = new TodoModelDtoConverter();
            TodoRequestModel model = TodoRequestModelBuilder.CreateValid();
            
            // 2.
            TodoDto result = converter.Convert(model);

            // 3.
            result.Should().BeEquivalentTo(model);
            result.Id.Should().Be(null);

        }
        
    }
}
