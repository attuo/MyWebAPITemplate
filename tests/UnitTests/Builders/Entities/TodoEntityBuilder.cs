using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyWebAPITemplate.Core.Entities;

namespace MyWebAPITemplate.UnitTests.Builders.Entities
{
    public class TodoEntityBuilder
    {
        public static TodoEntity CreateValid()
        {
            return new TodoEntity
            {
                Id = Guid.NewGuid(),
                Description = "This is a valid todo",
                IsDone = false
            };
        }
    }
}
