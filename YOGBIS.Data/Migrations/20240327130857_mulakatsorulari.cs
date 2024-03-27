using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YOGBIS.Data.Migrations
{
    public partial class mulakatsorulari : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MulakatSorulari_Mulakatlar_MulakatId",
                table: "MulakatSorulari");

            migrationBuilder.DropIndex(
                name: "IX_MulakatSorulari_MulakatId",
                table: "MulakatSorulari");

            migrationBuilder.DropColumn(
                name: "DereceAdi",
                table: "MulakatSorulari");

            migrationBuilder.DropColumn(
                name: "DereceId",
                table: "MulakatSorulari");

            migrationBuilder.DropColumn(
                name: "SoruId",
                table: "MulakatSorulari");

            migrationBuilder.DropColumn(
                name: "SoruKategoriAdi",
                table: "MulakatSorulari");

            migrationBuilder.DropColumn(
                name: "SoruKategoriId",
                table: "MulakatSorulari");

            migrationBuilder.DropColumn(
                name: "SorulanAdayTC",
                table: "MulakatSorulari");

            migrationBuilder.AddColumn<string>(
                name: "KategoriAdi",
                table: "MulakatSorulari",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "KategoriID",
                table: "MulakatSorulari",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte[]>(
                name: "MulakatlarMulakatId",
                table: "MulakatSorulari",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SinavKategoriAdi",
                table: "MulakatSorulari",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SinavKategoriID",
                table: "MulakatSorulari",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SoruDereceAdi",
                table: "MulakatSorulari",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SoruDereceId",
                table: "MulakatSorulari",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SoruNo",
                table: "MulakatSorulari",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MulakatSorulari_MulakatlarMulakatId",
                table: "MulakatSorulari",
                column: "MulakatlarMulakatId");

            migrationBuilder.AddForeignKey(
                name: "FK_MulakatSorulari_Mulakatlar_MulakatlarMulakatId",
                table: "MulakatSorulari",
                column: "MulakatlarMulakatId",
                principalTable: "Mulakatlar",
                principalColumn: "MulakatId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MulakatSorulari_Mulakatlar_MulakatlarMulakatId",
                table: "MulakatSorulari");

            migrationBuilder.DropIndex(
                name: "IX_MulakatSorulari_MulakatlarMulakatId",
                table: "MulakatSorulari");

            migrationBuilder.DropColumn(
                name: "KategoriAdi",
                table: "MulakatSorulari");

            migrationBuilder.DropColumn(
                name: "KategoriID",
                table: "MulakatSorulari");

            migrationBuilder.DropColumn(
                name: "MulakatlarMulakatId",
                table: "MulakatSorulari");

            migrationBuilder.DropColumn(
                name: "SinavKategoriAdi",
                table: "MulakatSorulari");

            migrationBuilder.DropColumn(
                name: "SinavKategoriID",
                table: "MulakatSorulari");

            migrationBuilder.DropColumn(
                name: "SoruDereceAdi",
                table: "MulakatSorulari");

            migrationBuilder.DropColumn(
                name: "SoruDereceId",
                table: "MulakatSorulari");

            migrationBuilder.DropColumn(
                name: "SoruNo",
                table: "MulakatSorulari");

            migrationBuilder.AddColumn<string>(
                name: "DereceAdi",
                table: "MulakatSorulari",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "DereceId",
                table: "MulakatSorulari",
                type: "varbinary(16)",
                nullable: false,
                defaultValue: new byte[] {  });

            migrationBuilder.AddColumn<byte[]>(
                name: "SoruId",
                table: "MulakatSorulari",
                type: "varbinary(16)",
                nullable: false,
                defaultValue: new byte[] {  });

            migrationBuilder.AddColumn<string>(
                name: "SoruKategoriAdi",
                table: "MulakatSorulari",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "SoruKategoriId",
                table: "MulakatSorulari",
                type: "varbinary(16)",
                nullable: false,
                defaultValue: new byte[] {  });

            migrationBuilder.AddColumn<int>(
                name: "SorulanAdayTC",
                table: "MulakatSorulari",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MulakatSorulari_MulakatId",
                table: "MulakatSorulari",
                column: "MulakatId");

            migrationBuilder.AddForeignKey(
                name: "FK_MulakatSorulari_Mulakatlar_MulakatId",
                table: "MulakatSorulari",
                column: "MulakatId",
                principalTable: "Mulakatlar",
                principalColumn: "MulakatId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
