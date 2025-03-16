using Microsoft.EntityFrameworkCore.Migrations;

namespace YOGBIS.Data.Migrations
{
    public partial class tysalanekleme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GorevlendirmeSiraNo",
                table: "AdayTYS",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GorevlendirmeSiraNo",
                table: "AdayTYS");
        }
    }
}
