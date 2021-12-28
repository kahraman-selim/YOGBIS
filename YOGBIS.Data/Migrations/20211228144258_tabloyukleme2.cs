using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace YOGBIS.Data.Migrations
{
    public partial class tabloyukleme2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OkulBilgis",
                columns: table => new
                {
                    OkulBilgiId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    OkulTelefon = table.Column<string>(nullable: true),
                    OkulAdres = table.Column<string>(nullable: true),
                    MudurAdiSoyadi = table.Column<string>(nullable: true),
                    MudurTelefon = table.Column<string>(nullable: true),
                    MudurEPosta = table.Column<string>(nullable: true),
                    MudurDonusYil = table.Column<string>(nullable: true),
                    MdYrdAdiSoyadi = table.Column<string>(nullable: true),
                    MdYrdTelefon = table.Column<string>(nullable: true),
                    MdYrdEPosta = table.Column<string>(nullable: true),
                    MdYrdDonusYil = table.Column<string>(nullable: true),
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
        }
    }
}
