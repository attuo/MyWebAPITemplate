using Microsoft.EntityFrameworkCore;
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
        // NOTE: This is by no means a good solution for production.
        // Only use this for local development, but it should be adviced that this is best to do with a separate migration runner.

        // TODO: Create a separate migration runner which handles all the migrations
        // and so it is a completely different instance which can be run in CI/CD pipeline etc..
        MigratePendingMigrations(context);

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
    /// Migrates database with pending migrations. This should not be used in production.
    /// </summary>
    /// <param name="context">See <see cref="ApplicationDbContext"/>.</param>
    private static void MigratePendingMigrations(ApplicationDbContext context)
    {
        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }
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