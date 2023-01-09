using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheBlogAPI.Migrations
{
    public partial class UpdateVocabulary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sentence",
                table: "Vocabulary",
                newName: "Example");

            migrationBuilder.AddColumn<string>(
                name: "Pronunciation",
                table: "Vocabulary",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pronunciation",
                table: "Vocabulary");

            migrationBuilder.RenameColumn(
                name: "Example",
                table: "Vocabulary",
                newName: "Sentence");
        }
    }
}
