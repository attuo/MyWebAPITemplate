using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyWebAPITemplate.Core.Entities;
using MyWebAPITemplate.Infrastructure.Database;

namespace MyWebAPITemplate.FunctionalTests
{
    public static class TestDatabaseSeed
    {
        // TODO: Make test objects for each test section (todos etc.), so the functional tests

        public const string TodoId1 = "11111111-1111-1111-1111-111111111111";
        public const string TodoId2 = "22222222-2222-2222-2222-222222222222";

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

        public static List<TodoEntity> CreateTestTodos()
        {
            return new List<TodoEntity>()
            {
                new TodoEntity { Id=Guid.Parse(TodoId1), Description = "Test Todo 1", IsDone = false },
                new TodoEntity { Id=Guid.Parse(TodoId2), Description = "Test Todo 2", IsDone = false },
            };
        }
    }
}
