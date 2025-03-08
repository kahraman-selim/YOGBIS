using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YOGBIS.Data.Migrations
{
    public partial class adaybilgiformu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BelgeEk1",
                table: "AdayBasvuruBilgileri");

            migrationBuilder.AddColumn<byte[]>(
                name: "BilgiFormu",
                table: "AdayBasvuruBilgileri",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BilgiFormu",
                table: "AdayBasvuruBilgileri");

            migrationBuilder.AddColumn<byte[]>(
                name: "BelgeEk1",
                table: "AdayBasvuruBilgileri",
                type: "varbinary(4000)",
                nullable: true);
        }
    }
}
