using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace YOGBIS.Data.Migrations
{
    public partial class AddYeniTblolar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kategoriler",
                columns: table => new
                {
                    KategoriId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    KategoriAdi = table.Column<string>(nullable: true),
                    KategoriKullanimi = table.Column<string>(nullable: true),
                    KategoriPuan = table.Column<int>(nullable: false),
                    KategoriDerece = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategoriler", x => x.KategoriId);
                });

            migrationBuilder.CreateTable(
                name: "Mulakatlars",
                columns: table => new
                {
                    MulakatId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    OnaySayisi = table.Column<string>(nullable: true),
                    MulakatAdi = table.Column<string>(nullable: true),
                    Derecesi = table.Column<string>(nullable: true),
                    BaslamaTarihi = table.Column<DateTime>(nullable: false),
                    BitisTarihi = table.Column<DateTime>(nullable: false),
                    AdaySayisi = table.Column<int>(nullable: false),
                    SorulanSoruSayisi = table.Column<int>(nullable: false),
                    Durumu = table.Column<string>(nullable: true),
                    MulakatAciklama = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mulakatlars", x => x.MulakatId);
                });

            migrationBuilder.CreateTable(
                name: "SoruBankasis",
                columns: table => new
                {
                    SoruBankasiId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    SoruKategoriId = table.Column<int>(nullable: false),
                    Soru = table.Column<string>(nullable: true),
                    Cevap = table.Column<string>(nullable: true),
                    Derecesi = table.Column<string>(nullable: true),
                    SorulmaSayisi = table.Column<int>(nullable: false),
                    SoruDurumu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoruBankasis", x => x.SoruBankasiId);
                });

            migrationBuilder.CreateTable(
                name: "MulakatSorularis",
                columns: table => new
                {
                    MulakatSorulariId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    SoruSiraNo = table.Column<int>(nullable: false),
                    SoruNo = table.Column<int>(nullable: false),
                    KategoriId = table.Column<int>(nullable: false),
                    Derecesi = table.Column<string>(nullable: true),
                    Soru = table.Column<string>(nullable: true),
                    Cevap = table.Column<string>(nullable: true),
                    MulakatId = table.Column<int>(nullable: false),
                    MulakatlarMulakatId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MulakatSorularis", x => x.MulakatSorulariId);
                    table.ForeignKey(
                        name: "FK_MulakatSorularis_Mulakatlars_MulakatlarMulakatId",
                        column: x => x.MulakatlarMulakatId,
                        principalTable: "Mulakatlars",
                        principalColumn: "MulakatId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SoruBankasiLogs",
                columns: table => new
                {
                    SoruBankasiLogId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Soru = table.Column<string>(nullable: true),
                    Cevap = table.Column<string>(nullable: true),
                    KullaniciAdi = table.Column<string>(nullable: true),
                    Islem = table.Column<string>(nullable: true),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    SoruBankasiId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoruBankasiLogs", x => x.SoruBankasiLogId);
                    table.ForeignKey(
                        name: "FK_SoruBankasiLogs_SoruBankasis_SoruBankasiId",
                        column: x => x.SoruBankasiId,
                        principalTable: "SoruBankasis",
                        principalColumn: "SoruBankasiId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SoruKategoris",
                columns: table => new
                {
                    SoruKategoriId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    SoruId = table.Column<int>(nullable: false),
                    SoruBankasiId = table.Column<int>(nullable: true),
                    KategoriId = table.Column<int>(nullable: false),
                    KategorilerKategoriId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoruKategoris", x => x.SoruKategoriId);
                    table.ForeignKey(
                        name: "FK_SoruKategoris_Kategoriler_KategorilerKategoriId",
                        column: x => x.KategorilerKategoriId,
                        principalTable: "Kategoriler",
                        principalColumn: "KategoriId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SoruKategoris_SoruBankasis_SoruBankasiId",
                        column: x => x.SoruBankasiId,
                        principalTable: "SoruBankasis",
                        principalColumn: "SoruBankasiId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MulakatSorularis_MulakatlarMulakatId",
                table: "MulakatSorularis",
                column: "MulakatlarMulakatId");

            migrationBuilder.CreateIndex(
                name: "IX_SoruBankasiLogs_SoruBankasiId",
                table: "SoruBankasiLogs",
                column: "SoruBankasiId");

            migrationBuilder.CreateIndex(
                name: "IX_SoruKategoris_KategorilerKategoriId",
                table: "SoruKategoris",
                column: "KategorilerKategoriId");

            migrationBuilder.CreateIndex(
                name: "IX_SoruKategoris_SoruBankasiId",
                table: "SoruKategoris",
                column: "SoruBankasiId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MulakatSorularis");

            migrationBuilder.DropTable(
                name: "SoruBankasiLogs");

            migrationBuilder.DropTable(
                name: "SoruKategoris");

            migrationBuilder.DropTable(
                name: "Mulakatlars");

            migrationBuilder.DropTable(
                name: "Kategoriler");

            migrationBuilder.DropTable(
                name: "SoruBankasis");
        }
    }
}
