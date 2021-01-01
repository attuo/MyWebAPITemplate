using MyWebAPITemplate.Core.Entities;

namespace MyWebAPITemplate.Core.Interfaces.Database
{
    public interface ITodoRepository : IAsyncRepository<TodoEntity>
    {
        //Task<TodoEntity> GetByIdWithItemsAsync(int id);
    }
}
