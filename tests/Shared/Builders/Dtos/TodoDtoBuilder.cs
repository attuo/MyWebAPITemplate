using System;
using MyWebAPITemplate.Source.Core.Dtos;

namespace MyWebAPITemplate.Tests.Shared.Builders.Dtos
{
    public class TodoDtoBuilder
    {
        public static TodoDto CreateValid(Guid id)
        {
            return new TodoDto
            {
                Id = id,
                Description = "This is a valid todo",
                IsDone = false
            };
        }
    }
}
