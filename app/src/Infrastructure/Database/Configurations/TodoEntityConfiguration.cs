using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyWebAPITemplate.Source.Core.Entities;

namespace MyWebAPITemplate.Source.Infrastructure.Database.Configurations;

/// <summary>
/// TodoEntity database table configurations for Entity Framework
/// This class gets automatically called by Infrastructure/Database/ApplicationDbContext.
/// </summary>
public class TodoEntityConfiguration : IEntityTypeConfiguration<TodoEntity>
{
    /// <summary>
    /// Configure all entity's properties here that are set on database table.
    /// </summary>
    /// <param name="builder">See <see cref="EntityTypeBuilder"/>.</param>
    /// <exception cref="ArgumentNullException">Null checks the method parameters.</exception>
    public void Configure(EntityTypeBuilder<TodoEntity> builder)
    {
        _ = builder ?? throw new ArgumentNullException(nameof(builder));
        SetProperties(builder);
    }

    private static void SetProperties(EntityTypeBuilder<TodoEntity> builder)
    {
        _ = builder.Property(entity => entity.Description)
            .HasMaxLength(1000);
    }
}