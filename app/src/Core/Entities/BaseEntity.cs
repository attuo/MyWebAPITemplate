using System;

namespace MyWebAPITemplate.Source.Core.Entities;

/// <summary>
/// All entities inherit this base class.
/// </summary>
public class BaseEntity
{
    /// <summary>
    /// Gets or sets as Id for each entity.
    /// </summary>
    public virtual Guid Id { get; set; }
}