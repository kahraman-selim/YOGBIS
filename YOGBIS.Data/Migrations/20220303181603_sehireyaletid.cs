using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YOGBIS.Data.Migrations
{
    public partial class sehireyaletid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sehirlers_Eyaletlers_EyaletId",
                table: "Sehirlers");

            migrationBuilder.AlterColumn<byte[]>(
                name: "EyaletId",
                table: "Sehirlers",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(16)");

            migrationBuilder.AddForeignKey(
                name: "FK_Sehirlers_Eyaletlers_EyaletId",
                table: "Sehirlers",
                column: "EyaletId",
                principalTable: "Eyaletlers",
                principalColumn: "EyaletId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sehirlers_Eyaletlers_EyaletId",
                table: "Sehirlers");

            migrationBuilder.AlterColumn<byte[]>(
                name: "EyaletId",
                table: "Sehirlers",
                type: "varbinary(16)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Sehirlers_Eyaletlers_EyaletId",
                table: "Sehirlers",
                column: "EyaletId",
                principalTable: "Eyaletlers",
                principalColumn: "EyaletId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
