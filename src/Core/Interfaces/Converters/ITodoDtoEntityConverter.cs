using System.Collections.Generic;
using MyWebAPITemplate.Source.Core.Dtos;
using MyWebAPITemplate.Source.Core.Entities;

namespace MyWebAPITemplate.Source.Core.Interfaces.Converters
{
    public interface ITodoDtoEntityConverter
    {
        TodoEntity Convert(TodoDto dto);
        TodoEntity Convert(TodoDto dto, TodoEntity entity);
        TodoDto Convert(TodoEntity entity);
        IEnumerable<TodoDto> Convert(IReadOnlyList<TodoEntity> entities);
    }
}
