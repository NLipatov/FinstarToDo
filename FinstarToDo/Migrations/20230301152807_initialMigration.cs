using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinstarToDo.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ToDos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Header = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDone = table.Column<bool>(type: "boolean", nullable: false),
                    Category = table.Column<int>(type: "integer", nullable: false),
                    Color = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ToDos_Category",
                table: "ToDos",
                column: "Category",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ToDos_Header",
                table: "ToDos",
                column: "Header",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ToDos");
        }
    }
}
