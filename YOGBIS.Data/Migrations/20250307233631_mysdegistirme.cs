using Microsoft.EntityFrameworkCore.Migrations;

namespace YOGBIS.Data.Migrations
{
    public partial class mysdegistirme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MYSSPuan",
                table: "AdayMYSS");

            migrationBuilder.DropColumn(
                name: "MYSSSonuc",
                table: "AdayMYSS");

            migrationBuilder.DropColumn(
                name: "MYSSSonucAck",
                table: "AdayMYSS");

            migrationBuilder.AddColumn<string>(
                name: "MYSPuan",
                table: "AdayMYSS",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MYSSonuc",
                table: "AdayMYSS",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MYSSonucAck",
                table: "AdayMYSS",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MYSPuan",
                table: "AdayMYSS");

            migrationBuilder.DropColumn(
                name: "MYSSonuc",
                table: "AdayMYSS");

            migrationBuilder.DropColumn(
                name: "MYSSonucAck",
                table: "AdayMYSS");

            migrationBuilder.AddColumn<string>(
                name: "MYSSPuan",
                table: "AdayMYSS",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MYSSSonuc",
                table: "AdayMYSS",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MYSSSonucAck",
                table: "AdayMYSS",
                type: "text",
                nullable: true);
        }
    }
}
