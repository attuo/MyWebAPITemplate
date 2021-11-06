using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyWebAPITemplate.Source.Infrastructure.Database.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Todos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsDone = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Todos", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Todos",
                columns: new[] { "Id", "Description", "IsDone" },
                values: new object[] { new Guid("10000000-0000-0000-0000-000000000000"), "This is a first default todo", false });

            migrationBuilder.InsertData(
                table: "Todos",
                columns: new[] { "Id", "Description", "IsDone" },
                values: new object[] { new Guid("20000000-0000-0000-0000-000000000000"), "This is a second default todo", false });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Todos");
        }
    }
}
