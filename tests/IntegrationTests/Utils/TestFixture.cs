using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyWebAPITemplate.Source.Core.Entities;
using MyWebAPITemplate.Source.Infrastructure.Database;

namespace MyWebAPITemplate.Tests.IntegrationTests.Utils
{
    public abstract class TestFixture
    {
        protected ApplicationDbContext _dbContext;

        protected static DbContextOptions<ApplicationDbContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseInMemoryDatabase("InMemoryDB-Integration-Tests") // TODO: Config to use real database
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        // TODO: Make this a generic method
        protected EfRepository<TodoEntity> GetTodoRepository()
        {
            var options = CreateNewContextOptions();

            _dbContext = new ApplicationDbContext(options);
            return new EfRepository<TodoEntity>(_dbContext);
        }
    }

}
