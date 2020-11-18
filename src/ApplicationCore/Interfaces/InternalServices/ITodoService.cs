using System.Collections.Generic;
using System.Threading.Tasks;
using MyWebAPITemplate.ApplicationCore.Dtos;

namespace MyWebAPITemplate.ApplicationCore.Interfaces.InternalServices
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoDto>> GetTodos();
        Task<TodoDto> GetTodo(int id);
        Task<TodoDto> CreateTodo(TodoDto dto);
        Task<TodoDto> UpdateTodo(int id, TodoDto dto);
        Task<bool> DeleteTodo(int id);

    }
}
