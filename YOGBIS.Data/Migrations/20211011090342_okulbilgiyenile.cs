using Microsoft.EntityFrameworkCore.Migrations;

namespace YOGBIS.Data.Migrations
{
    public partial class okulbilgiyenile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OkulAdres",
                table: "OkulBilgis",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "YoneticiDonusYil",
                table: "OkulBilgis",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "YoneticiEPosta",
                table: "OkulBilgis",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OkulAdres",
                table: "OkulBilgis");

            migrationBuilder.DropColumn(
                name: "YoneticiDonusYil",
                table: "OkulBilgis");

            migrationBuilder.DropColumn(
                name: "YoneticiEPosta",
                table: "OkulBilgis");
        }
    }
}
