using Microsoft.EntityFrameworkCore.Migrations;

namespace YOGBIS.Data.Migrations
{
    public partial class sorubankasiduz : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SoruBankasis_SoruKategorilers_SoruKategoriId",
                table: "SoruBankasis");

            migrationBuilder.DropIndex(
                name: "IX_SoruBankasis_SoruKategoriId",
                table: "SoruBankasis");

            migrationBuilder.DropColumn(
                name: "SoruKategoriId",
                table: "SoruBankasis");

            migrationBuilder.AddColumn<int>(
                name: "SoruKategorilerId",
                table: "SoruBankasis",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SoruBankasis_SoruKategorilerId",
                table: "SoruBankasis",
                column: "SoruKategorilerId");

            migrationBuilder.AddForeignKey(
                name: "FK_SoruBankasis_SoruKategorilers_SoruKategorilerId",
                table: "SoruBankasis",
                column: "SoruKategorilerId",
                principalTable: "SoruKategorilers",
                principalColumn: "SoruKategorilerId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SoruBankasis_SoruKategorilers_SoruKategorilerId",
                table: "SoruBankasis");

            migrationBuilder.DropIndex(
                name: "IX_SoruBankasis_SoruKategorilerId",
                table: "SoruBankasis");

            migrationBuilder.DropColumn(
                name: "SoruKategorilerId",
                table: "SoruBankasis");

            migrationBuilder.AddColumn<int>(
                name: "SoruKategoriId",
                table: "SoruBankasis",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SoruBankasis_SoruKategoriId",
                table: "SoruBankasis",
                column: "SoruKategoriId");

            migrationBuilder.AddForeignKey(
                name: "FK_SoruBankasis_SoruKategorilers_SoruKategoriId",
                table: "SoruBankasis",
                column: "SoruKategoriId",
                principalTable: "SoruKategorilers",
                principalColumn: "SoruKategorilerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
