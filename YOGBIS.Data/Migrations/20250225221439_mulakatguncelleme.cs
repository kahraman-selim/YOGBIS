using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YOGBIS.Data.Migrations
{
    public partial class mulakatguncelleme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Komisyonlar_Mulakatlar_MulakatId",
                table: "Komisyonlar");

            migrationBuilder.DropForeignKey(
                name: "FK_Mulakatlar_SoruDereceler_DereceId",
                table: "Mulakatlar");

            migrationBuilder.DropIndex(
                name: "IX_Mulakatlar_DereceId",
                table: "Mulakatlar");

            migrationBuilder.DropIndex(
                name: "IX_Komisyonlar_MulakatId",
                table: "Komisyonlar");

            migrationBuilder.DropColumn(
                name: "DereceId",
                table: "Mulakatlar");

            migrationBuilder.DropColumn(
                name: "MulakatId",
                table: "Komisyonlar");

            migrationBuilder.AddColumn<byte[]>(
                name: "KomisyonId",
                table: "AdayBasvuruBilgileri",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KomisyonId",
                table: "AdayBasvuruBilgileri");

            migrationBuilder.AddColumn<byte[]>(
                name: "DereceId",
                table: "Mulakatlar",
                type: "varbinary(16)",
                nullable: false,
                defaultValue: new byte[] {  });

            migrationBuilder.AddColumn<byte[]>(
                name: "MulakatId",
                table: "Komisyonlar",
                type: "varbinary(16)",
                nullable: false,
                defaultValue: new byte[] {  });

            migrationBuilder.CreateIndex(
                name: "IX_Mulakatlar_DereceId",
                table: "Mulakatlar",
                column: "DereceId");

            migrationBuilder.CreateIndex(
                name: "IX_Komisyonlar_MulakatId",
                table: "Komisyonlar",
                column: "MulakatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Komisyonlar_Mulakatlar_MulakatId",
                table: "Komisyonlar",
                column: "MulakatId",
                principalTable: "Mulakatlar",
                principalColumn: "MulakatId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Mulakatlar_SoruDereceler_DereceId",
                table: "Mulakatlar",
                column: "DereceId",
                principalTable: "SoruDereceler",
                principalColumn: "DereceId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
