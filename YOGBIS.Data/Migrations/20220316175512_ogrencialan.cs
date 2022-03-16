using Microsoft.EntityFrameworkCore.Migrations;

namespace YOGBIS.Data.Migrations
{
    public partial class ogrencialan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AyrilanSayisi",
                table: "Ogrenciler",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "KayitSayisi",
                table: "Ogrenciler",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AyrilanSayisi",
                table: "Ogrenciler");

            migrationBuilder.DropColumn(
                name: "KayitSayisi",
                table: "Ogrenciler");
        }
    }
}
