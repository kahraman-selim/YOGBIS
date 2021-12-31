using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace YOGBIS.Data.Migrations
{
    public partial class fotogaleri : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FotoGaleri",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    URL = table.Column<string>(nullable: true),
                    AktiviteId = table.Column<int>(nullable: false),
                    AktivitelerAktiviteId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FotoGaleri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FotoGaleri_Aktivitelers_AktivitelerAktiviteId",
                        column: x => x.AktivitelerAktiviteId,
                        principalTable: "Aktivitelers",
                        principalColumn: "AktiviteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FotoGaleri_AktivitelerAktiviteId",
                table: "FotoGaleri",
                column: "AktivitelerAktiviteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FotoGaleri");
        }
    }
}
