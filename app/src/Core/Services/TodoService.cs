using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyWebAPITemplate.Source.Core.Dtos;
using MyWebAPITemplate.Source.Core.Entities;
using MyWebAPITemplate.Source.Core.Interfaces.Database;
using MyWebAPITemplate.Source.Core.Interfaces.InternalServices;
using MyWebAPITemplate.Source.Core.Interfaces.Mappers;

namespace MyWebAPITemplate.Source.Core.Services;

/// <inheritdoc/>
public class TodoService : ITodoService
{
    private readonly ITodoRepository _todoRepository;
    private readonly ITodoDtoEntityMapper _todoMapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="TodoService"/> class.
    /// </summary>
    /// <param name="todoRepository">See <see cref="ITodoRepository"/>.</param>
    /// <param name="todoMapper">See <see cref="ITodoDtoEntityMapper"/>.</param>
    public TodoService(ITodoRepository todoRepository, ITodoDtoEntityMapper todoMapper)
    {
        _todoRepository = todoRepository;
        _todoMapper = todoMapper;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<TodoDto>> GetTodos()
    {
        IReadOnlyList<TodoEntity> todoEntities = await _todoRepository.ListAllAsync();
        return _todoMapper.Map(todoEntities);
    }

    /// <inheritdoc/>
    public async Task<TodoDto> GetTodo(Guid id)
    {
        TodoEntity todoEntity = await _todoRepository.GetByIdAsync(id);
        return _todoMapper.Map(todoEntity);
    }

    /// <inheritdoc/>
    public async Task<TodoDto> CreateTodo(TodoDto dto)
    {
        TodoEntity newTodoEntity = _todoMapper.Map(dto);
        TodoEntity createdTodoEntity = await _todoRepository.AddAsync(newTodoEntity);
        return _todoMapper.Map(createdTodoEntity);
    }

    /// <inheritdoc/>
    public async Task<TodoDto> UpdateTodo(Guid id, TodoDto dto)
    {
        TodoEntity existingTodoEntity = await _todoRepository.GetByIdAsync(id);
        TodoEntity updatableTodoEntity = _todoMapper.Map(dto, existingTodoEntity);
        await _todoRepository.UpdateAsync(updatableTodoEntity);
        return _todoMapper.Map(updatableTodoEntity);
    }

    /// <inheritdoc/>
    public async Task DeleteTodo(Guid id)
    {
        TodoEntity existingTodoEntity = await _todoRepository.GetByIdAsync(id);
        await _todoRepository.DeleteAsync(existingTodoEntity);
    }
}