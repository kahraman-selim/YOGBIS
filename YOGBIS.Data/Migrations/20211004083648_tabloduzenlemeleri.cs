using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace YOGBIS.Data.Migrations
{
    public partial class tabloduzenlemeleri : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Gecmiss");

            migrationBuilder.DropColumn(
                name: "DereceAdi",
                table: "SoruKategorilers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DereceAdi",
                table: "SoruKategorilers",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Gecmiss",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Changed = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    Kind = table.Column<int>(type: "int", nullable: false),
                    KullaniciId = table.Column<string>(type: "varchar(767)", nullable: true),
                    RowId = table.Column<string>(type: "text", nullable: true),
                    TableName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gecmiss", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gecmiss_AspNetUsers_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gecmiss_KullaniciId",
                table: "Gecmiss",
                column: "KullaniciId");
        }
    }
}
