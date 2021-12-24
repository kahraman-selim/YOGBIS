using Microsoft.EntityFrameworkCore.Migrations;

namespace YOGBIS.Data.Migrations
{
    public partial class tabloyenileme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MulakatSorularis_Derecelers_DereceId",
                table: "MulakatSorularis");

            migrationBuilder.DropForeignKey(
                name: "FK_MulakatSorularis_SoruBankasis_SoruId",
                table: "MulakatSorularis");

            migrationBuilder.DropForeignKey(
                name: "FK_MulakatSorularis_SoruKategorilers_SoruKategoriId",
                table: "MulakatSorularis");

            migrationBuilder.DropIndex(
                name: "IX_MulakatSorularis_DereceId",
                table: "MulakatSorularis");

            migrationBuilder.DropIndex(
                name: "IX_MulakatSorularis_SoruId",
                table: "MulakatSorularis");

            migrationBuilder.DropIndex(
                name: "IX_MulakatSorularis_SoruKategoriId",
                table: "MulakatSorularis");

            migrationBuilder.AddColumn<string>(
                name: "KullaniciResimYol",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KaydedenId",
                table: "Adaylars",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KullaniciResimYol",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "KaydedenId",
                table: "Adaylars");

            migrationBuilder.CreateIndex(
                name: "IX_MulakatSorularis_DereceId",
                table: "MulakatSorularis",
                column: "DereceId");

            migrationBuilder.CreateIndex(
                name: "IX_MulakatSorularis_SoruId",
                table: "MulakatSorularis",
                column: "SoruId");

            migrationBuilder.CreateIndex(
                name: "IX_MulakatSorularis_SoruKategoriId",
                table: "MulakatSorularis",
                column: "SoruKategoriId");

            migrationBuilder.AddForeignKey(
                name: "FK_MulakatSorularis_Derecelers_DereceId",
                table: "MulakatSorularis",
                column: "DereceId",
                principalTable: "Derecelers",
                principalColumn: "DereceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MulakatSorularis_SoruBankasis_SoruId",
                table: "MulakatSorularis",
                column: "SoruId",
                principalTable: "SoruBankasis",
                principalColumn: "SoruBankasiId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MulakatSorularis_SoruKategorilers_SoruKategoriId",
                table: "MulakatSorularis",
                column: "SoruKategoriId",
                principalTable: "SoruKategorilers",
                principalColumn: "SoruKategorilerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
