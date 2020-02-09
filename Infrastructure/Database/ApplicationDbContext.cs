using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database
{
    public class ApplicationDbContext : DbContext //, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<TodoEntity> Todos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<TodoEntity>(ConfigureTodoEntity);
        }

        protected void ConfigureTodoEntity(EntityTypeBuilder<TodoEntity> builder)
        {
            builder.ToTable("Todo");
        }
    }
}