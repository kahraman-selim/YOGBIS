using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YOGBIS.Data.Migrations
{
    public partial class yenileme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Okullar_Sehirler_SehirId",
                table: "Okullar");

            migrationBuilder.DropIndex(
                name: "IX_Okullar_SehirId",
                table: "Okullar");

            migrationBuilder.AlterColumn<byte[]>(
                name: "SehirId",
                table: "Okullar",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(16)");

            migrationBuilder.AddColumn<byte[]>(
                name: "SehirlerSehirId",
                table: "Okullar",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Okullar_SehirlerSehirId",
                table: "Okullar",
                column: "SehirlerSehirId");

            migrationBuilder.AddForeignKey(
                name: "FK_Okullar_Sehirler_SehirlerSehirId",
                table: "Okullar",
                column: "SehirlerSehirId",
                principalTable: "Sehirler",
                principalColumn: "SehirId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Okullar_Sehirler_SehirlerSehirId",
                table: "Okullar");

            migrationBuilder.DropIndex(
                name: "IX_Okullar_SehirlerSehirId",
                table: "Okullar");

            migrationBuilder.DropColumn(
                name: "SehirlerSehirId",
                table: "Okullar");

            migrationBuilder.AlterColumn<byte[]>(
                name: "SehirId",
                table: "Okullar",
                type: "varbinary(16)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Okullar_SehirId",
                table: "Okullar",
                column: "SehirId");

            migrationBuilder.AddForeignKey(
                name: "FK_Okullar_Sehirler_SehirId",
                table: "Okullar",
                column: "SehirId",
                principalTable: "Sehirler",
                principalColumn: "SehirId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
