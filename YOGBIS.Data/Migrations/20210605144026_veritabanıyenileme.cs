using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace YOGBIS.Data.Migrations
{
    public partial class veritabanıyenileme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SoruKategoris_Kategoriler_KategorilerKategoriId",
                table: "SoruKategoris");

            migrationBuilder.DropTable(
                name: "Kategoriler");

            migrationBuilder.DropIndex(
                name: "IX_SoruKategoris_KategorilerKategoriId",
                table: "SoruKategoris");

            migrationBuilder.DropColumn(
                name: "KategorilerKategoriId",
                table: "SoruKategoris");

            migrationBuilder.DropColumn(
                name: "KategoriId",
                table: "MulakatSorularis");

            migrationBuilder.AddColumn<int>(
                name: "SoruKategorilerSoruKategoriId",
                table: "SoruKategoris",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SoruKategoriAdi",
                table: "MulakatSorularis",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SoruKategoriId",
                table: "MulakatSorularis",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SoruKategoriler",
                columns: table => new
                {
                    SoruKategoriId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    SoruKategoriAdi = table.Column<string>(nullable: true),
                    SoruKategoriKullanimi = table.Column<string>(nullable: true),
                    SoruKategoriPuan = table.Column<int>(nullable: false),
                    SoruKategoriDerece = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoruKategoriler", x => x.SoruKategoriId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SoruKategoris_SoruKategorilerSoruKategoriId",
                table: "SoruKategoris",
                column: "SoruKategorilerSoruKategoriId");

            migrationBuilder.AddForeignKey(
                name: "FK_SoruKategoris_SoruKategoriler_SoruKategorilerSoruKategoriId",
                table: "SoruKategoris",
                column: "SoruKategorilerSoruKategoriId",
                principalTable: "SoruKategoriler",
                principalColumn: "SoruKategoriId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SoruKategoris_SoruKategoriler_SoruKategorilerSoruKategoriId",
                table: "SoruKategoris");

            migrationBuilder.DropTable(
                name: "SoruKategoriler");

            migrationBuilder.DropIndex(
                name: "IX_SoruKategoris_SoruKategorilerSoruKategoriId",
                table: "SoruKategoris");

            migrationBuilder.DropColumn(
                name: "SoruKategorilerSoruKategoriId",
                table: "SoruKategoris");

            migrationBuilder.DropColumn(
                name: "SoruKategoriAdi",
                table: "MulakatSorularis");

            migrationBuilder.DropColumn(
                name: "SoruKategoriId",
                table: "MulakatSorularis");

            migrationBuilder.AddColumn<int>(
                name: "KategorilerKategoriId",
                table: "SoruKategoris",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "KategoriId",
                table: "MulakatSorularis",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Kategoriler",
                columns: table => new
                {
                    KategoriId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    KategoriAdi = table.Column<string>(type: "text", nullable: true),
                    KategoriDerece = table.Column<string>(type: "text", nullable: true),
                    KategoriKullanimi = table.Column<string>(type: "text", nullable: true),
                    KategoriPuan = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategoriler", x => x.KategoriId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SoruKategoris_KategorilerKategoriId",
                table: "SoruKategoris",
                column: "KategorilerKategoriId");

            migrationBuilder.AddForeignKey(
                name: "FK_SoruKategoris_Kategoriler_KategorilerKategoriId",
                table: "SoruKategoris",
                column: "KategorilerKategoriId",
                principalTable: "Kategoriler",
                principalColumn: "KategoriId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
