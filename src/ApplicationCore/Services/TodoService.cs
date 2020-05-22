using System.Collections.Generic;
using AspNetCoreWebApiTemplate.ApplicationCore.Dtos;
using AspNetCoreWebApiTemplate.ApplicationCore.Interfaces;

namespace AspNetCoreWebApiTemplate.ApplicationCore.Services
{
    public class TodoService : ITodoService
    {

        public TodoService()
        {

        }

        public IEnumerable<TodoDto> GetTodos()
        {
            var todos = new List<TodoDto>
            {
                new TodoDto { Id = 1, Text = "Todo text", IsDone = false }
            };
            return todos;
        }
    }
}
