using System.Collections.Generic;
using MyWebAPITemplate.Core.Dtos;
using MyWebAPITemplate.Core.Entities;

namespace MyWebAPITemplate.Core.Interfaces.Converters
{
    public interface ITodoDtoEntityConverter
    {
        TodoEntity Convert(TodoDto dto);
        TodoEntity Convert(TodoDto dto, TodoEntity entity);
        TodoDto Convert(TodoEntity entity);
        IEnumerable<TodoDto> Convert(IReadOnlyList<TodoEntity> entities);
    }
}
