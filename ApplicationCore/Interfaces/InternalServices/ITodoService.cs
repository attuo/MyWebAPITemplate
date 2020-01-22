using ApplicationCore.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Interfaces.InternalServices
{
    public interface ITodoService
    {
        IEnumerable<TodoDto> GetTodos();
    }
}
