using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyWebAPITemplate.Core.Dtos;

namespace MyWebAPITemplate.UnitTests.Builders.Dtos
{
    public class TodoDtoBuilder
    {
        public static TodoDto CreateValid()
        {
            return new TodoDto
            {
                Id = Guid.NewGuid(),
                Description = "This is a valid todo",
                IsDone = false
            };
        }
    }
}
