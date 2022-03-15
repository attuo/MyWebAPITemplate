namespace MyWebAPITemplate.Source.Core.Dtos;

/// <summary>
/// Data transfer object for the Todo.
/// </summary>
public class TodoDto : BaseDto
{
    /// <summary>
    /// Gets or inits a description. This is the actual info that the Todo contains.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Gets a value indicating whether Todo is done or not.
    /// </summary>
    public bool IsDone { get; init; }
}