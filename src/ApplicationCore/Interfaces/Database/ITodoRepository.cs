using MyWebAPITemplate.ApplicationCore.Entities;

namespace MyWebAPITemplate.ApplicationCore.Interfaces.Database
{
    public interface ITodoRepository : IAsyncRepository<TodoEntity>
    {
        //Task<TodoEntity> GetByIdWithItemsAsync(int id);
    }
}
