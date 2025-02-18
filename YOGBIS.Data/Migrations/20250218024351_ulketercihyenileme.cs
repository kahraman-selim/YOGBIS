using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YOGBIS.Data.Migrations
{
    public partial class ulketercihyenileme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DereceId",
                table: "UlkeTercih");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "DereceId",
                table: "UlkeTercih",
                type: "varbinary(16)",
                nullable: false,
                defaultValue: new byte[] {  });
        }
    }
}
