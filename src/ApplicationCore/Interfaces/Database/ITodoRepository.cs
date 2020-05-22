using System;
using System.Collections.Generic;
using System.Text;
using AspNetCoreWebApiTemplate.ApplicationCore.Entities;

namespace AspNetCoreWebApiTemplate.ApplicationCore.Interfaces.Database
{
    public interface ITodoRepository : IAsyncRepository<TodoEntity>
    {
        //Task<TodoEntity> GetByIdWithItemsAsync(int id);
    }
}
