using Microsoft.EntityFrameworkCore.Migrations;

namespace YOGBIS.Data.Migrations
{
    public partial class komisyonalanguncelleme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "KomisyonUyeDurum",
                table: "Komisyonlar",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "KomisyonUyeDurum",
                table: "Komisyonlar",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
