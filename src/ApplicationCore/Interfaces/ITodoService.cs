using System.Collections.Generic;
using AspNetCoreWebApiTemplate.ApplicationCore.Dtos;

namespace AspNetCoreWebApiTemplate.ApplicationCore.Interfaces
{
    public interface ITodoService
    {
        IEnumerable<TodoDto> GetTodos();
    }
}
