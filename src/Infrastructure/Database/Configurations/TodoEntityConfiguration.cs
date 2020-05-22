using System;
using System.Collections.Generic;
using System.Text;
using AspNetCoreWebApiTemplate.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetCoreWebApiTemplate.Infrastructure.Database.Configurations
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
