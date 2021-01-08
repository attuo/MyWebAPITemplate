using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyWebAPITemplate.Source.Core.Dtos;

namespace MyWebAPITemplate.Source.Core.Interfaces.InternalServices
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoDto>> GetTodos();
        Task<TodoDto> GetTodo(Guid id);
        Task<TodoDto> CreateTodo(TodoDto dto);
        Task<TodoDto> UpdateTodo(Guid id, TodoDto dto);
        Task<bool?> DeleteTodo(Guid id);

    }
}
