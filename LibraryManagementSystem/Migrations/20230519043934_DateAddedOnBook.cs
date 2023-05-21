using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class DateAddedOnBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Books",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedUserId",
                table: "Books",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_ModifiedUserId",
                table: "Books",
                column: "ModifiedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_AspNetUsers_ModifiedUserId",
                table: "Books",
                column: "ModifiedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_AspNetUsers_ModifiedUserId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_ModifiedUserId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ModifiedUserId",
                table: "Books");
        }
    }
}
