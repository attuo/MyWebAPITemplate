using MyWebAPITemplate.Source.Core.Entities;
using MyWebAPITemplate.Source.Infrastructure.Database;
using MyWebAPITemplate.Tests.SharedComponents.Builders.Entities;
using MyWebAPITemplate.Tests.SharedComponents.Ids;

namespace MyWebAPITemplate.Tests.FunctionalTests.Utils;
// TODO: Consider unifying this with Integration Test DB, or other plan?

/// <summary>
/// Database seeding to ensure test database has data.
/// </summary>
public static class TestDatabaseSeed
{
    /// <summary>
    /// Setups the database to default state for testing purposes.
    /// </summary>
    /// <param name="context">See <see cref="ApplicationDbContext"/>.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public static async Task ReinitializeDbForTests(ApplicationDbContext context)
    {
        await ClearDbForTests(context);
        await InitializeDbForTests(context);
    }

    /// <summary>
    /// Seeds all the initial data to the test database.
    /// </summary>
    /// <param name="context">See <see cref="ApplicationDbContext"/>.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    private static async Task InitializeDbForTests(ApplicationDbContext context)
    {
        await context.Todos.AddRangeAsync(CreateTestTodos());
        _ = await context.SaveChangesAsync();
    }

    /// <summary>
    /// Removes all the data from the test database.
    /// </summary>
    /// <param name="context">See <see cref="ApplicationDbContext"/>.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    private static async Task ClearDbForTests(ApplicationDbContext context)
    {
        context.Todos.RemoveRange(context.Todos);
        _ = await context.SaveChangesAsync();
    }

    /// <summary>
    /// Creates test todos for testing purposes.
    /// </summary>
    /// <returns>List of todos for tests.</returns>
    private static List<TodoEntity> CreateTestTodos()
    {
        // TODO: Change this to be a generic method
        return new List<TodoEntity>()
            {
                TodoEntityBuilder.CreateValid(TestIds.NormalUsageId),
                TodoEntityBuilder.CreateValid(TestIds.OtherUsageId)
            };
    }
}