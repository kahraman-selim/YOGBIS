using Microsoft.EntityFrameworkCore.Migrations;

namespace YOGBIS.Data.Migrations
{
    public partial class okulbinabolumadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "KaydedenId",
                table: "OkulBinaBolums",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OkulBinaBolums_KaydedenId",
                table: "OkulBinaBolums",
                column: "KaydedenId");

            migrationBuilder.AddForeignKey(
                name: "FK_OkulBinaBolums_AspNetUsers_KaydedenId",
                table: "OkulBinaBolums",
                column: "KaydedenId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OkulBinaBolums_AspNetUsers_KaydedenId",
                table: "OkulBinaBolums");

            migrationBuilder.DropIndex(
                name: "IX_OkulBinaBolums_KaydedenId",
                table: "OkulBinaBolums");

            migrationBuilder.DropColumn(
                name: "KaydedenId",
                table: "OkulBinaBolums");
        }
    }
}
