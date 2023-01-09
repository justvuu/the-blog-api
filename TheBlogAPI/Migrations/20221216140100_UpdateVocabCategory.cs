using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheBlogAPI.Migrations
{
    public partial class UpdateVocabCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "OpenTime",
                table: "VocabCategory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OpenTime",
                table: "VocabCategory");
        }
    }
}
