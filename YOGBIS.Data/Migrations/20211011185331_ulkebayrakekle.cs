using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YOGBIS.Data.Migrations
{
    public partial class ulkebayrakekle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UlkeBayrak",
                table: "Ulkelers",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(4000)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "UlkeBayrak",
                table: "Ulkelers",
                type: "varbinary(4000)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
