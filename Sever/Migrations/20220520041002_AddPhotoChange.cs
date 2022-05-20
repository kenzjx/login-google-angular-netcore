using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sever.Migrations
{
    public partial class AddPhotoChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileExtension",
                table: "Photos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Photos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileExtension",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Photos");
        }
    }
}
