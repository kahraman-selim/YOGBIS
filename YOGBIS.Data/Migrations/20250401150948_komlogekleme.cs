using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YOGBIS.Data.Migrations
{
    public partial class komlogekleme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KomisyonLog",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    KomisyonId = table.Column<byte[]>(nullable: false),
                    KomisyonUyeSiraNo = table.Column<int>(nullable: false),
                    KomisyonUyeAdSoyad = table.Column<string>(nullable: true),
                    GorevBaslamaTarihi = table.Column<DateTime>(nullable: false),
                    GorevBitisTarihi = table.Column<DateTime>(nullable: false),
                    KaydedenId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KomisyonLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KomisyonLog_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KomisyonLog_Komisyonlar_KomisyonId",
                        column: x => x.KomisyonId,
                        principalTable: "Komisyonlar",
                        principalColumn: "KomisyonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KomisyonLog_KaydedenId",
                table: "KomisyonLog",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_KomisyonLog_KomisyonId",
                table: "KomisyonLog",
                column: "KomisyonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KomisyonLog");
        }
    }
}
