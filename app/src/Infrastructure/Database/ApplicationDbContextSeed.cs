using System;
using System.Threading.Tasks;
using MyWebAPITemplate.Source.Core.Entities;

namespace MyWebAPITemplate.Source.Infrastructure.Database;

/// <summary>
/// Data seeding for the DbContext.
/// </summary>
public static class ApplicationDbContextSeed
{
    /// <summary>
    /// Seeds Develop specific data.
    /// </summary>
    /// <param name="context">See <see cref="ApplicationDbContext"/>.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public static async Task SeedDevelopAsync(ApplicationDbContext context)
    {
        // TODO Seeding could also be done in OnModelCreating?
        // https://www.learnentityframeworkcore.com/migrations/seeding
        await AddTodos(context);
    }

    /// <summary>
    /// Seed initial production and common data needed by the system.
    /// </summary>
    /// <param name="context">See <see cref="ApplicationDbContext"/>.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        await SeedDevelopAsync(context);
    }

    /// <summary>
    /// Adds seed Todos to the database.
    /// </summary>
    /// <param name="context">See <see cref="ApplicationDbContext"/>.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    private static async Task AddTodos(ApplicationDbContext context)
    {
        Guid todoId = Guid.Parse("10000000-0000-0000-0000-000000000000");
        TodoEntity? existingTodo = await context.Todos.FindAsync(todoId);

        if (existingTodo == null)
        {
            var todo = new TodoEntity()
            {
                Id = todoId,
                Description = "Description for seeded todo",
                IsDone = false
            };

            _ = await context.Todos.AddAsync(todo);
            _ = await context.SaveChangesAsync();
        }
    }
}