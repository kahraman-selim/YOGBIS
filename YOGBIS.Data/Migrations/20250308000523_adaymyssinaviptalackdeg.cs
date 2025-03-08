using Microsoft.EntityFrameworkCore.Migrations;

namespace YOGBIS.Data.Migrations
{
    public partial class adaymyssinaviptalackdeg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SinavIptalAciklama",
                table: "AdayMYSS");

            migrationBuilder.AddColumn<string>(
                name: "SinavIptalAck",
                table: "AdayMYSS",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SinavIptalAck",
                table: "AdayMYSS");

            migrationBuilder.AddColumn<string>(
                name: "SinavIptalAciklama",
                table: "AdayMYSS",
                type: "text",
                nullable: true);
        }
    }
}
