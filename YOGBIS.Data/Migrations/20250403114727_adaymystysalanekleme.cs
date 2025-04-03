using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YOGBIS.Data.Migrations
{
    public partial class adaymystysalanekleme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SinavBaslamaTarihi",
                table: "AdayTYS",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SinavBitisTarihi",
                table: "AdayTYS",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SinavYapildi",
                table: "AdayTYS",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SinavBaslamaTarihi",
                table: "AdayMYSS",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SinavBitisTarihi",
                table: "AdayMYSS",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SinavYapildi",
                table: "AdayMYSS",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SinavBaslamaTarihi",
                table: "AdayTYS");

            migrationBuilder.DropColumn(
                name: "SinavBitisTarihi",
                table: "AdayTYS");

            migrationBuilder.DropColumn(
                name: "SinavYapildi",
                table: "AdayTYS");

            migrationBuilder.DropColumn(
                name: "SinavBaslamaTarihi",
                table: "AdayMYSS");

            migrationBuilder.DropColumn(
                name: "SinavBitisTarihi",
                table: "AdayMYSS");

            migrationBuilder.DropColumn(
                name: "SinavYapildi",
                table: "AdayMYSS");
        }
    }
}
