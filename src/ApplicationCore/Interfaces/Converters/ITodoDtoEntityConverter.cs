using System.Collections.Generic;
using MyWebAPITemplate.ApplicationCore.Dtos;
using MyWebAPITemplate.ApplicationCore.Entities;

namespace MyWebAPITemplate.ApplicationCore.Interfaces.Converters
{
    public interface ITodoDtoEntityConverter
    {
        TodoEntity Convert(TodoDto dto);
        TodoEntity Convert(TodoDto dto, TodoEntity entity);
        TodoDto Convert(TodoEntity entity);
        IEnumerable<TodoDto> Convert(IReadOnlyList<TodoEntity> entities);
    }
}
