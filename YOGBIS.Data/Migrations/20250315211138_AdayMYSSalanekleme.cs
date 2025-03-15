using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YOGBIS.Data.Migrations
{
    public partial class AdayMYSSalanekleme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BransAdi",
                table: "AdayMYSS",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "BransId",
                table: "AdayMYSS",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DereceAdi",
                table: "AdayMYSS",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "DereceId",
                table: "AdayMYSS",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UlkeTercihAdi",
                table: "AdayMYSS",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "UlkeTercihId",
                table: "AdayMYSS",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BransAdi",
                table: "AdayMYSS");

            migrationBuilder.DropColumn(
                name: "BransId",
                table: "AdayMYSS");

            migrationBuilder.DropColumn(
                name: "DereceAdi",
                table: "AdayMYSS");

            migrationBuilder.DropColumn(
                name: "DereceId",
                table: "AdayMYSS");

            migrationBuilder.DropColumn(
                name: "UlkeTercihAdi",
                table: "AdayMYSS");

            migrationBuilder.DropColumn(
                name: "UlkeTercihId",
                table: "AdayMYSS");
        }
    }
}
