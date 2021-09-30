using Microsoft.EntityFrameworkCore.Migrations;

namespace YOGBIS.Data.Migrations
{
    public partial class kitalar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Kitalars",
                columns: new[] { "KitaId", "KitaAciklama", "KitaAdi", "UlkeGrupId", "UlkeGruplariUlkeGrupId" },
                values: new object[,]
                {
                    { 1, "Afrika", "Afrika", 0, null },
                    { 2, "Antartika", "Antartika", 0, null },
                    { 3, "Asya", "Asya", 0, null },
                    { 4, "Avrupa", "Avrupa", 0, null },
                    { 5, "Avustralya", "Avustralya", 0, null },
                    { 6, "Güney Amerika", "Güney Amerika", 0, null },
                    { 7, "Kuzey Amerika", "Kuzey Amerika", 0, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Kitalars",
                keyColumn: "KitaId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Kitalars",
                keyColumn: "KitaId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Kitalars",
                keyColumn: "KitaId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Kitalars",
                keyColumn: "KitaId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Kitalars",
                keyColumn: "KitaId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Kitalars",
                keyColumn: "KitaId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Kitalars",
                keyColumn: "KitaId",
                keyValue: 7);
        }
    }
}
