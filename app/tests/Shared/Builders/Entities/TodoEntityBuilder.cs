using System;
using MyWebAPITemplate.Source.Core.Entities;

namespace MyWebAPITemplate.Tests.Shared.Builders.Entities;

/// <summary>
/// TodoEntity builder 
/// </summary>
public static class TodoEntityBuilder
{
    /// <summary>
    /// Create valid entity with given id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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
