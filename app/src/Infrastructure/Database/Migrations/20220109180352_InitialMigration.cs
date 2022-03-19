using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyWebAPITemplate.Source.Infrastructure.Database.Migrations;

/// <summary>
/// Initial migration of the system.
/// </summary>
public partial class InitialMigration : Migration
{
    /// <inheritdoc/>
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        _ = migrationBuilder.CreateTable(
            name: "Todos",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                IsDone = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_Todos", x => x.Id));
    }

    /// <inheritdoc/>
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        _ = migrationBuilder.DropTable(
            name: "Todos");
    }
}