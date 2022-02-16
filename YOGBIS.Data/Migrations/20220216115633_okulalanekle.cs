using Microsoft.EntityFrameworkCore.Migrations;

namespace YOGBIS.Data.Migrations
{
    public partial class okulalanekle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcikAlan",
                table: "Okullars");

            migrationBuilder.DropColumn(
                name: "EPostaAdresi",
                table: "Okullars");

            migrationBuilder.DropColumn(
                name: "HizmetGecisDonem",
                table: "Okullars");

            migrationBuilder.DropColumn(
                name: "InternetAdresi",
                table: "Okullars");

            migrationBuilder.DropColumn(
                name: "KapaliAlan",
                table: "Okullars");

            migrationBuilder.DropColumn(
                name: "MulkiDurum",
                table: "Okullars");

            migrationBuilder.DropColumn(
                name: "UlkeId",
                table: "Okullars");

            migrationBuilder.AddColumn<string>(
                name: "OkulAcikAlan",
                table: "Okullars",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OkulEPostaAdresi",
                table: "Okullars",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OkulHizmetGecisDonem",
                table: "Okullars",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OkulInternetAdresi",
                table: "Okullars",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OkulKapaliAlan",
                table: "Okullars",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "OkulMulkiDurum",
                table: "Okullars",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OkulUlkeId",
                table: "Okullars",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OkulAcikAlan",
                table: "Okullars");

            migrationBuilder.DropColumn(
                name: "OkulEPostaAdresi",
                table: "Okullars");

            migrationBuilder.DropColumn(
                name: "OkulHizmetGecisDonem",
                table: "Okullars");

            migrationBuilder.DropColumn(
                name: "OkulInternetAdresi",
                table: "Okullars");

            migrationBuilder.DropColumn(
                name: "OkulKapaliAlan",
                table: "Okullars");

            migrationBuilder.DropColumn(
                name: "OkulMulkiDurum",
                table: "Okullars");

            migrationBuilder.DropColumn(
                name: "OkulUlkeId",
                table: "Okullars");

            migrationBuilder.AddColumn<string>(
                name: "AcikAlan",
                table: "Okullars",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EPostaAdresi",
                table: "Okullars",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HizmetGecisDonem",
                table: "Okullars",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InternetAdresi",
                table: "Okullars",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KapaliAlan",
                table: "Okullars",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "MulkiDurum",
                table: "Okullars",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "UlkeId",
                table: "Okullars",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
