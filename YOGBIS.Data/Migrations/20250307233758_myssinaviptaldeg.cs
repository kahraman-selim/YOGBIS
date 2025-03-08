using Microsoft.EntityFrameworkCore.Migrations;

namespace YOGBIS.Data.Migrations
{
    public partial class myssinaviptaldeg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sinavİptal",
                table: "AdayMYSS");

            migrationBuilder.AddColumn<bool>(
                name: "SinavIptal",
                table: "AdayMYSS",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SinavIptal",
                table: "AdayMYSS");

            migrationBuilder.AddColumn<bool>(
                name: "Sinavİptal",
                table: "AdayMYSS",
                type: "tinyint(1)",
                nullable: true);
        }
    }
}
