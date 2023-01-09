using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheBlogAPI.Migrations
{
    public partial class UpdatePostModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "UrlHandle",
                table: "Posts",
                newName: "Path");

            migrationBuilder.RenameColumn(
                name: "FeaturedImageUrl",
                table: "Posts",
                newName: "Image");

            migrationBuilder.AddColumn<Guid>(
                name: "AuthorId",
                table: "Posts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Like",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AuthorId",
                table: "Posts",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_AuthorId",
                table: "Posts",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_AuthorId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_AuthorId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Like",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "Path",
                table: "Posts",
                newName: "UrlHandle");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Posts",
                newName: "FeaturedImageUrl");

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
