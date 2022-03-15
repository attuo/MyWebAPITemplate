using MyWebAPITemplate.Source.Core.Interfaces.Database;

namespace MyWebAPITemplate.Source.Core.Entities;

/// <summary>
/// Entity for Todo.
/// </summary>
public class TodoEntity : BaseEntity, IAggregateRoot
{
    /// <summary>
    /// Gets or sets text for what needs to be done in the todo item.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether is the todo still in progress or done.
    /// </summary>
    public bool IsDone { get; set; }
}