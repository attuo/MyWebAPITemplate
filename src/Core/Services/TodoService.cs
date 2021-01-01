using System.Collections.Generic;
using System.Threading.Tasks;
using MyWebAPITemplate.Core.Dtos;
using MyWebAPITemplate.Core.Entities;
using MyWebAPITemplate.Core.Interfaces.Converters;
using MyWebAPITemplate.Core.Interfaces.Database;
using MyWebAPITemplate.Core.Interfaces.InternalServices;

namespace MyWebAPITemplate.Core.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;
        private readonly ITodoDtoEntityConverter _todoConverter;

        public TodoService(ITodoRepository todoRepository, ITodoDtoEntityConverter todoConverter)
        {
            _todoRepository = todoRepository;
            _todoConverter = todoConverter;
        }

        public async Task<IEnumerable<TodoDto>> GetTodos()
        {
            IReadOnlyList<TodoEntity> todoEntities = await _todoRepository.ListAllAsync();
            IEnumerable<TodoDto> todoDtos = _todoConverter.Convert(todoEntities);
            return todoDtos;
        }

        public async Task<TodoDto> GetTodo(int id)
        {
            TodoEntity todoEntity = await _todoRepository.GetByIdAsync(id);
            if (todoEntity == null) return null;
            TodoDto todoDto = _todoConverter.Convert(todoEntity);
            return todoDto;
        }

        public async Task<TodoDto> CreateTodo(TodoDto newTodoDto)
        {
            TodoEntity newTodoEntity = _todoConverter.Convert(newTodoDto);
            TodoEntity createdTodoEntity = await _todoRepository.AddAsync(newTodoEntity);
            TodoDto createdTodoDto = _todoConverter.Convert(createdTodoEntity);
            return createdTodoDto;
        }

        public async Task<TodoDto> UpdateTodo(int id, TodoDto updatableTodoDto)
        {
            TodoEntity existingTodoEntity = await _todoRepository.GetByIdAsync(id);
            if (existingTodoEntity == null) return null;

            TodoEntity updatableTodoEntity = _todoConverter.Convert(updatableTodoDto, existingTodoEntity);
            await _todoRepository.UpdateAsync(updatableTodoEntity);

            TodoDto updatedTodo = _todoConverter.Convert(updatableTodoEntity);
            return updatedTodo;
        }

        public async Task<bool> DeleteTodo(int id)
        {
            TodoEntity existingTodoEntity = await _todoRepository.GetByIdAsync(id);
            if (existingTodoEntity == null) return false;

            await _todoRepository.DeleteAsync(existingTodoEntity);
            return true;
        }
    }
}
