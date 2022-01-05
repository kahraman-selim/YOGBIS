using Microsoft.EntityFrameworkCore.Migrations;

namespace YOGBIS.Data.Migrations
{
    public partial class ulketabloduz : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UlkeBayrak",
                table: "Ulkelers");

            migrationBuilder.AddColumn<string>(
                name: "UlkeBayrakURL",
                table: "Ulkelers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UlkeBayrakURL",
                table: "Ulkelers");

            migrationBuilder.AddColumn<string>(
                name: "UlkeBayrak",
                table: "Ulkelers",
                type: "text",
                nullable: true);
        }
    }
}
