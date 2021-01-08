using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyWebAPITemplate.Source.Core.Entities;

namespace MyWebAPITemplate.Source.Infrastructure.Database.Configurations
{
    public class TodoEntityConfiguration : IEntityTypeConfiguration<TodoEntity>
    {
        public void Configure(EntityTypeBuilder<TodoEntity> builder)
        {
            builder.Property(entity => entity.Description)
                .HasMaxLength(1000);
        }
    }
}
