using Microsoft.EntityFrameworkCore.Migrations;

namespace YOGBIS.Data.Migrations
{
    public partial class kullanicialan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdayId",
                table: "FotoGaleris");

            migrationBuilder.DropColumn(
                name: "EtkinlikId",
                table: "FotoGaleris");

            migrationBuilder.DropColumn(
                name: "OkulId",
                table: "FotoGaleris");

            migrationBuilder.DropColumn(
                name: "SehirId",
                table: "FotoGaleris");

            migrationBuilder.DropColumn(
                name: "UlkeId",
                table: "FotoGaleris");

            migrationBuilder.DropColumn(
                name: "UniId",
                table: "FotoGaleris");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdayId",
                table: "FotoGaleris",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EtkinlikId",
                table: "FotoGaleris",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OkulId",
                table: "FotoGaleris",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SehirId",
                table: "FotoGaleris",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UlkeId",
                table: "FotoGaleris",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UniId",
                table: "FotoGaleris",
                type: "int",
                nullable: true);
        }
    }
}
