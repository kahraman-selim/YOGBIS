using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YOGBIS.Data.Migrations
{
    public partial class adayiletisimadayıdguncelleme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdayIletisimBilgileri_Adaylar_AdayId",
                table: "AdayIletisimBilgileri");

            migrationBuilder.DropIndex(
                name: "IX_AdayIletisimBilgileri_AdayId",
                table: "AdayIletisimBilgileri");

            migrationBuilder.DropColumn(
                name: "AdayId",
                table: "AdayIletisimBilgileri");

            migrationBuilder.AddColumn<byte[]>(
                name: "AdaylarAdayId",
                table: "AdayIletisimBilgileri",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AdayIletisimBilgileri_AdaylarAdayId",
                table: "AdayIletisimBilgileri",
                column: "AdaylarAdayId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdayIletisimBilgileri_Adaylar_AdaylarAdayId",
                table: "AdayIletisimBilgileri",
                column: "AdaylarAdayId",
                principalTable: "Adaylar",
                principalColumn: "AdayId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdayIletisimBilgileri_Adaylar_AdaylarAdayId",
                table: "AdayIletisimBilgileri");

            migrationBuilder.DropIndex(
                name: "IX_AdayIletisimBilgileri_AdaylarAdayId",
                table: "AdayIletisimBilgileri");

            migrationBuilder.DropColumn(
                name: "AdaylarAdayId",
                table: "AdayIletisimBilgileri");

            migrationBuilder.AddColumn<byte[]>(
                name: "AdayId",
                table: "AdayIletisimBilgileri",
                type: "varbinary(16)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AdayIletisimBilgileri_AdayId",
                table: "AdayIletisimBilgileri",
                column: "AdayId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdayIletisimBilgileri_Adaylar_AdayId",
                table: "AdayIletisimBilgileri",
                column: "AdayId",
                principalTable: "Adaylar",
                principalColumn: "AdayId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
