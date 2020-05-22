using System;
using System.Collections.Generic;
using System.Text;
using AspNetCoreWebApiTemplate.ApplicationCore.Dtos;
using AspNetCoreWebApiTemplate.ApplicationCore.Entities;

namespace AspNetCoreWebApiTemplate.ApplicationCore.Interfaces.Converters
{
    public interface ITodoDtoEntityConverter
    {
        TodoEntity Convert(TodoDto dto);
        TodoEntity Convert(TodoDto dto, TodoEntity entity);
        TodoDto Convert(TodoEntity entity);
        IEnumerable<TodoDto> Convert(IReadOnlyList<TodoEntity> entities);
    }
}
