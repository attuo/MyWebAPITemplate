using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyWebAPITemplate.Source.Core.Dtos;

namespace MyWebAPITemplate.Source.Core.Interfaces.InternalServices;

/// <summary>
/// Interface for service for Todo methods
/// Should contain all business logic specific actions etc.
/// </summary>
public interface ITodoService
{
    /// <summary>
    /// Gets todos.
    /// </summary>
    /// <returns>Found todos or empty.</returns>
    Task<IEnumerable<TodoDto>> GetTodos();

    /// <summary>
    /// Gets todo.
    /// </summary>
    /// <param name="id">Id of the existing Todo.</param>
    /// <returns>Found todo or null.</returns>
    Task<TodoDto> GetTodo(Guid id);

    /// <summary>
    /// Creates todo.
    /// </summary>
    /// <param name="dto">Todo data.</param>
    /// <returns>Created todo.</returns>
    Task<TodoDto> CreateTodo(TodoDto dto);

    /// <summary>
    /// Updates todo.
    /// </summary>
    /// <param name="id">Id of the existing Todo.</param>
    /// <param name="dto">Todo data to be updated.</param>
    /// <returns>Updated todo or null.</returns>
    Task<TodoDto> UpdateTodo(Guid id, TodoDto dto);

    /// <summary>
    /// Delete todo.
    /// </summary>
    /// <param name="id">Id of the existing Todo.</param>
    /// <returns>Success or null if not found.</returns>
    Task DeleteTodo(Guid id);
}