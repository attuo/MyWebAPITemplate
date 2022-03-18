namespace MyWebAPITemplate.Source.Core.Exceptions;

/// <summary>
/// Represents error states where entity is not found.
/// </summary>
public class EntityNotFoundException : Exception
{
    /// <inheritdoc cref="Exception"/>
    public EntityNotFoundException()
    {
    }

    /// <inheritdoc cref="Exception"/>
    public EntityNotFoundException(string? message) : base(message)
    {
    }

    /// <inheritdoc cref="Exception"/>
    public EntityNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}