using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YOGBIS.Data.Migrations
{
    public partial class komperdeg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KomisyonPersoneller_Komisyonlar_KomisyonId",
                table: "KomisyonPersoneller");

            migrationBuilder.AlterColumn<byte[]>(
                name: "KomisyonId",
                table: "KomisyonPersoneller",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(16)");

            migrationBuilder.AddColumn<string>(
                name: "KomisyonKullaniciId",
                table: "KomisyonPersoneller",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_KomisyonPersoneller_Komisyonlar_KomisyonId",
                table: "KomisyonPersoneller",
                column: "KomisyonId",
                principalTable: "Komisyonlar",
                principalColumn: "KomisyonId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KomisyonPersoneller_Komisyonlar_KomisyonId",
                table: "KomisyonPersoneller");

            migrationBuilder.DropColumn(
                name: "KomisyonKullaniciId",
                table: "KomisyonPersoneller");

            migrationBuilder.AlterColumn<byte[]>(
                name: "KomisyonId",
                table: "KomisyonPersoneller",
                type: "varbinary(16)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_KomisyonPersoneller_Komisyonlar_KomisyonId",
                table: "KomisyonPersoneller",
                column: "KomisyonId",
                principalTable: "Komisyonlar",
                principalColumn: "KomisyonId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
