using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YOGBIS.Data.Migrations
{
    public partial class alanguncelleme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "KararSayisi",
                table: "Mulakatlars",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "KararTarihi",
                table: "Mulakatlars",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "OnayTarihi",
                table: "Mulakatlars",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "TcKimlikNo",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "YabanciDilBelgesi",
                table: "Adaylars",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KararSayisi",
                table: "Mulakatlars");

            migrationBuilder.DropColumn(
                name: "KararTarihi",
                table: "Mulakatlars");

            migrationBuilder.DropColumn(
                name: "OnayTarihi",
                table: "Mulakatlars");

            migrationBuilder.DropColumn(
                name: "YabanciDilBelgesi",
                table: "Adaylars");

            migrationBuilder.AlterColumn<int>(
                name: "TcKimlikNo",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
