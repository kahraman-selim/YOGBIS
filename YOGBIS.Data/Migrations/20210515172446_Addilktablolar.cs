using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace YOGBIS.Data.Migrations
{
    public partial class Addilktablolar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UlkeGruplaris",
                columns: table => new
                {
                    UlkeGrupId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UlkeGrupAdi = table.Column<string>(nullable: true),
                    UlkeGrupAciklama = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UlkeGruplaris", x => x.UlkeGrupId);
                });

            migrationBuilder.CreateTable(
                name: "Kitalars",
                columns: table => new
                {
                    KitaId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    KitaAdi = table.Column<string>(nullable: true),
                    KitaAciklama = table.Column<string>(nullable: true),
                    UlkeGrupId = table.Column<int>(nullable: false),
                    UlkeGruplariUlkeGrupId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kitalars", x => x.KitaId);
                    table.ForeignKey(
                        name: "FK_Kitalars_UlkeGruplaris_UlkeGruplariUlkeGrupId",
                        column: x => x.UlkeGruplariUlkeGrupId,
                        principalTable: "UlkeGruplaris",
                        principalColumn: "UlkeGrupId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ulkelers",
                columns: table => new
                {
                    UlkeId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UlkeAdi = table.Column<string>(nullable: true),
                    UlkeBayrak = table.Column<string>(nullable: true),
                    UlkeAciklama = table.Column<string>(nullable: true),
                    KitaId = table.Column<int>(nullable: false),
                    KitalarKitaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ulkelers", x => x.UlkeId);
                    table.ForeignKey(
                        name: "FK_Ulkelers_Kitalars_KitalarKitaId",
                        column: x => x.KitalarKitaId,
                        principalTable: "Kitalars",
                        principalColumn: "KitaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Eyaletlers",
                columns: table => new
                {
                    EyaletId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    EyaletAdi = table.Column<string>(nullable: true),
                    EyaletAciklama = table.Column<string>(nullable: true),
                    UlkeId = table.Column<int>(nullable: false),
                    UlkelerUlkeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eyaletlers", x => x.EyaletId);
                    table.ForeignKey(
                        name: "FK_Eyaletlers_Ulkelers_UlkelerUlkeId",
                        column: x => x.UlkelerUlkeId,
                        principalTable: "Ulkelers",
                        principalColumn: "UlkeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sehirlers",
                columns: table => new
                {
                    SehirId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    SehirAdi = table.Column<string>(nullable: true),
                    Baskent = table.Column<bool>(nullable: true),
                    SehirAciklama = table.Column<string>(nullable: true),
                    EyaletId = table.Column<int>(nullable: false),
                    EyaletlerEyaletId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sehirlers", x => x.SehirId);
                    table.ForeignKey(
                        name: "FK_Sehirlers_Eyaletlers_EyaletlerEyaletId",
                        column: x => x.EyaletlerEyaletId,
                        principalTable: "Eyaletlers",
                        principalColumn: "EyaletId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Eyaletlers_UlkelerUlkeId",
                table: "Eyaletlers",
                column: "UlkelerUlkeId");

            migrationBuilder.CreateIndex(
                name: "IX_Kitalars_UlkeGruplariUlkeGrupId",
                table: "Kitalars",
                column: "UlkeGruplariUlkeGrupId");

            migrationBuilder.CreateIndex(
                name: "IX_Sehirlers_EyaletlerEyaletId",
                table: "Sehirlers",
                column: "EyaletlerEyaletId");

            migrationBuilder.CreateIndex(
                name: "IX_Ulkelers_KitalarKitaId",
                table: "Ulkelers",
                column: "KitalarKitaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sehirlers");

            migrationBuilder.DropTable(
                name: "Eyaletlers");

            migrationBuilder.DropTable(
                name: "Ulkelers");

            migrationBuilder.DropTable(
                name: "Kitalars");

            migrationBuilder.DropTable(
                name: "UlkeGruplaris");
        }
    }
}
