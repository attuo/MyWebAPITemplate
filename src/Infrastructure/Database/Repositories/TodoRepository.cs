using System;
using System.Collections.Generic;
using System.Text;
using AspNetCoreWebApiTemplate.ApplicationCore.Entities;
using AspNetCoreWebApiTemplate.ApplicationCore.Interfaces.Database;

namespace AspNetCoreWebApiTemplate.Infrastructure.Database.Repositories
{
    public class TodoRepository : EfRepository<TodoEntity>, ITodoRepository
    {
        public TodoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        //public Task<TodoEntity> GetByIdWithItemsAsync(int id)
        //{
        //    return _dbContext.Todos
        //        .Include(o => o.TodoItems)
        //        .Include($"{nameof(Todo.TodoItems)}.{nameof(TodoItem.ItemOrdered)}")
        //        .FirstOrDefaultAsync(x => x.Id == id);
        //}
    }
}
