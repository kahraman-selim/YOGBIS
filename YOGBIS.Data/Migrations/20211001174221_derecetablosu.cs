using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace YOGBIS.Data.Migrations
{
    public partial class derecetablosu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Derecelers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    DereceAdi = table.Column<string>(nullable: true),
                    SoruKategorilerId = table.Column<int>(nullable: false),
                    KullaniciId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Derecelers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Derecelers_AspNetUsers_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Derecelers_SoruKategorilers_SoruKategorilerId",
                        column: x => x.SoruKategorilerId,
                        principalTable: "SoruKategorilers",
                        principalColumn: "SoruKategorilerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Derecelers_KullaniciId",
                table: "Derecelers",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_Derecelers_SoruKategorilerId",
                table: "Derecelers",
                column: "SoruKategorilerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Derecelers");
        }
    }
}
