using MyWebAPITemplate.Source.Core.Entities;

namespace MyWebAPITemplate.Source.Core.Interfaces.Database
{
    public interface ITodoRepository : IAsyncRepository<TodoEntity>
    {
        //Task<TodoEntity> GetByIdWithItemsAsync(int id);
    }
}
