using System.Collections.Generic;
using MyWebAPITemplate.Source.Core.Dtos;
using MyWebAPITemplate.Source.Core.Entities;

namespace MyWebAPITemplate.Source.Core.Interfaces.Mappers;

/// <summary>
/// Mapping methods between TodoDtos and TodoEntities.
/// </summary>
public interface ITodoDtoEntityMapper
{
    /// <summary>
    /// Maps Dto to Entity.
    /// </summary>
    /// <param name="dto">Dto to be mapped from.</param>
    /// <returns>Mapped new entity.</returns>
    TodoEntity Map(TodoDto dto);

    /// <summary>
    /// Maps Dto data to existing entity.
    /// </summary>
    /// <param name="dto">Dto to be mapped from.</param>
    /// <param name="entity">Entity to be mapped to.</param>
    /// <returns>Mapped entity.</returns>
    TodoEntity Map(TodoDto dto, TodoEntity entity);

    /// <summary>
    /// Maps entity to Dto.
    /// </summary>
    /// <param name="entity">Entity to be mapped from.</param>
    /// <returns>Mapped new Dto.</returns>
    TodoDto Map(TodoEntity entity);

    /// <summary>
    /// Maps list of entities to list of Dtos.
    /// </summary>
    /// <param name="entities">List of entities to be mapped from.</param>
    /// <returns>Mapped list of new Dtos.</returns>
    IEnumerable<TodoDto> Map(IReadOnlyList<TodoEntity> entities);
}