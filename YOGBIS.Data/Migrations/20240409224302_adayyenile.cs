using Microsoft.EntityFrameworkCore.Migrations;

namespace YOGBIS.Data.Migrations
{
    public partial class adayyenile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Yasi",
                table: "Adaylar",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "TYSKomisyonSayi",
                table: "Adaylar",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "MYSSKomisyonSiraNo",
                table: "Adaylar",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "KomisyonSN",
                table: "Adaylar",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "KomisyonGunSN",
                table: "Adaylar",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "CagriDurum",
                table: "Adaylar",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "KabulDurum",
                table: "Adaylar",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SinavDurum",
                table: "Adaylar",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CagriDurum",
                table: "Adaylar");

            migrationBuilder.DropColumn(
                name: "KabulDurum",
                table: "Adaylar");

            migrationBuilder.DropColumn(
                name: "SinavDurum",
                table: "Adaylar");

            migrationBuilder.AlterColumn<int>(
                name: "Yasi",
                table: "Adaylar",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TYSKomisyonSayi",
                table: "Adaylar",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MYSSKomisyonSiraNo",
                table: "Adaylar",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "KomisyonSN",
                table: "Adaylar",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "KomisyonGunSN",
                table: "Adaylar",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
