using System.Collections.Generic;
using MyWebAPITemplate.Source.Core.Dtos;
using MyWebAPITemplate.Source.Core.Entities;

namespace MyWebAPITemplate.Source.Core.Interfaces.Mappers
{
    public interface ITodoDtoEntityMapper
    {
        TodoEntity Map(TodoDto dto);
        TodoEntity Map(TodoDto dto, TodoEntity entity);
        TodoDto Map(TodoEntity entity);
        IEnumerable<TodoDto> Map(IReadOnlyList<TodoEntity> entities);
    }
}
