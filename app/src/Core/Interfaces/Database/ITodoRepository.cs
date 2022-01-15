using MyWebAPITemplate.Source.Core.Entities;

namespace MyWebAPITemplate.Source.Core.Interfaces.Database;

/// <summary>
/// For repository pattern
/// </summary>
public interface ITodoRepository : IEfRepository<TodoEntity>
{
    // Implement repository pattern
    //Task<TodoEntity> GetByIdWithItemsAsync(int id);
}