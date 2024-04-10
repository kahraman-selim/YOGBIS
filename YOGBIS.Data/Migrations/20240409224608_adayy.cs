using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YOGBIS.Data.Migrations
{
    public partial class adayy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adaylar_Komisyonlar_KomisyonId",
                table: "Adaylar");

            migrationBuilder.DropForeignKey(
                name: "FK_Adaylar_Mulakatlar_MulakatId",
                table: "Adaylar");

            migrationBuilder.AlterColumn<byte[]>(
                name: "MulakatId",
                table: "Adaylar",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(16)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "KomisyonId",
                table: "Adaylar",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(16)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "DereceId",
                table: "Adaylar",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(16)");

            migrationBuilder.AddForeignKey(
                name: "FK_Adaylar_Komisyonlar_KomisyonId",
                table: "Adaylar",
                column: "KomisyonId",
                principalTable: "Komisyonlar",
                principalColumn: "KomisyonId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Adaylar_Mulakatlar_MulakatId",
                table: "Adaylar",
                column: "MulakatId",
                principalTable: "Mulakatlar",
                principalColumn: "MulakatId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adaylar_Komisyonlar_KomisyonId",
                table: "Adaylar");

            migrationBuilder.DropForeignKey(
                name: "FK_Adaylar_Mulakatlar_MulakatId",
                table: "Adaylar");

            migrationBuilder.AlterColumn<byte[]>(
                name: "MulakatId",
                table: "Adaylar",
                type: "varbinary(16)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "KomisyonId",
                table: "Adaylar",
                type: "varbinary(16)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "DereceId",
                table: "Adaylar",
                type: "varbinary(16)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Adaylar_Komisyonlar_KomisyonId",
                table: "Adaylar",
                column: "KomisyonId",
                principalTable: "Komisyonlar",
                principalColumn: "KomisyonId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Adaylar_Mulakatlar_MulakatId",
                table: "Adaylar",
                column: "MulakatId",
                principalTable: "Mulakatlar",
                principalColumn: "MulakatId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
