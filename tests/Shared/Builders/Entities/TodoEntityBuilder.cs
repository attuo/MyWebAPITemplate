using System;
using MyWebAPITemplate.Source.Core.Entities;

namespace MyWebAPITemplate.Tests.Shared.Builders.Entities
{
    public class TodoEntityBuilder
    {
        public static TodoEntity CreateValid(Guid id)
        {
            return new TodoEntity
            {
                Id = id,
                Description = "This is a valid todo",
                IsDone = false
            };
        }
    }
}
