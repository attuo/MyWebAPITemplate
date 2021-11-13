using MyWebAPITemplate.Source.Core.Entities;
using MyWebAPITemplate.Source.Core.Interfaces.Database;

namespace MyWebAPITemplate.Source.Infrastructure.Database.Repositories;

///<inheritdoc/>
public class TodoRepository : EfRepository<TodoEntity>, ITodoRepository
{
    public TodoRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    //public Task<TodoEntity> GetByIdWithItemsAsync(int id)
    //{
    //    return _dbContext.Todos
    //        .Include(o => o.TodoItems)
    //        .FirstOrDefaultAsync(x => x.Id == id);
    //}
}
