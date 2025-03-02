using Microsoft.EntityFrameworkCore.Migrations;

namespace YOGBIS.Data.Migrations
{
    public partial class adaybasvurubilgileriguncelleme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MYYSinavTedbiri",
                table: "AdayBasvuruBilgileri");

            migrationBuilder.DropColumn(
                name: "MYYSonuc",
                table: "AdayBasvuruBilgileri");

            migrationBuilder.AddColumn<string>(
                name: "MYYSSinavTedbiri",
                table: "AdayBasvuruBilgileri",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MYYSSonuc",
                table: "AdayBasvuruBilgileri",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MYYSSinavTedbiri",
                table: "AdayBasvuruBilgileri");

            migrationBuilder.DropColumn(
                name: "MYYSSonuc",
                table: "AdayBasvuruBilgileri");

            migrationBuilder.AddColumn<string>(
                name: "MYYSinavTedbiri",
                table: "AdayBasvuruBilgileri",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MYYSonuc",
                table: "AdayBasvuruBilgileri",
                type: "text",
                nullable: true);
        }
    }
}
