using ApplicationCore.Dtos;
using ApplicationCore.Interfaces.InternalServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Services
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
