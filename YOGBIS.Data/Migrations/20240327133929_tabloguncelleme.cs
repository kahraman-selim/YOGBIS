using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YOGBIS.Data.Migrations
{
    public partial class tabloguncelleme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdayGorevKaydi_Adaylar_AdaylarAdayId",
                table: "AdayGorevKaydi");

            migrationBuilder.DropForeignKey(
                name: "FK_Adaylar_Mulakatlar_MulakatId",
                table: "Adaylar");

            migrationBuilder.DropForeignKey(
                name: "FK_EPostaAdresleri_Adaylar_AdaylarAdayId",
                table: "EPostaAdresleri");

            migrationBuilder.DropForeignKey(
                name: "FK_IkametAdresleri_Adaylar_AdaylarAdayId",
                table: "IkametAdresleri");

            migrationBuilder.DropForeignKey(
                name: "FK_Komisyonlar_Mulakatlar_MulakatId",
                table: "Komisyonlar");

            migrationBuilder.DropForeignKey(
                name: "FK_MulakatSorulari_Mulakatlar_MulakatlarMulakatId",
                table: "MulakatSorulari");

            migrationBuilder.DropForeignKey(
                name: "FK_Telefonlar_Adaylar_AdaylarAdayId",
                table: "Telefonlar");

            migrationBuilder.DropIndex(
                name: "IX_Telefonlar_AdaylarAdayId",
                table: "Telefonlar");

            migrationBuilder.DropIndex(
                name: "IX_MulakatSorulari_MulakatlarMulakatId",
                table: "MulakatSorulari");

            migrationBuilder.DropIndex(
                name: "IX_Komisyonlar_MulakatId",
                table: "Komisyonlar");

            migrationBuilder.DropIndex(
                name: "IX_IkametAdresleri_AdaylarAdayId",
                table: "IkametAdresleri");

            migrationBuilder.DropIndex(
                name: "IX_EPostaAdresleri_AdaylarAdayId",
                table: "EPostaAdresleri");

            migrationBuilder.DropIndex(
                name: "IX_Adaylar_MulakatId",
                table: "Adaylar");

            migrationBuilder.DropIndex(
                name: "IX_AdayGorevKaydi_AdaylarAdayId",
                table: "AdayGorevKaydi");

            migrationBuilder.DropColumn(
                name: "AdaylarAdayId",
                table: "Telefonlar");

            migrationBuilder.DropColumn(
                name: "MulakatlarMulakatId",
                table: "MulakatSorulari");

            migrationBuilder.DropColumn(
                name: "MulakatId",
                table: "Komisyonlar");

            migrationBuilder.DropColumn(
                name: "AdaylarAdayId",
                table: "IkametAdresleri");

            migrationBuilder.DropColumn(
                name: "AdaylarAdayId",
                table: "EPostaAdresleri");

            migrationBuilder.DropColumn(
                name: "MulakatId",
                table: "AdayNot");

            migrationBuilder.DropColumn(
                name: "MulakatId",
                table: "Adaylar");

            migrationBuilder.DropColumn(
                name: "AdaylarAdayId",
                table: "AdayGorevKaydi");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "AdaylarAdayId",
                table: "Telefonlar",
                type: "varbinary(16)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "MulakatlarMulakatId",
                table: "MulakatSorulari",
                type: "varbinary(16)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "MulakatId",
                table: "Komisyonlar",
                type: "varbinary(16)",
                nullable: false,
                defaultValue: new byte[] {  });

            migrationBuilder.AddColumn<byte[]>(
                name: "AdaylarAdayId",
                table: "IkametAdresleri",
                type: "varbinary(16)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "AdaylarAdayId",
                table: "EPostaAdresleri",
                type: "varbinary(16)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "MulakatId",
                table: "AdayNot",
                type: "varbinary(16)",
                nullable: false,
                defaultValue: new byte[] {  });

            migrationBuilder.AddColumn<byte[]>(
                name: "MulakatId",
                table: "Adaylar",
                type: "varbinary(16)",
                nullable: false,
                defaultValue: new byte[] {  });

            migrationBuilder.AddColumn<byte[]>(
                name: "AdaylarAdayId",
                table: "AdayGorevKaydi",
                type: "varbinary(16)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Telefonlar_AdaylarAdayId",
                table: "Telefonlar",
                column: "AdaylarAdayId");

            migrationBuilder.CreateIndex(
                name: "IX_MulakatSorulari_MulakatlarMulakatId",
                table: "MulakatSorulari",
                column: "MulakatlarMulakatId");

            migrationBuilder.CreateIndex(
                name: "IX_Komisyonlar_MulakatId",
                table: "Komisyonlar",
                column: "MulakatId");

            migrationBuilder.CreateIndex(
                name: "IX_IkametAdresleri_AdaylarAdayId",
                table: "IkametAdresleri",
                column: "AdaylarAdayId");

            migrationBuilder.CreateIndex(
                name: "IX_EPostaAdresleri_AdaylarAdayId",
                table: "EPostaAdresleri",
                column: "AdaylarAdayId");

            migrationBuilder.CreateIndex(
                name: "IX_Adaylar_MulakatId",
                table: "Adaylar",
                column: "MulakatId");

            migrationBuilder.CreateIndex(
                name: "IX_AdayGorevKaydi_AdaylarAdayId",
                table: "AdayGorevKaydi",
                column: "AdaylarAdayId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdayGorevKaydi_Adaylar_AdaylarAdayId",
                table: "AdayGorevKaydi",
                column: "AdaylarAdayId",
                principalTable: "Adaylar",
                principalColumn: "AdayId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Adaylar_Mulakatlar_MulakatId",
                table: "Adaylar",
                column: "MulakatId",
                principalTable: "Mulakatlar",
                principalColumn: "MulakatId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EPostaAdresleri_Adaylar_AdaylarAdayId",
                table: "EPostaAdresleri",
                column: "AdaylarAdayId",
                principalTable: "Adaylar",
                principalColumn: "AdayId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IkametAdresleri_Adaylar_AdaylarAdayId",
                table: "IkametAdresleri",
                column: "AdaylarAdayId",
                principalTable: "Adaylar",
                principalColumn: "AdayId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Komisyonlar_Mulakatlar_MulakatId",
                table: "Komisyonlar",
                column: "MulakatId",
                principalTable: "Mulakatlar",
                principalColumn: "MulakatId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MulakatSorulari_Mulakatlar_MulakatlarMulakatId",
                table: "MulakatSorulari",
                column: "MulakatlarMulakatId",
                principalTable: "Mulakatlar",
                principalColumn: "MulakatId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Telefonlar_Adaylar_AdaylarAdayId",
                table: "Telefonlar",
                column: "AdaylarAdayId",
                principalTable: "Adaylar",
                principalColumn: "AdayId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
