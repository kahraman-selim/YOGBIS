using Microsoft.EntityFrameworkCore.Migrations;

namespace YOGBIS.Data.Migrations
{
    public partial class adaylarpropekle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdayAd",
                table: "Adaylars",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdayAd2",
                table: "Adaylars",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdaySoyad",
                table: "Adaylars",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdaySoyad2",
                table: "Adaylars",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdayAd",
                table: "Adaylars");

            migrationBuilder.DropColumn(
                name: "AdayAd2",
                table: "Adaylars");

            migrationBuilder.DropColumn(
                name: "AdaySoyad",
                table: "Adaylars");

            migrationBuilder.DropColumn(
                name: "AdaySoyad2",
                table: "Adaylars");
        }
    }
}
