using Microsoft.EntityFrameworkCore.Migrations;

namespace YOGBIS.Data.Migrations
{
    public partial class mulakatopt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mulakatlars_Derecelers_DerecelerDereceId",
                table: "Mulakatlars");

            migrationBuilder.DropIndex(
                name: "IX_Mulakatlars_DerecelerDereceId",
                table: "Mulakatlars");

            migrationBuilder.DropColumn(
                name: "DerecelerDereceId",
                table: "Mulakatlars");

            migrationBuilder.DropColumn(
                name: "Derecesi",
                table: "Mulakatlars");

            migrationBuilder.AddColumn<int>(
                name: "DereceId",
                table: "Mulakatlars",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Mulakatlars_DereceId",
                table: "Mulakatlars",
                column: "DereceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mulakatlars_Derecelers_DereceId",
                table: "Mulakatlars",
                column: "DereceId",
                principalTable: "Derecelers",
                principalColumn: "DereceId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mulakatlars_Derecelers_DereceId",
                table: "Mulakatlars");

            migrationBuilder.DropIndex(
                name: "IX_Mulakatlars_DereceId",
                table: "Mulakatlars");

            migrationBuilder.DropColumn(
                name: "DereceId",
                table: "Mulakatlars");

            migrationBuilder.AddColumn<int>(
                name: "DerecelerDereceId",
                table: "Mulakatlars",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Derecesi",
                table: "Mulakatlars",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mulakatlars_DerecelerDereceId",
                table: "Mulakatlars",
                column: "DerecelerDereceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mulakatlars_Derecelers_DerecelerDereceId",
                table: "Mulakatlars",
                column: "DerecelerDereceId",
                principalTable: "Derecelers",
                principalColumn: "DereceId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
