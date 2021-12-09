using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace YOGBIS.Data.Migrations
{
    public partial class notlar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notlars",
                columns: table => new
                {
                    NotId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    NotAdi = table.Column<string>(nullable: true),
                    NotDetay = table.Column<string>(nullable: true),
                    BaTarihi = table.Column<DateTime>(nullable: false),
                    BiTarihi = table.Column<DateTime>(nullable: false),
                    NotRenk = table.Column<string>(nullable: true),
                    KullaniciId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notlars", x => x.NotId);
                    table.ForeignKey(
                        name: "FK_Notlars_AspNetUsers_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notlars_KullaniciId",
                table: "Notlars",
                column: "KullaniciId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notlars");
        }
    }
}
