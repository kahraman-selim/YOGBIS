using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YOGBIS.Data.Migrations
{
    public partial class okulalanduz : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Okullars_Eyaletlers_EyaletId",
                table: "Okullars");

            migrationBuilder.DropForeignKey(
                name: "FK_Okullars_Sehirlers_SehirId",
                table: "Okullars");

            migrationBuilder.AlterColumn<byte[]>(
                name: "SehirId",
                table: "Okullars",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(16)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "OkulUlkeId",
                table: "Okullars",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(16)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "EyaletId",
                table: "Okullars",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(16)");

            migrationBuilder.AddForeignKey(
                name: "FK_Okullars_Eyaletlers_EyaletId",
                table: "Okullars",
                column: "EyaletId",
                principalTable: "Eyaletlers",
                principalColumn: "EyaletId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Okullars_Sehirlers_SehirId",
                table: "Okullars",
                column: "SehirId",
                principalTable: "Sehirlers",
                principalColumn: "SehirId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Okullars_Eyaletlers_EyaletId",
                table: "Okullars");

            migrationBuilder.DropForeignKey(
                name: "FK_Okullars_Sehirlers_SehirId",
                table: "Okullars");

            migrationBuilder.AlterColumn<byte[]>(
                name: "SehirId",
                table: "Okullars",
                type: "varbinary(16)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "OkulUlkeId",
                table: "Okullars",
                type: "varbinary(16)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "EyaletId",
                table: "Okullars",
                type: "varbinary(16)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Okullars_Eyaletlers_EyaletId",
                table: "Okullars",
                column: "EyaletId",
                principalTable: "Eyaletlers",
                principalColumn: "EyaletId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Okullars_Sehirlers_SehirId",
                table: "Okullars",
                column: "SehirId",
                principalTable: "Sehirlers",
                principalColumn: "SehirId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
