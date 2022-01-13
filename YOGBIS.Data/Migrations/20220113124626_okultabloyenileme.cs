using Microsoft.EntityFrameworkCore.Migrations;

namespace YOGBIS.Data.Migrations
{
    public partial class okultabloyenileme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UlkeId",
                table: "Okullars",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UlkelerUlkeId",
                table: "Okullars",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Okullars_UlkelerUlkeId",
                table: "Okullars",
                column: "UlkelerUlkeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Okullars_Ulkelers_UlkelerUlkeId",
                table: "Okullars",
                column: "UlkelerUlkeId",
                principalTable: "Ulkelers",
                principalColumn: "UlkeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Okullars_Ulkelers_UlkelerUlkeId",
                table: "Okullars");

            migrationBuilder.DropIndex(
                name: "IX_Okullars_UlkelerUlkeId",
                table: "Okullars");

            migrationBuilder.DropColumn(
                name: "UlkeId",
                table: "Okullars");

            migrationBuilder.DropColumn(
                name: "UlkelerUlkeId",
                table: "Okullars");
        }
    }
}
