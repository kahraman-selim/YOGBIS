using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YOGBIS.Data.Migrations
{
    public partial class adaybasvurudeg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DahaOnceYYGorev",
                table: "AdayBasvuruBilgileri");

            migrationBuilder.DropColumn(
                name: "KomisyonId",
                table: "AdayBasvuruBilgileri");

            migrationBuilder.DropColumn(
                name: "OnayAciklama",
                table: "AdayBasvuruBilgileri");

            migrationBuilder.DropColumn(
                name: "YabanciDilBelge",
                table: "AdayBasvuruBilgileri");

            migrationBuilder.DropColumn(
                name: "YabanciDilIGN",
                table: "AdayBasvuruBilgileri");

            migrationBuilder.AddColumn<string>(
                name: "BasvuruBrans",
                table: "AdayBasvuruBilgileri",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DahaOnceYDGorev",
                table: "AdayBasvuruBilgileri",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MYYSSonucItiraz",
                table: "AdayBasvuruBilgileri",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mezuniyet",
                table: "AdayBasvuruBilgileri",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OnayDurumuAck",
                table: "AdayBasvuruBilgileri",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "YabanciDilBasvuru",
                table: "AdayBasvuruBilgileri",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "YabanciDilING",
                table: "AdayBasvuruBilgileri",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BasvuruBrans",
                table: "AdayBasvuruBilgileri");

            migrationBuilder.DropColumn(
                name: "DahaOnceYDGorev",
                table: "AdayBasvuruBilgileri");

            migrationBuilder.DropColumn(
                name: "MYYSSonucItiraz",
                table: "AdayBasvuruBilgileri");

            migrationBuilder.DropColumn(
                name: "Mezuniyet",
                table: "AdayBasvuruBilgileri");

            migrationBuilder.DropColumn(
                name: "OnayDurumuAck",
                table: "AdayBasvuruBilgileri");

            migrationBuilder.DropColumn(
                name: "YabanciDilBasvuru",
                table: "AdayBasvuruBilgileri");

            migrationBuilder.DropColumn(
                name: "YabanciDilING",
                table: "AdayBasvuruBilgileri");

            migrationBuilder.AddColumn<string>(
                name: "DahaOnceYYGorev",
                table: "AdayBasvuruBilgileri",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "KomisyonId",
                table: "AdayBasvuruBilgileri",
                type: "varbinary(16)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OnayAciklama",
                table: "AdayBasvuruBilgileri",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "YabanciDilBelge",
                table: "AdayBasvuruBilgileri",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "YabanciDilIGN",
                table: "AdayBasvuruBilgileri",
                type: "text",
                nullable: true);
        }
    }
}
