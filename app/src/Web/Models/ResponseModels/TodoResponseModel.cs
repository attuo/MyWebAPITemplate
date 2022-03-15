using System;
using MyWebAPITemplate.Source.Web.Models.BaseModels;

namespace MyWebAPITemplate.Source.Web.Models.ResponseModels;

/// <summary>
/// Response model.
/// </summary>
public class TodoResponseModel : TodoBaseModel
{
    /// <summary>
    /// Gets id of the model.
    /// </summary>
    public Guid Id { get; init; }
}