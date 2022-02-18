using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YOGBIS.Data.Migrations
{
    public partial class okulalangun : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "OkulMulkiDurum",
                table: "Okullars",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "OkulDurumu",
                table: "Okullars",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "OkulAcilisTarihi",
                table: "Okullars",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "OkulMulkiDurum",
                table: "Okullars",
                type: "tinyint(1)",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "OkulDurumu",
                table: "Okullars",
                type: "tinyint(1)",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OkulAcilisTarihi",
                table: "Okullars",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime));
        }
    }
}
