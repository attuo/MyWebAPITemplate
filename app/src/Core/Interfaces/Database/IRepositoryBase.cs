namespace MyWebAPITemplate.Source.Core.Interfaces.Database;

/// <summary>
/// Base database methods.
/// </summary>
/// <typeparam name="T">A specific repository.</typeparam>
public interface IRepositoryBase<T> where T : class, IAggregateRoot
{
    /// <summary>
    /// Get selected type entity from database with given id.
    /// </summary>
    /// <param name="id">Entity's id.</param>
    /// <returns>Found entity or null.</returns>
    Task<T> GetByIdAsync(Guid id);

    /// <summary>
    /// Get all selected type entities from database.
    /// </summary>
    /// <returns>Found entities or empty.</returns>
    Task<IReadOnlyList<T>> ListAllAsync();

    /// <summary>
    /// Add new selected type entity to database.
    /// </summary>
    /// <param name="entity">Entity with values.</param>
    /// <returns>Created entity with generated id.</returns>
    Task<T> AddAsync(T entity);

    /// <summary>
    /// Update existing selected type entity from database.
    /// </summary>
    /// <param name="entity">Existing entity with updated values.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task UpdateAsync(T entity);

    /// <summary>
    /// Delete existing selected type entity from database.
    /// </summary>
    /// <param name="entity">Existing entity.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task DeleteAsync(T entity);

    // TODO: Add other methods
}