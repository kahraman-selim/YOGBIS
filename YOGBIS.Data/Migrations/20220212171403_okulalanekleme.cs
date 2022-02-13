using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace YOGBIS.Data.Migrations
{
    public partial class okulalanekleme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OkulKutuphane",
                table: "Okullars");

            migrationBuilder.DropColumn(
                name: "OkulLab",
                table: "Okullars");

            migrationBuilder.DropColumn(
                name: "OkulLogo",
                table: "Okullars");

            migrationBuilder.AddColumn<string>(
                name: "AcikAlan",
                table: "Okullars",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EPostaAdresi",
                table: "Okullars",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EyaletId",
                table: "Okullars",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "HizmetGecisDonem",
                table: "Okullars",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InternetAdresi",
                table: "Okullars",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KapaliAlan",
                table: "Okullars",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "MulkiDurum",
                table: "Okullars",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "OkulLogoURL",
                table: "Okullars",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OkulTelefon",
                table: "Okullars",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OkulBinaBolumId",
                table: "FotoGaleris",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OkulBinaBolums",
                columns: table => new
                {
                    OkulBinaBolumId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    BolumAdi = table.Column<string>(nullable: true),
                    OkulId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OkulBinaBolums", x => x.OkulBinaBolumId);
                    table.ForeignKey(
                        name: "FK_OkulBinaBolums_Okullars_OkulId",
                        column: x => x.OkulId,
                        principalTable: "Okullars",
                        principalColumn: "OkulId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Okullars_EyaletId",
                table: "Okullars",
                column: "EyaletId");

            migrationBuilder.CreateIndex(
                name: "IX_FotoGaleris_OkulBinaBolumId",
                table: "FotoGaleris",
                column: "OkulBinaBolumId");

            migrationBuilder.CreateIndex(
                name: "IX_OkulBinaBolums_OkulId",
                table: "OkulBinaBolums",
                column: "OkulId");

            migrationBuilder.AddForeignKey(
                name: "FK_FotoGaleris_OkulBinaBolums_OkulBinaBolumId",
                table: "FotoGaleris",
                column: "OkulBinaBolumId",
                principalTable: "OkulBinaBolums",
                principalColumn: "OkulBinaBolumId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Okullars_Eyaletlers_EyaletId",
                table: "Okullars",
                column: "EyaletId",
                principalTable: "Eyaletlers",
                principalColumn: "EyaletId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FotoGaleris_OkulBinaBolums_OkulBinaBolumId",
                table: "FotoGaleris");

            migrationBuilder.DropForeignKey(
                name: "FK_Okullars_Eyaletlers_EyaletId",
                table: "Okullars");

            migrationBuilder.DropTable(
                name: "OkulBinaBolums");

            migrationBuilder.DropIndex(
                name: "IX_Okullars_EyaletId",
                table: "Okullars");

            migrationBuilder.DropIndex(
                name: "IX_FotoGaleris_OkulBinaBolumId",
                table: "FotoGaleris");

            migrationBuilder.DropColumn(
                name: "AcikAlan",
                table: "Okullars");

            migrationBuilder.DropColumn(
                name: "EPostaAdresi",
                table: "Okullars");

            migrationBuilder.DropColumn(
                name: "EyaletId",
                table: "Okullars");

            migrationBuilder.DropColumn(
                name: "HizmetGecisDonem",
                table: "Okullars");

            migrationBuilder.DropColumn(
                name: "InternetAdresi",
                table: "Okullars");

            migrationBuilder.DropColumn(
                name: "KapaliAlan",
                table: "Okullars");

            migrationBuilder.DropColumn(
                name: "MulkiDurum",
                table: "Okullars");

            migrationBuilder.DropColumn(
                name: "OkulLogoURL",
                table: "Okullars");

            migrationBuilder.DropColumn(
                name: "OkulTelefon",
                table: "Okullars");

            migrationBuilder.DropColumn(
                name: "OkulBinaBolumId",
                table: "FotoGaleris");

            migrationBuilder.AddColumn<bool>(
                name: "OkulKutuphane",
                table: "Okullars",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "OkulLab",
                table: "Okullars",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OkulLogo",
                table: "Okullars",
                type: "text",
                nullable: true);
        }
    }
}
