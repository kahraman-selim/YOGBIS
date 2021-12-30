using Microsoft.EntityFrameworkCore.Migrations;

namespace YOGBIS.Data.Migrations
{
    public partial class etkinlikresimyuk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Resim1Yol",
                table: "Aktivitelers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Resim2Yol",
                table: "Aktivitelers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Resim3Yol",
                table: "Aktivitelers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Resim1Yol",
                table: "Aktivitelers");

            migrationBuilder.DropColumn(
                name: "Resim2Yol",
                table: "Aktivitelers");

            migrationBuilder.DropColumn(
                name: "Resim3Yol",
                table: "Aktivitelers");
        }
    }
}
