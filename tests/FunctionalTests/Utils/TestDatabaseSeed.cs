using System.Collections.Generic;
using System.Threading.Tasks;
using MyWebAPITemplate.Source.Core.Entities;
using MyWebAPITemplate.Source.Infrastructure.Database;
using MyWebAPITemplate.Tests.Shared.Builders.Entities;
using MyWebAPITemplate.Tests.UnitTests.Shared.Ids;

// TODO: Consider unifying this with Integration Test DB, or other plan?
namespace MyWebAPITemplate.Tests.FunctionalTests.Utils
{
    /// <summary>
    /// Database seeding to ensure test database has data
    /// </summary>
    public static class TestDatabaseSeed
    {
        public static async Task ReinitializeDbForTests(ApplicationDbContext context)
        {
            await ClearDbForTests(context);
            await InitializeDbForTests(context);
        }

        private static async Task InitializeDbForTests(ApplicationDbContext context)
        {
            await context.Todos.AddRangeAsync(CreateTestTodos());
            await context.SaveChangesAsync();
        }

        private static async Task ClearDbForTests(ApplicationDbContext context)
        {
            context.Todos.RemoveRange(context.Todos);
            await context.SaveChangesAsync();
        }

        // TODO: Change this to be a generic method
        public static List<TodoEntity> CreateTestTodos()
        {
            return new List<TodoEntity>()
            {
                TodoEntityBuilder.CreateValid(TestIds.NormalUsageId),
                TodoEntityBuilder.CreateValid(TestIds.OtherUsageId)
            };
        }
    }
}
