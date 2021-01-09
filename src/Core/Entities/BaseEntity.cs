using System;

namespace MyWebAPITemplate.Source.Core.Entities
{
    /// <summary>
    /// All entities inherit this base class
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Entity Framework sets this as Id for each entity
        /// </summary>
        public virtual Guid Id { get; set; }
    }
}