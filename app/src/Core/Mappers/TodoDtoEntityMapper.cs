using MyWebAPITemplate.Source.Core.Dtos;
using MyWebAPITemplate.Source.Core.Entities;
using MyWebAPITemplate.Source.Core.Interfaces.Mappers;

namespace MyWebAPITemplate.Source.Core.Mappers;

/// <inheritdoc/>
public class TodoDtoEntityMapper : ITodoDtoEntityMapper
{
    /// <inheritdoc/>
    public TodoEntity Map(TodoDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto, nameof(dto));

        return new TodoEntity
        {
            Description = dto.Description,
            IsDone = dto.IsDone,
        };
    }

    /// <inheritdoc/>
    public TodoDto Map(TodoEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        return new TodoDto
        {
            Id = entity.Id,
            Description = entity.Description,
            IsDone = entity.IsDone,
        };
    }

    /// <inheritdoc/>
    public TodoEntity Map(TodoDto dto, TodoEntity entity)
    {
        ArgumentNullException.ThrowIfNull(dto, nameof(dto));
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        entity.Description = dto.Description;
        entity.IsDone = dto.IsDone;
        return entity;
    }

    /// <inheritdoc/>
    public IEnumerable<TodoDto> Map(IReadOnlyList<TodoEntity> entities)
    {
        ArgumentNullException.ThrowIfNull(entities, nameof(entities));

        return entities.Select(entity => Map(entity));
    }
}