using System.Collections.Generic;
using MyWebAPITemplate.Source.Core.Dtos;
using MyWebAPITemplate.Source.Core.Entities;

namespace MyWebAPITemplate.Source.Core.Interfaces.Mappers;

/// <summary>
/// Mapping methods between TodoDtos and TodoEntities
/// </summary>
public interface ITodoDtoEntityMapper
{
    /// <summary>
    /// TodoDto -> TodoEntity
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    TodoEntity Map(TodoDto dto);

    /// <summary>
    /// TodoDto, TodoEntity -> TodoEntity
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="entity"></param>
    /// <returns></returns>
    TodoEntity Map(TodoDto dto, TodoEntity entity);

    /// <summary>
    /// TodoEntity -> TodoDto
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    TodoDto Map(TodoEntity entity);

    /// <summary>
    /// TodoEntities -> TodoDtos
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    IEnumerable<TodoDto> Map(IReadOnlyList<TodoEntity> entities);
}