using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyWebAPITemplate.Source.Core.Dtos;

namespace MyWebAPITemplate.Source.Core.Interfaces.InternalServices;

/// <summary>
/// Service for Todo methods
/// Should contain all business logic specific actions etc.
/// </summary>
public interface ITodoService
{
    /// <summary>
    /// Get todos
    /// </summary>
    /// <returns>Found todos or empty</returns>
    Task<IEnumerable<TodoDto>> GetTodos();

    /// <summary>
    /// Get todo
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Found todo or null</returns>
    Task<TodoDto> GetTodo(Guid id);

    /// <summary>
    /// Create todo
    /// </summary>
    /// <param name="dto"></param>
    /// <returns>Created todo</returns>
    Task<TodoDto> CreateTodo(TodoDto dto);

    /// <summary>
    /// Update todo
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dto"></param>
    /// <returns>Updated todo or null</returns>
    Task<TodoDto> UpdateTodo(Guid id, TodoDto dto);

    /// <summary>
    /// Delete todo
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Success or null if not found</returns>
    Task<bool?> DeleteTodo(Guid id);

}
