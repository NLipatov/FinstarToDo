using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinstarToDo.Migrations
{
    /// <inheritdoc />
    public partial class DeletedToDoPropFromCommentaryModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_ToDos_ToDoId",
                table: "Comments");

            migrationBuilder.AlterColumn<Guid>(
                name: "ToDoId",
                table: "Comments",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_ToDos_ToDoId",
                table: "Comments",
                column: "ToDoId",
                principalTable: "ToDos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_ToDos_ToDoId",
                table: "Comments");

            migrationBuilder.AlterColumn<Guid>(
                name: "ToDoId",
                table: "Comments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_ToDos_ToDoId",
                table: "Comments",
                column: "ToDoId",
                principalTable: "ToDos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
