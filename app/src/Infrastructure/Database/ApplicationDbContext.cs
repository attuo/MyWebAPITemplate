using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MyWebAPITemplate.Source.Core.Entities;

namespace MyWebAPITemplate.Source.Infrastructure.Database;

/// <summary>
/// Applications DB Context which sets configurations for Entity Framework.
/// </summary>
public class ApplicationDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
    /// </summary>
    /// <param name="options">See <see cref="DbContextOptions"/>.</param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    // When adding new entity (table), add its DbSet here

    /// <summary>
    /// Gets the entity set for Entity Framework.
    /// </summary>
    public DbSet<TodoEntity> Todos => Set<TodoEntity>();

    // .. Add more entities here

    /// <summary>
    /// Overridden method for applying all the entity type configurations from assembly.
    /// </summary>
    /// <param name="modelBuilder">See <see cref="ModelBuilder"/>.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // This will automatically find all the entity configurations from Infrastructure/Database/Configurations that inherit IEntityTypeConfiguration
        _ = modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}