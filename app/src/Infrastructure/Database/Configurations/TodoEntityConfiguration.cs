using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyWebAPITemplate.Source.Core.Entities;

namespace MyWebAPITemplate.Source.Infrastructure.Database.Configurations;

/// <summary>
/// TodoEntity database table configurations for Entity Framework
/// This class gets automatically called by Infrastructure/Database/ApplicationDbContext
/// </summary>
public class TodoEntityConfiguration : IEntityTypeConfiguration<TodoEntity>
{
    /// <summary>
    /// Configure all entity's properties here that are set on database table
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<TodoEntity> builder)
    {
        SetProperties(builder);
        SeedData(builder);
    }

    private void SetProperties(EntityTypeBuilder<TodoEntity> builder)
    {
        builder.Property(entity => entity.Description)
            .HasMaxLength(1000);
    }

    private void SeedData(EntityTypeBuilder<TodoEntity> builder)
    {
        builder.HasData
            (
                new TodoEntity
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000000"),
                    Description = "This is a first default todo",
                    IsDone = false
                },
                new TodoEntity
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000000"),
                    Description = "This is a second default todo",
                    IsDone = false
                }
            );
    }
}
