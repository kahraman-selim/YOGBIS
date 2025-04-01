using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YOGBIS.Data.Migrations
{
    public partial class komisyonperekleme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KomisyonPersoneller",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    KomisyonId = table.Column<byte[]>(nullable: false),
                    PersonelId = table.Column<byte[]>(nullable: false),
                    RolId = table.Column<byte[]>(nullable: false),
                    KaydedenId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KomisyonPersoneller", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KomisyonPersoneller_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KomisyonPersoneller_Komisyonlar_KomisyonId",
                        column: x => x.KomisyonId,
                        principalTable: "Komisyonlar",
                        principalColumn: "KomisyonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KomisyonPersoneller_KaydedenId",
                table: "KomisyonPersoneller",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_KomisyonPersoneller_KomisyonId",
                table: "KomisyonPersoneller",
                column: "KomisyonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KomisyonPersoneller");
        }
    }
}
