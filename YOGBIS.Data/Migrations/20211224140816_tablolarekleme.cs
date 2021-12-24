using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace YOGBIS.Data.Migrations
{
    public partial class tablolarekleme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adaylars_AspNetUsers_KullaniciId",
                table: "Adaylars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Adaylars",
                table: "Adaylars");

            migrationBuilder.DropIndex(
                name: "IX_Adaylars_KullaniciId",
                table: "Adaylars");

            migrationBuilder.DropColumn(
                name: "AdayId",
                table: "Adaylars");

            migrationBuilder.DropColumn(
                name: "KullaniciId",
                table: "Adaylars");

            migrationBuilder.AddColumn<string>(
                name: "KaydedenId",
                table: "SoruKategorilers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KaydedenId",
                table: "MulakatSorularis",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KaydedenId",
                table: "Mulakatlars",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KaydedenId",
                table: "Derecelers",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "KaydedenId",
                table: "Adaylars",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TcKimlikNo",
                table: "Adaylars",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Adaylars",
                table: "Adaylars",
                columns: new[] { "AdayTC", "KaydedenId" });

            migrationBuilder.CreateIndex(
                name: "IX_Adaylars_TcKimlikNo",
                table: "Adaylars",
                column: "TcKimlikNo");

            migrationBuilder.AddForeignKey(
                name: "FK_Adaylars_AspNetUsers_TcKimlikNo",
                table: "Adaylars",
                column: "TcKimlikNo",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adaylars_AspNetUsers_TcKimlikNo",
                table: "Adaylars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Adaylars",
                table: "Adaylars");

            migrationBuilder.DropIndex(
                name: "IX_Adaylars_TcKimlikNo",
                table: "Adaylars");

            migrationBuilder.DropColumn(
                name: "KaydedenId",
                table: "SoruKategorilers");

            migrationBuilder.DropColumn(
                name: "KaydedenId",
                table: "MulakatSorularis");

            migrationBuilder.DropColumn(
                name: "KaydedenId",
                table: "Mulakatlars");

            migrationBuilder.DropColumn(
                name: "KaydedenId",
                table: "Derecelers");

            migrationBuilder.DropColumn(
                name: "TcKimlikNo",
                table: "Adaylars");

            migrationBuilder.AlterColumn<string>(
                name: "KaydedenId",
                table: "Adaylars",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "AdayId",
                table: "Adaylars",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<string>(
                name: "KullaniciId",
                table: "Adaylars",
                type: "varchar(767)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Adaylars",
                table: "Adaylars",
                column: "AdayId");

            migrationBuilder.CreateIndex(
                name: "IX_Adaylars_KullaniciId",
                table: "Adaylars",
                column: "KullaniciId");

            migrationBuilder.AddForeignKey(
                name: "FK_Adaylars_AspNetUsers_KullaniciId",
                table: "Adaylars",
                column: "KullaniciId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
