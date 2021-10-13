using Microsoft.EntityFrameworkCore.Migrations;

namespace YOGBIS.Data.Migrations
{
    public partial class sondegisiklkler : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Derecesi",
                table: "MulakatSorularis");

            migrationBuilder.AddColumn<string>(
                name: "KullaniciId",
                table: "Okullars",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DereceId",
                table: "MulakatSorularis",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Okullars_KullaniciId",
                table: "Okullars",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_MulakatSorularis_DereceId",
                table: "MulakatSorularis",
                column: "DereceId");

            migrationBuilder.AddForeignKey(
                name: "FK_MulakatSorularis_Derecelers_DereceId",
                table: "MulakatSorularis",
                column: "DereceId",
                principalTable: "Derecelers",
                principalColumn: "DereceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Okullars_AspNetUsers_KullaniciId",
                table: "Okullars",
                column: "KullaniciId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MulakatSorularis_Derecelers_DereceId",
                table: "MulakatSorularis");

            migrationBuilder.DropForeignKey(
                name: "FK_Okullars_AspNetUsers_KullaniciId",
                table: "Okullars");

            migrationBuilder.DropIndex(
                name: "IX_Okullars_KullaniciId",
                table: "Okullars");

            migrationBuilder.DropIndex(
                name: "IX_MulakatSorularis_DereceId",
                table: "MulakatSorularis");

            migrationBuilder.DropColumn(
                name: "KullaniciId",
                table: "Okullars");

            migrationBuilder.DropColumn(
                name: "DereceId",
                table: "MulakatSorularis");

            migrationBuilder.AddColumn<string>(
                name: "Derecesi",
                table: "MulakatSorularis",
                type: "text",
                nullable: true);
        }
    }
}
