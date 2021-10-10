using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace YOGBIS.Data.Migrations
{
    public partial class yenitabloekleme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Okullars",
                columns: table => new
                {
                    OkulId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    OkulKodu = table.Column<int>(nullable: false),
                    OkulAdi = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Okullars", x => x.OkulId);
                });

            migrationBuilder.CreateTable(
                name: "OkulBilgis",
                columns: table => new
                {
                    OkulBilgiId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    OkulTelefon = table.Column<string>(nullable: true),
                    YoneticiGorev = table.Column<string>(nullable: true),
                    YoneticiAdiSoyadi = table.Column<string>(nullable: true),
                    YoneticiTelefon = table.Column<string>(nullable: true),
                    OkulId = table.Column<int>(nullable: false),
                    UlkeId = table.Column<int>(nullable: false),
                    KullaniciId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OkulBilgis", x => x.OkulBilgiId);
                    table.ForeignKey(
                        name: "FK_OkulBilgis_AspNetUsers_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OkulBilgis_Okullars_OkulId",
                        column: x => x.OkulId,
                        principalTable: "Okullars",
                        principalColumn: "OkulId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OkulBilgis_Ulkelers_UlkeId",
                        column: x => x.UlkeId,
                        principalTable: "Ulkelers",
                        principalColumn: "UlkeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OkulBilgis_KullaniciId",
                table: "OkulBilgis",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_OkulBilgis_OkulId",
                table: "OkulBilgis",
                column: "OkulId");

            migrationBuilder.CreateIndex(
                name: "IX_OkulBilgis_UlkeId",
                table: "OkulBilgis",
                column: "UlkeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OkulBilgis");

            migrationBuilder.DropTable(
                name: "Okullars");
        }
    }
}
