using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YOGBIS.Data.Migrations
{
    public partial class AdayTYSalanekleme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BransAdi",
                table: "AdayTYS",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "BransId",
                table: "AdayTYS",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DereceAdi",
                table: "AdayTYS",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "DereceId",
                table: "AdayTYS",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UlkeTercihAdi",
                table: "AdayTYS",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "UlkeTercihId",
                table: "AdayTYS",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BransAdi",
                table: "AdayTYS");

            migrationBuilder.DropColumn(
                name: "BransId",
                table: "AdayTYS");

            migrationBuilder.DropColumn(
                name: "DereceAdi",
                table: "AdayTYS");

            migrationBuilder.DropColumn(
                name: "DereceId",
                table: "AdayTYS");

            migrationBuilder.DropColumn(
                name: "UlkeTercihAdi",
                table: "AdayTYS");

            migrationBuilder.DropColumn(
                name: "UlkeTercihId",
                table: "AdayTYS");
        }
    }
}
