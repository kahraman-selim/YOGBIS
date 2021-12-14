using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace YOGBIS.Data.Migrations
{
    public partial class adaylar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DerecelerDereceId",
                table: "Mulakatlars",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Adaylars",
                columns: table => new
                {
                    AdayId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    AdayTC = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adaylars", x => x.AdayId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mulakatlars_DerecelerDereceId",
                table: "Mulakatlars",
                column: "DerecelerDereceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mulakatlars_Derecelers_DerecelerDereceId",
                table: "Mulakatlars",
                column: "DerecelerDereceId",
                principalTable: "Derecelers",
                principalColumn: "DereceId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mulakatlars_Derecelers_DerecelerDereceId",
                table: "Mulakatlars");

            migrationBuilder.DropTable(
                name: "Adaylars");

            migrationBuilder.DropIndex(
                name: "IX_Mulakatlars_DerecelerDereceId",
                table: "Mulakatlars");

            migrationBuilder.DropColumn(
                name: "DerecelerDereceId",
                table: "Mulakatlars");
        }
    }
}
