using System;
using MyWebAPITemplate.Source.Core.Dtos;

namespace MyWebAPITemplate.Tests.Shared.Builders.Dtos
{
    /// <summary>
    /// TodoDto builder
    /// </summary>
    public class TodoDtoBuilder
    {
        /// <summary>
        /// Create valid dot with given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
