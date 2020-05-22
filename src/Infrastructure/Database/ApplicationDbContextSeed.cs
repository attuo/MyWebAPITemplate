using System.Linq;
using System.Threading.Tasks;
using AspNetCoreWebApiTemplate.ApplicationCore.Entities;

namespace AspNetCoreWebApiTemplate.Infrastructure.Database
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

            if (context.Todos.FirstOrDefault() == null)
            {
                var todo = new TodoEntity()
                {
                    TodoText = "TestTodo",
                    Done = false
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
            await Task.CompletedTask;
        }
    }
}