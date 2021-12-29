using Microsoft.EntityFrameworkCore.Migrations;

namespace YOGBIS.Data.Migrations
{
    public partial class adayekleme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdayAnaAd",
                table: "Adaylars",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdayBabaAd",
                table: "Adaylars",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdayAnaAd",
                table: "Adaylars");

            migrationBuilder.DropColumn(
                name: "AdayBabaAd",
                table: "Adaylars");
        }
    }
}
