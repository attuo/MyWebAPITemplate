namespace MyWebAPITemplate.Source.Core.Entities;

public class TodoEntity : BaseEntity
{
    /// <summary>
    /// Text for what needs to be done in the todo item
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Is the todo still in progress or done
    /// </summary>
    public bool IsDone { get; set; }
}