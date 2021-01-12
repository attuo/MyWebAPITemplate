using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyWebAPITemplate.Source.Core.Dtos;
using MyWebAPITemplate.Source.Core.Entities;
using MyWebAPITemplate.Source.Core.Interfaces.Database;
using MyWebAPITemplate.Source.Core.Interfaces.InternalServices;
using MyWebAPITemplate.Source.Core.Interfaces.Mappers;

namespace MyWebAPITemplate.Source.Core.Services
{
    ///<inheritdoc/>
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;
        private readonly ITodoDtoEntityMapper _todoMapper;

        public TodoService(ITodoRepository todoRepository, ITodoDtoEntityMapper todoMapper)
        {
            _todoRepository = todoRepository;
            _todoMapper = todoMapper;
        }

        public async Task<IEnumerable<TodoDto>> GetTodos()
        {
            IReadOnlyList<TodoEntity> todoEntities = await _todoRepository.ListAllAsync();
            IEnumerable<TodoDto> todoDtos = _todoMapper.Map(todoEntities);
            return todoDtos;
        }

        public async Task<TodoDto> GetTodo(Guid id)
        {
            TodoEntity todoEntity = await _todoRepository.GetByIdAsync(id);
            if (todoEntity == null) return null;
            TodoDto todoDto = _todoMapper.Map(todoEntity);
            return todoDto;
        }

        public async Task<TodoDto> CreateTodo(TodoDto newTodoDto)
        {
            TodoEntity newTodoEntity = _todoMapper.Map(newTodoDto);
            TodoEntity createdTodoEntity = await _todoRepository.AddAsync(newTodoEntity);
            TodoDto createdTodoDto = _todoMapper.Map(createdTodoEntity);
            return createdTodoDto;
        }

        public async Task<TodoDto> UpdateTodo(Guid id, TodoDto updatableTodoDto)
        {
            TodoEntity existingTodoEntity = await _todoRepository.GetByIdAsync(id);
            if (existingTodoEntity == null) return null;

            TodoEntity updatableTodoEntity = _todoMapper.Map(updatableTodoDto, existingTodoEntity);
            await _todoRepository.UpdateAsync(updatableTodoEntity);

            TodoDto updatedTodo = _todoMapper.Map(updatableTodoEntity);
            return updatedTodo;
        }

        public async Task<bool?> DeleteTodo(Guid id)
        {
            TodoEntity existingTodoEntity = await _todoRepository.GetByIdAsync(id);
            if (existingTodoEntity == null) return null;

            await _todoRepository.DeleteAsync(existingTodoEntity);
            return true;
        }
    }
}
