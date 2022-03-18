namespace MyWebAPITemplate.Source.Core.Dtos;

/// <summary>
/// Base class for all the data transfer objects.
/// </summary>
public abstract class BaseDto
{
    /// <summary>
    /// Gets or inits the Id.
    /// </summary>
    public Guid? Id { get; init; }
}