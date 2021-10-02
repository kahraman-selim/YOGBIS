using Microsoft.EntityFrameworkCore.Migrations;

namespace YOGBIS.Data.Migrations
{
    public partial class tabloyenilemeleri : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Derecelers_SoruKategorilers_SoruKategorilerId",
                table: "Derecelers");

            migrationBuilder.DropIndex(
                name: "IX_Derecelers_SoruKategorilerId",
                table: "Derecelers");

            migrationBuilder.DropColumn(
                name: "SoruKategorilerDerece",
                table: "SoruKategorilers");

            migrationBuilder.DropColumn(
                name: "SoruKategorilerId",
                table: "Derecelers");

            migrationBuilder.AddColumn<string>(
                name: "DereceAdi",
                table: "SoruKategorilers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "SoruKategorilers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SoruKategorilers_Id",
                table: "SoruKategorilers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SoruKategorilers_Derecelers_Id",
                table: "SoruKategorilers",
                column: "Id",
                principalTable: "Derecelers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SoruKategorilers_Derecelers_Id",
                table: "SoruKategorilers");

            migrationBuilder.DropIndex(
                name: "IX_SoruKategorilers_Id",
                table: "SoruKategorilers");

            migrationBuilder.DropColumn(
                name: "DereceAdi",
                table: "SoruKategorilers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "SoruKategorilers");

            migrationBuilder.AddColumn<string>(
                name: "SoruKategorilerDerece",
                table: "SoruKategorilers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SoruKategorilerId",
                table: "Derecelers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Derecelers_SoruKategorilerId",
                table: "Derecelers",
                column: "SoruKategorilerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Derecelers_SoruKategorilers_SoruKategorilerId",
                table: "Derecelers",
                column: "SoruKategorilerId",
                principalTable: "SoruKategorilers",
                principalColumn: "SoruKategorilerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
