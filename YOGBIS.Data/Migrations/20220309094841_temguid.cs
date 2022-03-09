using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YOGBIS.Data.Migrations
{
    public partial class temguid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "TemsilcilikId",
                table: "Okullar",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(16)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "TemsilcilikId",
                table: "Okullar",
                type: "varbinary(16)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldNullable: true);
        }
    }
}
