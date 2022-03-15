using System;
using MyWebAPITemplate.Source.Core.Entities;

namespace MyWebAPITemplate.Tests.Shared.Builders.Entities;

/// <summary>
/// TodoEntity builder for testing purposes.
/// </summary>
public static class TodoEntityBuilder
{
    /// <summary>
    /// Create valid entity with given id.
    /// </summary>
    /// <param name="id">Id of the entity to be created.</param>
    /// <returns>New valid entity.</returns>
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