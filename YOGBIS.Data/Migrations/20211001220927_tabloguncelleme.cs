using Microsoft.EntityFrameworkCore.Migrations;

namespace YOGBIS.Data.Migrations
{
    public partial class tabloguncelleme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Derecesi",
                table: "SoruBankasis");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "SoruBankasis",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SoruBankasis_Id",
                table: "SoruBankasis",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SoruBankasis_Derecelers_Id",
                table: "SoruBankasis",
                column: "Id",
                principalTable: "Derecelers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SoruBankasis_Derecelers_Id",
                table: "SoruBankasis");

            migrationBuilder.DropIndex(
                name: "IX_SoruBankasis_Id",
                table: "SoruBankasis");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "SoruBankasis");

            migrationBuilder.AddColumn<string>(
                name: "Derecesi",
                table: "SoruBankasis",
                type: "text",
                nullable: true);
        }
    }
}
