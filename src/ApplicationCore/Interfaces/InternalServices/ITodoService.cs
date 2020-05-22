using System.Collections.Generic;
using AspNetCoreWebApiTemplate.ApplicationCore.Dtos;

namespace AspNetCoreWebApiTemplate.ApplicationCore.Interfaces.InternalServices
{
    public interface ITodoService
    {
        IEnumerable<TodoDto> GetTodos();
    }
}
