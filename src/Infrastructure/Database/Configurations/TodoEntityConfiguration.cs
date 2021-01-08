using MyWebAPITemplate.Source.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
