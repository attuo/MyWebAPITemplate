using MyWebAPITemplate.Source.Core.Dtos;

namespace MyWebAPITemplate.Source.Web.Models.BaseModels;

/// <summary>
/// Acts as base model for Todo request and response models.
/// Contains the shared attributes.
/// </summary>
public abstract class TodoBaseModel
{
    /// <inheritdoc cref="TodoDto.Description"/>
    public string? Description { get; set; }

    /// <inheritdoc cref="TodoDto.IsDone"/>
    public bool IsDone { get; set; }
}