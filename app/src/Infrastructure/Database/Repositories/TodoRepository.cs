using MyWebAPITemplate.Source.Core.Entities;
using MyWebAPITemplate.Source.Core.Interfaces.Database;

namespace MyWebAPITemplate.Source.Infrastructure.Database.Repositories;

/// <inheritdoc/>
public class TodoRepository : RepositoryBase<TodoEntity>, ITodoRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TodoRepository"/> class.
    /// </summary>
    /// <param name="dbContext">See <see cref="ApplicationDbContext"/>.</param>
    public TodoRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    // public Task<TodoEntity> GetByIdWithItemsAsync(int id)
    // {
    //     return _dbContext.Todos
    //         .Include(o => o.TodoItems)
    //         .FirstOrDefaultAsync(x => x.Id == id);
    // }
}