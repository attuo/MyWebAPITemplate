using System;
using System.Linq;
using System.Threading.Tasks;
using MyWebAPITemplate.Core.Entities;

namespace MyWebAPITemplate.Infrastructure.Database
{
    public class ApplicationDbContextSeed
    {
        /// <summary>
        /// Seed Develop specific data
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static async Task SeedDevelopAsync(ApplicationDbContext context)
        {
            // TODO Seeding could also be done in OnModelCreating?
            // https://www.learnentityframeworkcore.com/migrations/seeding
            Guid todoId = Guid.Parse("10000000-0000-0000-0000-000000000000");
            TodoEntity existingTodo = await context.Todos.FindAsync(todoId);

            if (existingTodo == null)
            {
                var todo = new TodoEntity()
                {
                    Id = todoId,
                    Description = "Description for seeded todo",
                    IsDone = false
                };

                await context.Todos.AddAsync(todo);
                await context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Seed initial production & common data needed by the software.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            await SeedDevelopAsync(context);
        }
    }
}