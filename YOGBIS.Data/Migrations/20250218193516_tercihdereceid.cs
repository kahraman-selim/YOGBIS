using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YOGBIS.Data.Migrations
{
    public partial class tercihdereceid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "DereceId",
                table: "UlkeTercih",
                nullable: false,
                defaultValue: new byte[] {  });

            migrationBuilder.AddForeignKey(
                name: "FK_UlkeTercih_SoruDereceler_MulakatId",
                table: "UlkeTercih",
                column: "MulakatId",
                principalTable: "SoruDereceler",
                principalColumn: "DereceId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UlkeTercih_SoruDereceler_MulakatId",
                table: "UlkeTercih");

            migrationBuilder.DropColumn(
                name: "DereceId",
                table: "UlkeTercih");
        }
    }
}
