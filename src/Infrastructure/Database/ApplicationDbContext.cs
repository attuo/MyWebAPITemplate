using System.Reflection;
using MyWebAPITemplate.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace MyWebAPITemplate.Infrastructure.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DB sets
        public DbSet<TodoEntity> Todos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Taken from https://github.com/dotnet-architecture/eShopOnWeb/blob/master/src/Infrastructure/Data/CatalogContext.cs
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //builder.Entity<TodoEntity>(ConfigureTodoEntity);
        }

    }
}