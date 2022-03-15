using System;
using MyWebAPITemplate.Source.Core.Dtos;

namespace MyWebAPITemplate.Tests.Shared.Builders.Dtos;

/// <summary>
/// TodoDto builder for testing purposes.
/// </summary>
public static class TodoDtoBuilder
{
    /// <summary>
    /// Creates a valid Todo with given id.
    /// </summary>
    /// <param name="id">Id of the object.</param>
    /// <returns>Created valid object.</returns>
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