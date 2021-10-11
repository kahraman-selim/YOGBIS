using Microsoft.EntityFrameworkCore.Migrations;

namespace YOGBIS.Data.Migrations
{
    public partial class okulbilgiyenileme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YoneticiAdiSoyadi",
                table: "OkulBilgis");

            migrationBuilder.DropColumn(
                name: "YoneticiDonusYil",
                table: "OkulBilgis");

            migrationBuilder.DropColumn(
                name: "YoneticiEPosta",
                table: "OkulBilgis");

            migrationBuilder.DropColumn(
                name: "YoneticiGorev",
                table: "OkulBilgis");

            migrationBuilder.DropColumn(
                name: "YoneticiTelefon",
                table: "OkulBilgis");

            migrationBuilder.AddColumn<string>(
                name: "MdYrdAdiSoyadi",
                table: "OkulBilgis",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MdYrdDonusYil",
                table: "OkulBilgis",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MdYrdEPosta",
                table: "OkulBilgis",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MdYrdTelefon",
                table: "OkulBilgis",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MudurAdiSoyadi",
                table: "OkulBilgis",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MudurDonusYil",
                table: "OkulBilgis",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MudurEPosta",
                table: "OkulBilgis",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MudurTelefon",
                table: "OkulBilgis",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MdYrdAdiSoyadi",
                table: "OkulBilgis");

            migrationBuilder.DropColumn(
                name: "MdYrdDonusYil",
                table: "OkulBilgis");

            migrationBuilder.DropColumn(
                name: "MdYrdEPosta",
                table: "OkulBilgis");

            migrationBuilder.DropColumn(
                name: "MdYrdTelefon",
                table: "OkulBilgis");

            migrationBuilder.DropColumn(
                name: "MudurAdiSoyadi",
                table: "OkulBilgis");

            migrationBuilder.DropColumn(
                name: "MudurDonusYil",
                table: "OkulBilgis");

            migrationBuilder.DropColumn(
                name: "MudurEPosta",
                table: "OkulBilgis");

            migrationBuilder.DropColumn(
                name: "MudurTelefon",
                table: "OkulBilgis");

            migrationBuilder.AddColumn<string>(
                name: "YoneticiAdiSoyadi",
                table: "OkulBilgis",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "YoneticiDonusYil",
                table: "OkulBilgis",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "YoneticiEPosta",
                table: "OkulBilgis",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "YoneticiGorev",
                table: "OkulBilgis",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "YoneticiTelefon",
                table: "OkulBilgis",
                type: "text",
                nullable: true);
        }
    }
}
