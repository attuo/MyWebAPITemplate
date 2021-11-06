using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MyWebAPITemplate.Source.Core.Entities;

namespace MyWebAPITemplate.Source.Infrastructure.Database
{
    /// <summary>
    /// Applications DB Context which sets configurations for Entity Framework
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        // DB sets
        // When adding new entity (table), add its DbSet here
        public DbSet<TodoEntity> Todos { get; set; }
        // .. Add more entities here

        // Taken from https://github.com/dotnet-architecture/eShopOnWeb/blob/master/src/Infrastructure/Data/CatalogContext.cs
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // This will automatically find all the entity configurations from Infrastructure/Database/Configurations that inherit IEntityTypeConfiguration
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}