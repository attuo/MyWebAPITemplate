using MyWebAPITemplate.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyWebAPITemplate.Infrastructure.Database.Configurations
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
