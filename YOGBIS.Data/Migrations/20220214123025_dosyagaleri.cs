using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace YOGBIS.Data.Migrations
{
    public partial class dosyagaleri : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EtkinlikDosyaUrl",
                table: "Etkinliklers");

            migrationBuilder.AddColumn<int>(
                name: "TemsilcilikId",
                table: "Etkinliklers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DosyaGaleris",
                columns: table => new
                {
                    DosyaGaleriId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    DosyaAdi = table.Column<string>(nullable: true),
                    DosyaURL = table.Column<string>(nullable: true),
                    KaydedenId = table.Column<string>(nullable: true),
                    EtkinliklerEtkinlikId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DosyaGaleris", x => x.DosyaGaleriId);
                    table.ForeignKey(
                        name: "FK_DosyaGaleris_Etkinliklers_EtkinliklerEtkinlikId",
                        column: x => x.EtkinliklerEtkinlikId,
                        principalTable: "Etkinliklers",
                        principalColumn: "EtkinlikId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DosyaGaleris_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Etkinliklers_TemsilcilikId",
                table: "Etkinliklers",
                column: "TemsilcilikId");

            migrationBuilder.CreateIndex(
                name: "IX_DosyaGaleris_EtkinliklerEtkinlikId",
                table: "DosyaGaleris",
                column: "EtkinliklerEtkinlikId");

            migrationBuilder.CreateIndex(
                name: "IX_DosyaGaleris_KaydedenId",
                table: "DosyaGaleris",
                column: "KaydedenId");

            migrationBuilder.AddForeignKey(
                name: "FK_Etkinliklers_Temsilciliklers_TemsilcilikId",
                table: "Etkinliklers",
                column: "TemsilcilikId",
                principalTable: "Temsilciliklers",
                principalColumn: "TemsilcilikId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Etkinliklers_Temsilciliklers_TemsilcilikId",
                table: "Etkinliklers");

            migrationBuilder.DropTable(
                name: "DosyaGaleris");

            migrationBuilder.DropIndex(
                name: "IX_Etkinliklers_TemsilcilikId",
                table: "Etkinliklers");

            migrationBuilder.DropColumn(
                name: "TemsilcilikId",
                table: "Etkinliklers");

            migrationBuilder.AddColumn<string>(
                name: "EtkinlikDosyaUrl",
                table: "Etkinliklers",
                type: "text",
                nullable: true);
        }
    }
}
