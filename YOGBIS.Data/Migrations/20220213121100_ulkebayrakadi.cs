using Microsoft.EntityFrameworkCore.Migrations;

namespace YOGBIS.Data.Migrations
{
    public partial class ulkebayrakadi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UlkeBayrakAdi",
                table: "Ulkelers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UlkeBayrakAdi",
                table: "Ulkelers");
        }
    }
}
