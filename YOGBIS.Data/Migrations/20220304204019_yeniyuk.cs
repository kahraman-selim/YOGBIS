using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace YOGBIS.Data.Migrations
{
    public partial class yeniyuk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 127, nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "varchar(256)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    TcKimlikNo = table.Column<string>(nullable: true),
                    Ad = table.Column<string>(nullable: true),
                    Soyad = table.Column<string>(nullable: true),
                    KayitTarihi = table.Column<DateTime>(nullable: true),
                    KulaniciAdDegLimiti = table.Column<int>(nullable: true),
                    KullaniciResim = table.Column<byte[]>(nullable: true),
                    KullaniciResimYol = table.Column<string>(nullable: true),
                    Aktif = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AutoHistory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    RowId = table.Column<string>(maxLength: 50, nullable: false),
                    TableName = table.Column<string>(maxLength: 128, nullable: false),
                    Changed = table.Column<string>(nullable: true),
                    Kind = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutoHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 127, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 127, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(maxLength: 127, nullable: false),
                    RoleId = table.Column<string>(maxLength: 127, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(maxLength: 127, nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 127, nullable: false),
                    Name = table.Column<string>(maxLength: 127, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Branslar",
                columns: table => new
                {
                    BransId = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    BransAdi = table.Column<string>(nullable: true),
                    KaydedenId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branslar", x => x.BransId);
                    table.ForeignKey(
                        name: "FK_Branslar_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Duyurular",
                columns: table => new
                {
                    DuyurularId = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    DuyuruBaslık = table.Column<string>(nullable: true),
                    DuyuruDetay = table.Column<string>(nullable: true),
                    DuyuruLink = table.Column<string>(nullable: true),
                    KaydedenId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Duyurular", x => x.DuyurularId);
                    table.ForeignKey(
                        name: "FK_Duyurular_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Iller",
                columns: table => new
                {
                    IlId = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    PlakaKodu = table.Column<string>(nullable: true),
                    IlAdi = table.Column<string>(nullable: true),
                    KaydedenId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Iller", x => x.IlId);
                    table.ForeignKey(
                        name: "FK_Iller_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notlar",
                columns: table => new
                {
                    NotId = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    NotAdi = table.Column<string>(nullable: true),
                    NotDetay = table.Column<string>(nullable: true),
                    BaTarihi = table.Column<DateTime>(nullable: false),
                    BiTarihi = table.Column<DateTime>(nullable: false),
                    NotRenk = table.Column<string>(nullable: true),
                    KaydedenId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notlar", x => x.NotId);
                    table.ForeignKey(
                        name: "FK_Notlar_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SoruBankasi",
                columns: table => new
                {
                    SoruBankasiId = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    Soru = table.Column<string>(nullable: true),
                    Cevap = table.Column<string>(nullable: true),
                    SorulmaSayisi = table.Column<int>(nullable: false),
                    SoruDurumu = table.Column<bool>(nullable: false),
                    KaydedenId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoruBankasi", x => x.SoruBankasiId);
                    table.ForeignKey(
                        name: "FK_SoruBankasi_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SoruDereceler",
                columns: table => new
                {
                    DereceId = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    DereceAdi = table.Column<string>(nullable: true),
                    KaydedenId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoruDereceler", x => x.DereceId);
                    table.ForeignKey(
                        name: "FK_SoruDereceler_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SoruKategoriler",
                columns: table => new
                {
                    SoruKategorilerId = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    SoruKategorilerAdi = table.Column<string>(nullable: true),
                    SoruKategorilerKullanimi = table.Column<string>(nullable: true),
                    SoruKategorilerPuan = table.Column<int>(nullable: false),
                    DereceId = table.Column<byte[]>(nullable: false),
                    KaydedenId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoruKategoriler", x => x.SoruKategorilerId);
                    table.ForeignKey(
                        name: "FK_SoruKategoriler_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SSS",
                columns: table => new
                {
                    SSSId = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    Soru = table.Column<string>(nullable: true),
                    KaydedenId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SSS", x => x.SSSId);
                    table.ForeignKey(
                        name: "FK_SSS_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UlkeGruplari",
                columns: table => new
                {
                    UlkeGrupId = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    UlkeGrupAdi = table.Column<string>(nullable: true),
                    UlkeGrupAciklama = table.Column<string>(nullable: true),
                    KaydedenId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UlkeGruplari", x => x.UlkeGrupId);
                    table.ForeignKey(
                        name: "FK_UlkeGruplari_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ilceler",
                columns: table => new
                {
                    IlceId = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    IlceAdi = table.Column<string>(nullable: true),
                    IlId = table.Column<byte[]>(nullable: false),
                    KaydedenId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ilceler", x => x.IlceId);
                    table.ForeignKey(
                        name: "FK_Ilceler_Iller_IlId",
                        column: x => x.IlId,
                        principalTable: "Iller",
                        principalColumn: "IlId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ilceler_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IllerMdEPosta",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    IlId = table.Column<byte[]>(nullable: false),
                    IlEPostaAdres = table.Column<string>(nullable: true),
                    IlEpostaAdresAciklama = table.Column<string>(nullable: true),
                    KaydedenId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IllerMdEPosta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IllerMdEPosta_Iller_IlId",
                        column: x => x.IlId,
                        principalTable: "Iller",
                        principalColumn: "IlId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IllerMdEPosta_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SoruBankasiLog",
                columns: table => new
                {
                    SoruBankasiLogId = table.Column<byte[]>(nullable: false),
                    SoruBankasiId = table.Column<byte[]>(nullable: false),
                    Soru = table.Column<string>(nullable: true),
                    Cevap = table.Column<string>(nullable: true),
                    DereceId = table.Column<byte[]>(nullable: false),
                    SoruKategoriId = table.Column<byte[]>(nullable: false),
                    SorulmaSayisi = table.Column<int>(nullable: false),
                    SoruDurumu = table.Column<bool>(nullable: false),
                    KayitTuru = table.Column<int>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    KaydedenId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoruBankasiLog", x => x.SoruBankasiLogId);
                    table.ForeignKey(
                        name: "FK_SoruBankasiLog_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SoruBankasiLog_SoruBankasi_SoruBankasiId",
                        column: x => x.SoruBankasiId,
                        principalTable: "SoruBankasi",
                        principalColumn: "SoruBankasiId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SoruOnay",
                columns: table => new
                {
                    SoruOnayId = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    SoruBankasiId = table.Column<byte[]>(nullable: false),
                    OnayDurumu = table.Column<int>(nullable: false),
                    OnayAciklama = table.Column<string>(nullable: true),
                    OnaylayanId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoruOnay", x => x.SoruOnayId);
                    table.ForeignKey(
                        name: "FK_SoruOnay_AspNetUsers_OnaylayanId",
                        column: x => x.OnaylayanId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SoruOnay_SoruBankasi_SoruBankasiId",
                        column: x => x.SoruBankasiId,
                        principalTable: "SoruBankasi",
                        principalColumn: "SoruBankasiId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mulakatlar",
                columns: table => new
                {
                    MulakatId = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    OnaySayisi = table.Column<string>(nullable: true),
                    OnayTarihi = table.Column<DateTime>(nullable: false),
                    KararSayisi = table.Column<string>(nullable: true),
                    KararTarihi = table.Column<DateTime>(nullable: false),
                    MulakatAdi = table.Column<string>(nullable: true),
                    DereceId = table.Column<byte[]>(nullable: false),
                    BaslamaTarihi = table.Column<DateTime>(nullable: false),
                    BitisTarihi = table.Column<DateTime>(nullable: false),
                    AdaySayisi = table.Column<int>(nullable: true),
                    SorulanSoruSayisi = table.Column<int>(nullable: true),
                    Durumu = table.Column<bool>(nullable: false),
                    MulakatAciklama = table.Column<string>(nullable: true),
                    KaydedenId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mulakatlar", x => x.MulakatId);
                    table.ForeignKey(
                        name: "FK_Mulakatlar_SoruDereceler_DereceId",
                        column: x => x.DereceId,
                        principalTable: "SoruDereceler",
                        principalColumn: "DereceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mulakatlar_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SoruDerece",
                columns: table => new
                {
                    SoruId = table.Column<byte[]>(nullable: false),
                    DereceId = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoruDerece", x => new { x.SoruId, x.DereceId });
                    table.ForeignKey(
                        name: "FK_SoruDerece_SoruDereceler_DereceId",
                        column: x => x.DereceId,
                        principalTable: "SoruDereceler",
                        principalColumn: "DereceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SoruDerece_SoruBankasi_SoruId",
                        column: x => x.SoruId,
                        principalTable: "SoruBankasi",
                        principalColumn: "SoruBankasiId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SoruKategori",
                columns: table => new
                {
                    SoruId = table.Column<byte[]>(nullable: false),
                    KategoriId = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoruKategori", x => new { x.SoruId, x.KategoriId });
                    table.ForeignKey(
                        name: "FK_SoruKategori_SoruKategoriler_KategoriId",
                        column: x => x.KategoriId,
                        principalTable: "SoruKategoriler",
                        principalColumn: "SoruKategorilerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SoruKategori_SoruBankasi_SoruId",
                        column: x => x.SoruId,
                        principalTable: "SoruBankasi",
                        principalColumn: "SoruBankasiId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SSSCevap",
                columns: table => new
                {
                    SSSCevapId = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    SSSId = table.Column<byte[]>(nullable: false),
                    SSSCevapDetay = table.Column<string>(nullable: true),
                    KaydedenId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SSSCevap", x => x.SSSCevapId);
                    table.ForeignKey(
                        name: "FK_SSSCevap_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SSSCevap_SSS_SSSId",
                        column: x => x.SSSId,
                        principalTable: "SSS",
                        principalColumn: "SSSId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Kitalar",
                columns: table => new
                {
                    KitaId = table.Column<byte[]>(nullable: false),
                    KitaAdi = table.Column<string>(nullable: true),
                    KitaAciklama = table.Column<string>(nullable: true),
                    KaydedenId = table.Column<string>(nullable: true),
                    UlkeGrupId = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kitalar", x => x.KitaId);
                    table.ForeignKey(
                        name: "FK_Kitalar_UlkeGruplari_UlkeGrupId",
                        column: x => x.UlkeGrupId,
                        principalTable: "UlkeGruplari",
                        principalColumn: "UlkeGrupId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Adaylar",
                columns: table => new
                {
                    AdayId = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    AdayTC = table.Column<string>(nullable: true),
                    AdayAd = table.Column<string>(nullable: true),
                    AdaySoyad = table.Column<string>(nullable: true),
                    AdayBabaAd = table.Column<string>(nullable: true),
                    AdayAnaAd = table.Column<string>(nullable: true),
                    DogumTarihi = table.Column<string>(nullable: true),
                    DogumYeri = table.Column<string>(nullable: true),
                    Cinsiyet = table.Column<string>(nullable: true),
                    Askerlik = table.Column<string>(nullable: true),
                    CepTelNo = table.Column<string>(nullable: true),
                    EPosta = table.Column<string>(nullable: true),
                    AdresIl = table.Column<string>(nullable: true),
                    AdresIlce = table.Column<string>(nullable: true),
                    KurumKod = table.Column<string>(nullable: true),
                    KurumAdi = table.Column<string>(nullable: true),
                    Ogrenim = table.Column<string>(nullable: true),
                    MezunOkulKodu = table.Column<string>(nullable: true),
                    MezunOkulAdi = table.Column<string>(nullable: true),
                    Brans = table.Column<string>(nullable: true),
                    HizmetYil = table.Column<string>(nullable: true),
                    HizmetAy = table.Column<string>(nullable: true),
                    HizmetGun = table.Column<string>(nullable: true),
                    Derece = table.Column<string>(nullable: true),
                    Kademe = table.Column<string>(nullable: true),
                    Enaz5Yil = table.Column<string>(nullable: true),
                    DahaOnceYYGorev = table.Column<string>(nullable: true),
                    YYSonrasi2Yil = table.Column<string>(nullable: true),
                    YIciGorevBasTar = table.Column<string>(nullable: true),
                    DisiplinCeza = table.Column<string>(nullable: true),
                    YabanciDilAdi = table.Column<string>(nullable: true),
                    YabanciDilTuru = table.Column<string>(nullable: true),
                    YabanciDilTarihi = table.Column<string>(nullable: true),
                    YabanciDilSeviye = table.Column<string>(nullable: true),
                    YabanciDilBelgesi = table.Column<string>(nullable: true),
                    UlkeTercihi = table.Column<string>(nullable: true),
                    IlTercihi = table.Column<string>(nullable: true),
                    BasvuranIP = table.Column<string>(nullable: true),
                    BasvuruTarihi = table.Column<string>(nullable: true),
                    OnayDurumu = table.Column<string>(nullable: true),
                    OnayTarihi = table.Column<string>(nullable: true),
                    Onaylayan = table.Column<string>(nullable: true),
                    OnayAciklama = table.Column<string>(nullable: true),
                    MYSSinavTarihi = table.Column<string>(nullable: true),
                    MYSSinavTedbiri = table.Column<string>(nullable: true),
                    MYSSTAciklama = table.Column<string>(nullable: true),
                    DereceId = table.Column<byte[]>(nullable: false),
                    MYSPuan = table.Column<string>(nullable: true),
                    MYSSonuc = table.Column<string>(nullable: true),
                    MulakatDurum = table.Column<string>(nullable: true),
                    MulakatDurumAciklama = table.Column<string>(nullable: true),
                    IlMemGorus = table.Column<string>(nullable: true),
                    TYSTarih = table.Column<string>(nullable: true),
                    TYSSaat = table.Column<string>(nullable: true),
                    TYSPuan = table.Column<string>(nullable: true),
                    AritmetikOrtalama = table.Column<string>(nullable: true),
                    SonucDurumu = table.Column<string>(nullable: true),
                    GorevDurumu = table.Column<int>(nullable: true),
                    MulakatId = table.Column<byte[]>(nullable: false),
                    KaydedenId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adaylar", x => x.AdayId);
                    table.ForeignKey(
                        name: "FK_Adaylar_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Adaylar_Mulakatlar_MulakatId",
                        column: x => x.MulakatId,
                        principalTable: "Mulakatlar",
                        principalColumn: "MulakatId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Komisyonlar",
                columns: table => new
                {
                    KomisyonId = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    KomisyonAdi = table.Column<string>(nullable: true),
                    KomisyonUyeSiraId = table.Column<int>(nullable: false),
                    KomisyonGorevi = table.Column<int>(nullable: false),
                    KomisyonUyeGorevYeri = table.Column<string>(nullable: true),
                    KomisyonUyeAdiSoyadi = table.Column<string>(nullable: true),
                    KomisyoUyeStatu = table.Column<string>(nullable: true),
                    KomisyoUyeTel = table.Column<string>(nullable: true),
                    KomisyonUyeEPosta = table.Column<string>(nullable: true),
                    KomisyonGorevBaslamaTarihi = table.Column<DateTime>(nullable: false),
                    KomisyonGorevBitisTarihi = table.Column<DateTime>(nullable: false),
                    MulakatId = table.Column<byte[]>(nullable: false),
                    KaydedenId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Komisyonlar", x => x.KomisyonId);
                    table.ForeignKey(
                        name: "FK_Komisyonlar_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Komisyonlar_Mulakatlar_MulakatId",
                        column: x => x.MulakatId,
                        principalTable: "Mulakatlar",
                        principalColumn: "MulakatId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MulakatSorulari",
                columns: table => new
                {
                    MulakatSorulariId = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    SoruSiraNo = table.Column<int>(nullable: false),
                    SoruId = table.Column<byte[]>(nullable: false),
                    DereceId = table.Column<byte[]>(nullable: false),
                    DereceAdi = table.Column<string>(nullable: true),
                    SoruKategoriId = table.Column<byte[]>(nullable: false),
                    SoruKategoriAdi = table.Column<string>(nullable: true),
                    Soru = table.Column<string>(nullable: true),
                    Cevap = table.Column<string>(nullable: true),
                    MulakatId = table.Column<byte[]>(nullable: false),
                    SorulanAdayTC = table.Column<int>(nullable: true),
                    KaydedenId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MulakatSorulari", x => x.MulakatSorulariId);
                    table.ForeignKey(
                        name: "FK_MulakatSorulari_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MulakatSorulari_Mulakatlar_MulakatId",
                        column: x => x.MulakatId,
                        principalTable: "Mulakatlar",
                        principalColumn: "MulakatId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ulkeler",
                columns: table => new
                {
                    UlkeId = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    UlkeKodu = table.Column<string>(nullable: true),
                    UlkeAdi = table.Column<string>(nullable: true),
                    UlkeBayrakURL = table.Column<string>(nullable: true),
                    UlkeBayrakAdi = table.Column<string>(nullable: true),
                    UlkeAciklama = table.Column<string>(nullable: true),
                    Aktif = table.Column<bool>(nullable: false),
                    KitaId = table.Column<byte[]>(nullable: false),
                    KaydedenId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ulkeler", x => x.UlkeId);
                    table.ForeignKey(
                        name: "FK_Ulkeler_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ulkeler_Kitalar_KitaId",
                        column: x => x.KitaId,
                        principalTable: "Kitalar",
                        principalColumn: "KitaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdayDDK",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    AdayId = table.Column<byte[]>(nullable: false),
                    AdayTC = table.Column<string>(nullable: true),
                    KaydedenId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdayDDK", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdayDDK_Adaylar_AdayId",
                        column: x => x.AdayId,
                        principalTable: "Adaylar",
                        principalColumn: "AdayId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdayDDK_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AdayNot",
                columns: table => new
                {
                    AdayNotId = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    AdayId = table.Column<byte[]>(nullable: false),
                    AdayTC = table.Column<string>(nullable: true),
                    KomisyonId = table.Column<byte[]>(nullable: false),
                    KomisyonUyeSiraId = table.Column<int>(nullable: false),
                    NotKategoriId = table.Column<byte[]>(nullable: false),
                    Not = table.Column<int>(nullable: false),
                    MulakatId = table.Column<byte[]>(nullable: false),
                    KaydedenId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdayNot", x => x.AdayNotId);
                    table.ForeignKey(
                        name: "FK_AdayNot_Adaylar_AdayId",
                        column: x => x.AdayId,
                        principalTable: "Adaylar",
                        principalColumn: "AdayId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdayNot_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IkametAdresleri",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    IkametIlId = table.Column<byte[]>(nullable: false),
                    IkametIlceId = table.Column<byte[]>(nullable: false),
                    PostaKodu = table.Column<string>(nullable: true),
                    AdresTuru = table.Column<string>(nullable: true),
                    IkametAdresi = table.Column<string>(nullable: true),
                    KaydedenId = table.Column<string>(nullable: true),
                    AdaylarAdayId = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IkametAdresleri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IkametAdresleri_Adaylar_AdaylarAdayId",
                        column: x => x.AdaylarAdayId,
                        principalTable: "Adaylar",
                        principalColumn: "AdayId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IkametAdresleri_Ilceler_IkametIlceId",
                        column: x => x.IkametIlceId,
                        principalTable: "Ilceler",
                        principalColumn: "IlceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IkametAdresleri_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Temsilcilikler",
                columns: table => new
                {
                    TemsilcilikId = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    TemsilcilikAdi = table.Column<string>(nullable: true),
                    TemsilcilikTuru = table.Column<string>(nullable: true),
                    TemsilciId = table.Column<string>(nullable: true),
                    TemsilciGorevi = table.Column<string>(nullable: true),
                    TemsilcilikTel = table.Column<string>(nullable: true),
                    TemsilcilikEPosta = table.Column<string>(nullable: true),
                    TemsilcilikWebAdres = table.Column<string>(nullable: true),
                    UlkeId = table.Column<byte[]>(nullable: false),
                    KaydedenId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Temsilcilikler", x => x.TemsilcilikId);
                    table.ForeignKey(
                        name: "FK_Temsilcilikler_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Temsilcilikler_Ulkeler_UlkeId",
                        column: x => x.UlkeId,
                        principalTable: "Ulkeler",
                        principalColumn: "UlkeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Eyaletler",
                columns: table => new
                {
                    EyaletId = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    EyaletAdi = table.Column<string>(nullable: true),
                    EyaletAciklama = table.Column<string>(nullable: true),
                    UlkeId = table.Column<byte[]>(nullable: false),
                    TemsilciId = table.Column<string>(nullable: true),
                    KaydedenId = table.Column<string>(nullable: true),
                    TemsilciliklerTemsilcilikId = table.Column<byte[]>(nullable: true),
                    UlkelerUlkeId = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eyaletler", x => x.EyaletId);
                    table.ForeignKey(
                        name: "FK_Eyaletler_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Eyaletler_Temsilcilikler_TemsilciliklerTemsilcilikId",
                        column: x => x.TemsilciliklerTemsilcilikId,
                        principalTable: "Temsilcilikler",
                        principalColumn: "TemsilcilikId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Eyaletler_Ulkeler_UlkelerUlkeId",
                        column: x => x.UlkelerUlkeId,
                        principalTable: "Ulkeler",
                        principalColumn: "UlkeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Personeller",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    PersonelGrupAdi = table.Column<string>(nullable: true),
                    PersonelAdiSoyadi = table.Column<string>(nullable: true),
                    PersonelGorevi = table.Column<string>(nullable: true),
                    PersonelUnvan = table.Column<string>(nullable: true),
                    PersonelTelefon = table.Column<string>(nullable: true),
                    PersonelEPosta = table.Column<string>(nullable: true),
                    PersonelGorevAlani = table.Column<string>(nullable: true),
                    PersonelGorevYeri = table.Column<string>(nullable: true),
                    TemsilcilikId = table.Column<byte[]>(nullable: true),
                    KaydedenId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personeller", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Personeller_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Personeller_Temsilcilikler_TemsilcilikId",
                        column: x => x.TemsilcilikId,
                        principalTable: "Temsilcilikler",
                        principalColumn: "TemsilcilikId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sehirler",
                columns: table => new
                {
                    SehirId = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    SehirAdi = table.Column<string>(nullable: true),
                    Baskent = table.Column<bool>(nullable: true),
                    SehirAciklama = table.Column<string>(nullable: true),
                    SehirVatandas = table.Column<int>(nullable: false),
                    TemsilciId = table.Column<string>(nullable: true),
                    EyaletId = table.Column<byte[]>(nullable: true),
                    UlkeId = table.Column<byte[]>(nullable: false),
                    KaydedenId = table.Column<string>(nullable: true),
                    EyaletlerEyaletId = table.Column<byte[]>(nullable: true),
                    TemsilciliklerTemsilcilikId = table.Column<byte[]>(nullable: true),
                    UlkelerUlkeId = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sehirler", x => x.SehirId);
                    table.ForeignKey(
                        name: "FK_Sehirler_Eyaletler_EyaletlerEyaletId",
                        column: x => x.EyaletlerEyaletId,
                        principalTable: "Eyaletler",
                        principalColumn: "EyaletId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sehirler_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sehirler_Temsilcilikler_TemsilciliklerTemsilcilikId",
                        column: x => x.TemsilciliklerTemsilcilikId,
                        principalTable: "Temsilcilikler",
                        principalColumn: "TemsilcilikId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sehirler_Ulkeler_UlkelerUlkeId",
                        column: x => x.UlkelerUlkeId,
                        principalTable: "Ulkeler",
                        principalColumn: "UlkeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Okullar",
                columns: table => new
                {
                    OkulId = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    OkulKodu = table.Column<int>(nullable: false),
                    OkulAdi = table.Column<string>(nullable: true),
                    OkulTuru = table.Column<string>(nullable: true),
                    OkulLogoURL = table.Column<string>(nullable: true),
                    OkulBilgi = table.Column<string>(nullable: true),
                    OkulAcilisTarihi = table.Column<DateTime>(nullable: true),
                    OkulDurumu = table.Column<bool>(nullable: false),
                    OkulMudurId = table.Column<string>(nullable: true),
                    OkulHizmetGecisDonem = table.Column<string>(nullable: true),
                    OkulKapaliAlan = table.Column<string>(nullable: true),
                    OkulAcikAlan = table.Column<string>(nullable: true),
                    OkulMulkiDurum = table.Column<bool>(nullable: true),
                    OkulMulkiDurumAciklama = table.Column<string>(nullable: true),
                    OkulInternetAdresi = table.Column<string>(nullable: true),
                    OkulEPostaAdresi = table.Column<string>(nullable: true),
                    OkulTelefon = table.Column<string>(nullable: true),
                    SehirId = table.Column<byte[]>(nullable: false),
                    EyaletId = table.Column<byte[]>(nullable: true),
                    UlkeId = table.Column<byte[]>(nullable: false),
                    KaydedenId = table.Column<string>(nullable: true),
                    EyaletlerEyaletId = table.Column<byte[]>(nullable: true),
                    UlkelerUlkeId = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Okullar", x => x.OkulId);
                    table.ForeignKey(
                        name: "FK_Okullar_Eyaletler_EyaletlerEyaletId",
                        column: x => x.EyaletlerEyaletId,
                        principalTable: "Eyaletler",
                        principalColumn: "EyaletId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Okullar_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Okullar_Sehirler_SehirId",
                        column: x => x.SehirId,
                        principalTable: "Sehirler",
                        principalColumn: "SehirId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Okullar_Ulkeler_UlkelerUlkeId",
                        column: x => x.UlkelerUlkeId,
                        principalTable: "Ulkeler",
                        principalColumn: "UlkeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Universiteler",
                columns: table => new
                {
                    UniId = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    UniAdi = table.Column<string>(nullable: true),
                    YurtIciMi = table.Column<bool>(nullable: false),
                    UniStatu = table.Column<string>(nullable: true),
                    UniLogo = table.Column<string>(nullable: true),
                    UniBilgi = table.Column<string>(nullable: true),
                    SehirId = table.Column<byte[]>(nullable: true),
                    EyaletId = table.Column<byte[]>(nullable: true),
                    TemsilciId = table.Column<byte[]>(nullable: true),
                    UlkeId = table.Column<byte[]>(nullable: false),
                    KaydedenId = table.Column<string>(nullable: true),
                    EyaletlerEyaletId = table.Column<byte[]>(nullable: true),
                    SehirlerSehirId = table.Column<byte[]>(nullable: true),
                    UlkelerUlkeId = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Universiteler", x => x.UniId);
                    table.ForeignKey(
                        name: "FK_Universiteler_Eyaletler_EyaletlerEyaletId",
                        column: x => x.EyaletlerEyaletId,
                        principalTable: "Eyaletler",
                        principalColumn: "EyaletId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Universiteler_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Universiteler_Sehirler_SehirlerSehirId",
                        column: x => x.SehirlerSehirId,
                        principalTable: "Sehirler",
                        principalColumn: "SehirId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Universiteler_Ulkeler_UlkelerUlkeId",
                        column: x => x.UlkelerUlkeId,
                        principalTable: "Ulkeler",
                        principalColumn: "UlkeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EPostaAdresleri",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    EpostaAdresi = table.Column<string>(nullable: true),
                    KaydedenId = table.Column<string>(nullable: true),
                    AdaylarAdayId = table.Column<byte[]>(nullable: true),
                    OkullarOkulId = table.Column<byte[]>(nullable: true),
                    TemsilciliklerTemsilcilikId = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EPostaAdresleri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EPostaAdresleri_Adaylar_AdaylarAdayId",
                        column: x => x.AdaylarAdayId,
                        principalTable: "Adaylar",
                        principalColumn: "AdayId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EPostaAdresleri_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EPostaAdresleri_Okullar_OkullarOkulId",
                        column: x => x.OkullarOkulId,
                        principalTable: "Okullar",
                        principalColumn: "OkulId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EPostaAdresleri_Temsilcilikler_TemsilciliklerTemsilcilikId",
                        column: x => x.TemsilciliklerTemsilcilikId,
                        principalTable: "Temsilcilikler",
                        principalColumn: "TemsilcilikId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Etkinlikler",
                columns: table => new
                {
                    EtkinlikId = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    EtkinlikAdi = table.Column<string>(nullable: true),
                    BasTarihi = table.Column<DateTime>(nullable: false),
                    BitTarihi = table.Column<DateTime>(nullable: false),
                    EtkinlikBilgi = table.Column<string>(nullable: true),
                    KatilimciSayisi = table.Column<int>(nullable: false),
                    DuzenleyenAdiSoyadi = table.Column<string>(nullable: true),
                    EtkinlikKapakResimUrl = table.Column<string>(nullable: true),
                    KaydedenId = table.Column<string>(nullable: true),
                    OkullarOkulId = table.Column<byte[]>(nullable: true),
                    TemsilciliklerTemsilcilikId = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etkinlikler", x => x.EtkinlikId);
                    table.ForeignKey(
                        name: "FK_Etkinlikler_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Etkinlikler_Okullar_OkullarOkulId",
                        column: x => x.OkullarOkulId,
                        principalTable: "Okullar",
                        principalColumn: "OkulId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Etkinlikler_Temsilcilikler_TemsilciliklerTemsilcilikId",
                        column: x => x.TemsilciliklerTemsilcilikId,
                        principalTable: "Temsilcilikler",
                        principalColumn: "TemsilcilikId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OkulBilgi",
                columns: table => new
                {
                    OkulBilgiId = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    OkulTelefon = table.Column<string>(nullable: true),
                    OkulAdres = table.Column<string>(nullable: true),
                    MudurAdiSoyadi = table.Column<string>(nullable: true),
                    MudurTelefon = table.Column<string>(nullable: true),
                    MudurEPosta = table.Column<string>(nullable: true),
                    MudurDonusYil = table.Column<string>(nullable: true),
                    MdYrdAdiSoyadi = table.Column<string>(nullable: true),
                    MdYrdTelefon = table.Column<string>(nullable: true),
                    MdYrdEPosta = table.Column<string>(nullable: true),
                    MdYrdDonusYil = table.Column<string>(nullable: true),
                    OkulId = table.Column<byte[]>(nullable: false),
                    UlkeId = table.Column<byte[]>(nullable: false),
                    KaydedenId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OkulBilgi", x => x.OkulBilgiId);
                    table.ForeignKey(
                        name: "FK_OkulBilgi_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OkulBilgi_Okullar_OkulId",
                        column: x => x.OkulId,
                        principalTable: "Okullar",
                        principalColumn: "OkulId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OkulBinaBolum",
                columns: table => new
                {
                    OkulBinaBolumId = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    BolumAdi = table.Column<string>(nullable: true),
                    BolumAdedi = table.Column<int>(nullable: false),
                    OkulId = table.Column<byte[]>(nullable: false),
                    KaydedenId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OkulBinaBolum", x => x.OkulBinaBolumId);
                    table.ForeignKey(
                        name: "FK_OkulBinaBolum_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OkulBinaBolum_Okullar_OkulId",
                        column: x => x.OkulId,
                        principalTable: "Okullar",
                        principalColumn: "OkulId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subeler",
                columns: table => new
                {
                    SubeId = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    SubeAdi = table.Column<string>(nullable: true),
                    SubeAcilisTarihi = table.Column<DateTime>(nullable: false),
                    OkulId = table.Column<byte[]>(nullable: false),
                    KaydedenId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subeler", x => x.SubeId);
                    table.ForeignKey(
                        name: "FK_Subeler_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Subeler_Okullar_OkulId",
                        column: x => x.OkulId,
                        principalTable: "Okullar",
                        principalColumn: "OkulId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Telefonlar",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    TelefonNumarası = table.Column<string>(nullable: true),
                    TelefonTuru = table.Column<string>(nullable: true),
                    KaydedenId = table.Column<string>(nullable: true),
                    AdaylarAdayId = table.Column<byte[]>(nullable: true),
                    OkullarOkulId = table.Column<byte[]>(nullable: true),
                    TemsilciliklerTemsilcilikId = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telefonlar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Telefonlar_Adaylar_AdaylarAdayId",
                        column: x => x.AdaylarAdayId,
                        principalTable: "Adaylar",
                        principalColumn: "AdayId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Telefonlar_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Telefonlar_Okullar_OkullarOkulId",
                        column: x => x.OkullarOkulId,
                        principalTable: "Okullar",
                        principalColumn: "OkulId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Telefonlar_Temsilcilikler_TemsilciliklerTemsilcilikId",
                        column: x => x.TemsilciliklerTemsilcilikId,
                        principalTable: "Temsilcilikler",
                        principalColumn: "TemsilcilikId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AdayGorevKaydi",
                columns: table => new
                {
                    AdayGorevKaydiId = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    DereceId = table.Column<byte[]>(nullable: false),
                    GorevliTC = table.Column<string>(nullable: true),
                    GorevliAdi = table.Column<string>(nullable: true),
                    GorevliSoyadi = table.Column<string>(nullable: true),
                    Gorevi = table.Column<string>(nullable: true),
                    BransId = table.Column<byte[]>(nullable: false),
                    GorevOnaySayi = table.Column<string>(nullable: true),
                    GorevOnayTarihi = table.Column<DateTime>(nullable: false),
                    GorevlendirilmeTarihi = table.Column<DateTime>(nullable: true),
                    GorevBasTarihi = table.Column<DateTime>(nullable: true),
                    GorevBitisTarihi = table.Column<DateTime>(nullable: true),
                    GorevDurumu = table.Column<int>(nullable: false),
                    GorevAciklama = table.Column<string>(nullable: true),
                    OkulId = table.Column<byte[]>(nullable: true),
                    UniId = table.Column<byte[]>(nullable: true),
                    TemsilcilikId = table.Column<byte[]>(nullable: true),
                    SehirId = table.Column<byte[]>(nullable: true),
                    EyaletId = table.Column<byte[]>(nullable: true),
                    UlkeId = table.Column<byte[]>(nullable: false),
                    KaydedenId = table.Column<string>(nullable: true),
                    AdaylarAdayId = table.Column<byte[]>(nullable: true),
                    EyaletlerEyaletId = table.Column<byte[]>(nullable: true),
                    OkullarOkulId = table.Column<byte[]>(nullable: true),
                    SehirlerSehirId = table.Column<byte[]>(nullable: true),
                    TemsilciliklerTemsilcilikId = table.Column<byte[]>(nullable: true),
                    UniversitelerUniId = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdayGorevKaydi", x => x.AdayGorevKaydiId);
                    table.ForeignKey(
                        name: "FK_AdayGorevKaydi_Adaylar_AdaylarAdayId",
                        column: x => x.AdaylarAdayId,
                        principalTable: "Adaylar",
                        principalColumn: "AdayId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdayGorevKaydi_Eyaletler_EyaletlerEyaletId",
                        column: x => x.EyaletlerEyaletId,
                        principalTable: "Eyaletler",
                        principalColumn: "EyaletId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdayGorevKaydi_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdayGorevKaydi_Okullar_OkullarOkulId",
                        column: x => x.OkullarOkulId,
                        principalTable: "Okullar",
                        principalColumn: "OkulId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdayGorevKaydi_Sehirler_SehirlerSehirId",
                        column: x => x.SehirlerSehirId,
                        principalTable: "Sehirler",
                        principalColumn: "SehirId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdayGorevKaydi_Temsilcilikler_TemsilciliklerTemsilcilikId",
                        column: x => x.TemsilciliklerTemsilcilikId,
                        principalTable: "Temsilcilikler",
                        principalColumn: "TemsilcilikId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdayGorevKaydi_Ulkeler_UlkeId",
                        column: x => x.UlkeId,
                        principalTable: "Ulkeler",
                        principalColumn: "UlkeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdayGorevKaydi_Universiteler_UniversitelerUniId",
                        column: x => x.UniversitelerUniId,
                        principalTable: "Universiteler",
                        principalColumn: "UniId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DosyaGaleri",
                columns: table => new
                {
                    DosyaGaleriId = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    DosyaAdi = table.Column<string>(nullable: true),
                    DosyaURL = table.Column<string>(nullable: true),
                    KaydedenId = table.Column<string>(nullable: true),
                    DuyurularId = table.Column<byte[]>(nullable: true),
                    EtkinliklerEtkinlikId = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DosyaGaleri", x => x.DosyaGaleriId);
                    table.ForeignKey(
                        name: "FK_DosyaGaleri_Duyurular_DuyurularId",
                        column: x => x.DuyurularId,
                        principalTable: "Duyurular",
                        principalColumn: "DuyurularId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DosyaGaleri_Etkinlikler_EtkinliklerEtkinlikId",
                        column: x => x.EtkinliklerEtkinlikId,
                        principalTable: "Etkinlikler",
                        principalColumn: "EtkinlikId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DosyaGaleri_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Siniflar",
                columns: table => new
                {
                    SinifId = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    SinifAdi = table.Column<string>(nullable: true),
                    SinifAcilisTarihi = table.Column<DateTime>(nullable: false),
                    OkulId = table.Column<byte[]>(nullable: false),
                    SubeId = table.Column<byte[]>(nullable: false),
                    KaydedenId = table.Column<string>(nullable: true),
                    OkullarOkulId = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Siniflar", x => x.SinifId);
                    table.ForeignKey(
                        name: "FK_Siniflar_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Siniflar_Okullar_OkullarOkulId",
                        column: x => x.OkullarOkulId,
                        principalTable: "Okullar",
                        principalColumn: "OkulId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Siniflar_Subeler_SubeId",
                        column: x => x.SubeId,
                        principalTable: "Subeler",
                        principalColumn: "SubeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GorevKararPdfGaleri",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    GorevKararPdfUrl = table.Column<string>(nullable: true),
                    GorevKararPdfSayi = table.Column<string>(nullable: true),
                    GorevKararPdfDosyaAdi = table.Column<string>(nullable: true),
                    KaydedenId = table.Column<string>(nullable: true),
                    AdayGorevKaydiId = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GorevKararPdfGaleri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GorevKararPdfGaleri_AdayGorevKaydi_AdayGorevKaydiId",
                        column: x => x.AdayGorevKaydiId,
                        principalTable: "AdayGorevKaydi",
                        principalColumn: "AdayGorevKaydiId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GorevKararPdfGaleri_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FotoGaleri",
                columns: table => new
                {
                    FotoGaleriId = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    FotoAdi = table.Column<string>(nullable: true),
                    FotoURL = table.Column<string>(nullable: true),
                    KaydedenId = table.Column<string>(nullable: true),
                    AdayGorevKaydiId = table.Column<byte[]>(nullable: true),
                    AdaylarAdayId = table.Column<byte[]>(nullable: true),
                    DuyurularId = table.Column<byte[]>(nullable: true),
                    EtkinliklerEtkinlikId = table.Column<byte[]>(nullable: true),
                    OkulBinaBolumId = table.Column<byte[]>(nullable: true),
                    OkullarOkulId = table.Column<byte[]>(nullable: true),
                    SehirlerSehirId = table.Column<byte[]>(nullable: true),
                    SiniflarSinifId = table.Column<byte[]>(nullable: true),
                    UlkelerUlkeId = table.Column<byte[]>(nullable: true),
                    UniversitelerUniId = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FotoGaleri", x => x.FotoGaleriId);
                    table.ForeignKey(
                        name: "FK_FotoGaleri_AdayGorevKaydi_AdayGorevKaydiId",
                        column: x => x.AdayGorevKaydiId,
                        principalTable: "AdayGorevKaydi",
                        principalColumn: "AdayGorevKaydiId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FotoGaleri_Adaylar_AdaylarAdayId",
                        column: x => x.AdaylarAdayId,
                        principalTable: "Adaylar",
                        principalColumn: "AdayId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FotoGaleri_Duyurular_DuyurularId",
                        column: x => x.DuyurularId,
                        principalTable: "Duyurular",
                        principalColumn: "DuyurularId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FotoGaleri_Etkinlikler_EtkinliklerEtkinlikId",
                        column: x => x.EtkinliklerEtkinlikId,
                        principalTable: "Etkinlikler",
                        principalColumn: "EtkinlikId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FotoGaleri_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FotoGaleri_OkulBinaBolum_OkulBinaBolumId",
                        column: x => x.OkulBinaBolumId,
                        principalTable: "OkulBinaBolum",
                        principalColumn: "OkulBinaBolumId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FotoGaleri_Okullar_OkullarOkulId",
                        column: x => x.OkullarOkulId,
                        principalTable: "Okullar",
                        principalColumn: "OkulId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FotoGaleri_Sehirler_SehirlerSehirId",
                        column: x => x.SehirlerSehirId,
                        principalTable: "Sehirler",
                        principalColumn: "SehirId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FotoGaleri_Siniflar_SiniflarSinifId",
                        column: x => x.SiniflarSinifId,
                        principalTable: "Siniflar",
                        principalColumn: "SinifId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FotoGaleri_Ulkeler_UlkelerUlkeId",
                        column: x => x.UlkelerUlkeId,
                        principalTable: "Ulkeler",
                        principalColumn: "UlkeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FotoGaleri_Universiteler_UniversitelerUniId",
                        column: x => x.UniversitelerUniId,
                        principalTable: "Universiteler",
                        principalColumn: "UniId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ogrenciler",
                columns: table => new
                {
                    OgrencilerId = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    OgrenciTuru = table.Column<string>(nullable: true),
                    Uyruk = table.Column<string>(nullable: true),
                    Cinsiyet = table.Column<bool>(nullable: false),
                    BaslamaKayitTarihi = table.Column<DateTime>(nullable: false),
                    KayitNedeni = table.Column<string>(nullable: true),
                    AyrilmaTarihi = table.Column<DateTime>(nullable: true),
                    AyrilmaNedeni = table.Column<string>(nullable: true),
                    EgitimciId = table.Column<byte[]>(nullable: true),
                    SinifId = table.Column<byte[]>(nullable: true),
                    SubeId = table.Column<byte[]>(nullable: true),
                    OkulId = table.Column<byte[]>(nullable: true),
                    SehirId = table.Column<byte[]>(nullable: true),
                    EyaletId = table.Column<byte[]>(nullable: true),
                    TemsilcilikId = table.Column<byte[]>(nullable: true),
                    UlkeId = table.Column<byte[]>(nullable: false),
                    KaydedenId = table.Column<string>(nullable: true),
                    AdayGorevKaydiId = table.Column<byte[]>(nullable: true),
                    EyaletlerEyaletId = table.Column<byte[]>(nullable: true),
                    OkullarOkulId = table.Column<byte[]>(nullable: true),
                    SehirlerSehirId = table.Column<byte[]>(nullable: true),
                    SiniflarSinifId = table.Column<byte[]>(nullable: true),
                    SubelerSubeId = table.Column<byte[]>(nullable: true),
                    TemsilciliklerTemsilcilikId = table.Column<byte[]>(nullable: true),
                    UniversitelerUniId = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ogrenciler", x => x.OgrencilerId);
                    table.ForeignKey(
                        name: "FK_Ogrenciler_AdayGorevKaydi_AdayGorevKaydiId",
                        column: x => x.AdayGorevKaydiId,
                        principalTable: "AdayGorevKaydi",
                        principalColumn: "AdayGorevKaydiId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ogrenciler_Eyaletler_EyaletlerEyaletId",
                        column: x => x.EyaletlerEyaletId,
                        principalTable: "Eyaletler",
                        principalColumn: "EyaletId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ogrenciler_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ogrenciler_Okullar_OkullarOkulId",
                        column: x => x.OkullarOkulId,
                        principalTable: "Okullar",
                        principalColumn: "OkulId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ogrenciler_Sehirler_SehirlerSehirId",
                        column: x => x.SehirlerSehirId,
                        principalTable: "Sehirler",
                        principalColumn: "SehirId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ogrenciler_Siniflar_SiniflarSinifId",
                        column: x => x.SiniflarSinifId,
                        principalTable: "Siniflar",
                        principalColumn: "SinifId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ogrenciler_Subeler_SubelerSubeId",
                        column: x => x.SubelerSubeId,
                        principalTable: "Subeler",
                        principalColumn: "SubeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ogrenciler_Temsilcilikler_TemsilciliklerTemsilcilikId",
                        column: x => x.TemsilciliklerTemsilcilikId,
                        principalTable: "Temsilcilikler",
                        principalColumn: "TemsilcilikId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ogrenciler_Ulkeler_UlkeId",
                        column: x => x.UlkeId,
                        principalTable: "Ulkeler",
                        principalColumn: "UlkeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ogrenciler_Universiteler_UniversitelerUniId",
                        column: x => x.UniversitelerUniId,
                        principalTable: "Universiteler",
                        principalColumn: "UniId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdayDDK_AdayId",
                table: "AdayDDK",
                column: "AdayId");

            migrationBuilder.CreateIndex(
                name: "IX_AdayDDK_KaydedenId",
                table: "AdayDDK",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_AdayGorevKaydi_AdaylarAdayId",
                table: "AdayGorevKaydi",
                column: "AdaylarAdayId");

            migrationBuilder.CreateIndex(
                name: "IX_AdayGorevKaydi_EyaletlerEyaletId",
                table: "AdayGorevKaydi",
                column: "EyaletlerEyaletId");

            migrationBuilder.CreateIndex(
                name: "IX_AdayGorevKaydi_KaydedenId",
                table: "AdayGorevKaydi",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_AdayGorevKaydi_OkullarOkulId",
                table: "AdayGorevKaydi",
                column: "OkullarOkulId");

            migrationBuilder.CreateIndex(
                name: "IX_AdayGorevKaydi_SehirlerSehirId",
                table: "AdayGorevKaydi",
                column: "SehirlerSehirId");

            migrationBuilder.CreateIndex(
                name: "IX_AdayGorevKaydi_TemsilciliklerTemsilcilikId",
                table: "AdayGorevKaydi",
                column: "TemsilciliklerTemsilcilikId");

            migrationBuilder.CreateIndex(
                name: "IX_AdayGorevKaydi_UlkeId",
                table: "AdayGorevKaydi",
                column: "UlkeId");

            migrationBuilder.CreateIndex(
                name: "IX_AdayGorevKaydi_UniversitelerUniId",
                table: "AdayGorevKaydi",
                column: "UniversitelerUniId");

            migrationBuilder.CreateIndex(
                name: "IX_Adaylar_KaydedenId",
                table: "Adaylar",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_Adaylar_MulakatId",
                table: "Adaylar",
                column: "MulakatId");

            migrationBuilder.CreateIndex(
                name: "IX_AdayNot_AdayId",
                table: "AdayNot",
                column: "AdayId");

            migrationBuilder.CreateIndex(
                name: "IX_AdayNot_KaydedenId",
                table: "AdayNot",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Branslar_KaydedenId",
                table: "Branslar",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_DosyaGaleri_DuyurularId",
                table: "DosyaGaleri",
                column: "DuyurularId");

            migrationBuilder.CreateIndex(
                name: "IX_DosyaGaleri_EtkinliklerEtkinlikId",
                table: "DosyaGaleri",
                column: "EtkinliklerEtkinlikId");

            migrationBuilder.CreateIndex(
                name: "IX_DosyaGaleri_KaydedenId",
                table: "DosyaGaleri",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_Duyurular_KaydedenId",
                table: "Duyurular",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_EPostaAdresleri_AdaylarAdayId",
                table: "EPostaAdresleri",
                column: "AdaylarAdayId");

            migrationBuilder.CreateIndex(
                name: "IX_EPostaAdresleri_KaydedenId",
                table: "EPostaAdresleri",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_EPostaAdresleri_OkullarOkulId",
                table: "EPostaAdresleri",
                column: "OkullarOkulId");

            migrationBuilder.CreateIndex(
                name: "IX_EPostaAdresleri_TemsilciliklerTemsilcilikId",
                table: "EPostaAdresleri",
                column: "TemsilciliklerTemsilcilikId");

            migrationBuilder.CreateIndex(
                name: "IX_Etkinlikler_KaydedenId",
                table: "Etkinlikler",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_Etkinlikler_OkullarOkulId",
                table: "Etkinlikler",
                column: "OkullarOkulId");

            migrationBuilder.CreateIndex(
                name: "IX_Etkinlikler_TemsilciliklerTemsilcilikId",
                table: "Etkinlikler",
                column: "TemsilciliklerTemsilcilikId");

            migrationBuilder.CreateIndex(
                name: "IX_Eyaletler_KaydedenId",
                table: "Eyaletler",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_Eyaletler_TemsilciliklerTemsilcilikId",
                table: "Eyaletler",
                column: "TemsilciliklerTemsilcilikId");

            migrationBuilder.CreateIndex(
                name: "IX_Eyaletler_UlkelerUlkeId",
                table: "Eyaletler",
                column: "UlkelerUlkeId");

            migrationBuilder.CreateIndex(
                name: "IX_FotoGaleri_AdayGorevKaydiId",
                table: "FotoGaleri",
                column: "AdayGorevKaydiId");

            migrationBuilder.CreateIndex(
                name: "IX_FotoGaleri_AdaylarAdayId",
                table: "FotoGaleri",
                column: "AdaylarAdayId");

            migrationBuilder.CreateIndex(
                name: "IX_FotoGaleri_DuyurularId",
                table: "FotoGaleri",
                column: "DuyurularId");

            migrationBuilder.CreateIndex(
                name: "IX_FotoGaleri_EtkinliklerEtkinlikId",
                table: "FotoGaleri",
                column: "EtkinliklerEtkinlikId");

            migrationBuilder.CreateIndex(
                name: "IX_FotoGaleri_KaydedenId",
                table: "FotoGaleri",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_FotoGaleri_OkulBinaBolumId",
                table: "FotoGaleri",
                column: "OkulBinaBolumId");

            migrationBuilder.CreateIndex(
                name: "IX_FotoGaleri_OkullarOkulId",
                table: "FotoGaleri",
                column: "OkullarOkulId");

            migrationBuilder.CreateIndex(
                name: "IX_FotoGaleri_SehirlerSehirId",
                table: "FotoGaleri",
                column: "SehirlerSehirId");

            migrationBuilder.CreateIndex(
                name: "IX_FotoGaleri_SiniflarSinifId",
                table: "FotoGaleri",
                column: "SiniflarSinifId");

            migrationBuilder.CreateIndex(
                name: "IX_FotoGaleri_UlkelerUlkeId",
                table: "FotoGaleri",
                column: "UlkelerUlkeId");

            migrationBuilder.CreateIndex(
                name: "IX_FotoGaleri_UniversitelerUniId",
                table: "FotoGaleri",
                column: "UniversitelerUniId");

            migrationBuilder.CreateIndex(
                name: "IX_GorevKararPdfGaleri_AdayGorevKaydiId",
                table: "GorevKararPdfGaleri",
                column: "AdayGorevKaydiId");

            migrationBuilder.CreateIndex(
                name: "IX_GorevKararPdfGaleri_KaydedenId",
                table: "GorevKararPdfGaleri",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_IkametAdresleri_AdaylarAdayId",
                table: "IkametAdresleri",
                column: "AdaylarAdayId");

            migrationBuilder.CreateIndex(
                name: "IX_IkametAdresleri_IkametIlceId",
                table: "IkametAdresleri",
                column: "IkametIlceId");

            migrationBuilder.CreateIndex(
                name: "IX_IkametAdresleri_KaydedenId",
                table: "IkametAdresleri",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_Ilceler_IlId",
                table: "Ilceler",
                column: "IlId");

            migrationBuilder.CreateIndex(
                name: "IX_Ilceler_KaydedenId",
                table: "Ilceler",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_Iller_KaydedenId",
                table: "Iller",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_IllerMdEPosta_IlId",
                table: "IllerMdEPosta",
                column: "IlId");

            migrationBuilder.CreateIndex(
                name: "IX_IllerMdEPosta_KaydedenId",
                table: "IllerMdEPosta",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_Kitalar_UlkeGrupId",
                table: "Kitalar",
                column: "UlkeGrupId");

            migrationBuilder.CreateIndex(
                name: "IX_Komisyonlar_KaydedenId",
                table: "Komisyonlar",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_Komisyonlar_MulakatId",
                table: "Komisyonlar",
                column: "MulakatId");

            migrationBuilder.CreateIndex(
                name: "IX_Mulakatlar_DereceId",
                table: "Mulakatlar",
                column: "DereceId");

            migrationBuilder.CreateIndex(
                name: "IX_Mulakatlar_KaydedenId",
                table: "Mulakatlar",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_MulakatSorulari_KaydedenId",
                table: "MulakatSorulari",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_MulakatSorulari_MulakatId",
                table: "MulakatSorulari",
                column: "MulakatId");

            migrationBuilder.CreateIndex(
                name: "IX_Notlar_KaydedenId",
                table: "Notlar",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_Ogrenciler_AdayGorevKaydiId",
                table: "Ogrenciler",
                column: "AdayGorevKaydiId");

            migrationBuilder.CreateIndex(
                name: "IX_Ogrenciler_EyaletlerEyaletId",
                table: "Ogrenciler",
                column: "EyaletlerEyaletId");

            migrationBuilder.CreateIndex(
                name: "IX_Ogrenciler_KaydedenId",
                table: "Ogrenciler",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_Ogrenciler_OkullarOkulId",
                table: "Ogrenciler",
                column: "OkullarOkulId");

            migrationBuilder.CreateIndex(
                name: "IX_Ogrenciler_SehirlerSehirId",
                table: "Ogrenciler",
                column: "SehirlerSehirId");

            migrationBuilder.CreateIndex(
                name: "IX_Ogrenciler_SiniflarSinifId",
                table: "Ogrenciler",
                column: "SiniflarSinifId");

            migrationBuilder.CreateIndex(
                name: "IX_Ogrenciler_SubelerSubeId",
                table: "Ogrenciler",
                column: "SubelerSubeId");

            migrationBuilder.CreateIndex(
                name: "IX_Ogrenciler_TemsilciliklerTemsilcilikId",
                table: "Ogrenciler",
                column: "TemsilciliklerTemsilcilikId");

            migrationBuilder.CreateIndex(
                name: "IX_Ogrenciler_UlkeId",
                table: "Ogrenciler",
                column: "UlkeId");

            migrationBuilder.CreateIndex(
                name: "IX_Ogrenciler_UniversitelerUniId",
                table: "Ogrenciler",
                column: "UniversitelerUniId");

            migrationBuilder.CreateIndex(
                name: "IX_OkulBilgi_KaydedenId",
                table: "OkulBilgi",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_OkulBilgi_OkulId",
                table: "OkulBilgi",
                column: "OkulId");

            migrationBuilder.CreateIndex(
                name: "IX_OkulBinaBolum_KaydedenId",
                table: "OkulBinaBolum",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_OkulBinaBolum_OkulId",
                table: "OkulBinaBolum",
                column: "OkulId");

            migrationBuilder.CreateIndex(
                name: "IX_Okullar_EyaletlerEyaletId",
                table: "Okullar",
                column: "EyaletlerEyaletId");

            migrationBuilder.CreateIndex(
                name: "IX_Okullar_KaydedenId",
                table: "Okullar",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_Okullar_SehirId",
                table: "Okullar",
                column: "SehirId");

            migrationBuilder.CreateIndex(
                name: "IX_Okullar_UlkelerUlkeId",
                table: "Okullar",
                column: "UlkelerUlkeId");

            migrationBuilder.CreateIndex(
                name: "IX_Personeller_KaydedenId",
                table: "Personeller",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_Personeller_TemsilcilikId",
                table: "Personeller",
                column: "TemsilcilikId");

            migrationBuilder.CreateIndex(
                name: "IX_Sehirler_EyaletlerEyaletId",
                table: "Sehirler",
                column: "EyaletlerEyaletId");

            migrationBuilder.CreateIndex(
                name: "IX_Sehirler_KaydedenId",
                table: "Sehirler",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_Sehirler_TemsilciliklerTemsilcilikId",
                table: "Sehirler",
                column: "TemsilciliklerTemsilcilikId");

            migrationBuilder.CreateIndex(
                name: "IX_Sehirler_UlkelerUlkeId",
                table: "Sehirler",
                column: "UlkelerUlkeId");

            migrationBuilder.CreateIndex(
                name: "IX_Siniflar_KaydedenId",
                table: "Siniflar",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_Siniflar_OkullarOkulId",
                table: "Siniflar",
                column: "OkullarOkulId");

            migrationBuilder.CreateIndex(
                name: "IX_Siniflar_SubeId",
                table: "Siniflar",
                column: "SubeId");

            migrationBuilder.CreateIndex(
                name: "IX_SoruBankasi_KaydedenId",
                table: "SoruBankasi",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_SoruBankasiLog_KaydedenId",
                table: "SoruBankasiLog",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_SoruBankasiLog_SoruBankasiId",
                table: "SoruBankasiLog",
                column: "SoruBankasiId");

            migrationBuilder.CreateIndex(
                name: "IX_SoruDerece_DereceId",
                table: "SoruDerece",
                column: "DereceId");

            migrationBuilder.CreateIndex(
                name: "IX_SoruDereceler_KaydedenId",
                table: "SoruDereceler",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_SoruKategori_KategoriId",
                table: "SoruKategori",
                column: "KategoriId");

            migrationBuilder.CreateIndex(
                name: "IX_SoruKategoriler_KaydedenId",
                table: "SoruKategoriler",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_SoruOnay_OnaylayanId",
                table: "SoruOnay",
                column: "OnaylayanId");

            migrationBuilder.CreateIndex(
                name: "IX_SoruOnay_SoruBankasiId",
                table: "SoruOnay",
                column: "SoruBankasiId");

            migrationBuilder.CreateIndex(
                name: "IX_SSS_KaydedenId",
                table: "SSS",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_SSSCevap_KaydedenId",
                table: "SSSCevap",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_SSSCevap_SSSId",
                table: "SSSCevap",
                column: "SSSId");

            migrationBuilder.CreateIndex(
                name: "IX_Subeler_KaydedenId",
                table: "Subeler",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_Subeler_OkulId",
                table: "Subeler",
                column: "OkulId");

            migrationBuilder.CreateIndex(
                name: "IX_Telefonlar_AdaylarAdayId",
                table: "Telefonlar",
                column: "AdaylarAdayId");

            migrationBuilder.CreateIndex(
                name: "IX_Telefonlar_KaydedenId",
                table: "Telefonlar",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_Telefonlar_OkullarOkulId",
                table: "Telefonlar",
                column: "OkullarOkulId");

            migrationBuilder.CreateIndex(
                name: "IX_Telefonlar_TemsilciliklerTemsilcilikId",
                table: "Telefonlar",
                column: "TemsilciliklerTemsilcilikId");

            migrationBuilder.CreateIndex(
                name: "IX_Temsilcilikler_KaydedenId",
                table: "Temsilcilikler",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_Temsilcilikler_UlkeId",
                table: "Temsilcilikler",
                column: "UlkeId");

            migrationBuilder.CreateIndex(
                name: "IX_UlkeGruplari_KaydedenId",
                table: "UlkeGruplari",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_Ulkeler_KaydedenId",
                table: "Ulkeler",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_Ulkeler_KitaId",
                table: "Ulkeler",
                column: "KitaId");

            migrationBuilder.CreateIndex(
                name: "IX_Universiteler_EyaletlerEyaletId",
                table: "Universiteler",
                column: "EyaletlerEyaletId");

            migrationBuilder.CreateIndex(
                name: "IX_Universiteler_KaydedenId",
                table: "Universiteler",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_Universiteler_SehirlerSehirId",
                table: "Universiteler",
                column: "SehirlerSehirId");

            migrationBuilder.CreateIndex(
                name: "IX_Universiteler_UlkelerUlkeId",
                table: "Universiteler",
                column: "UlkelerUlkeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdayDDK");

            migrationBuilder.DropTable(
                name: "AdayNot");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AutoHistory");

            migrationBuilder.DropTable(
                name: "Branslar");

            migrationBuilder.DropTable(
                name: "DosyaGaleri");

            migrationBuilder.DropTable(
                name: "EPostaAdresleri");

            migrationBuilder.DropTable(
                name: "FotoGaleri");

            migrationBuilder.DropTable(
                name: "GorevKararPdfGaleri");

            migrationBuilder.DropTable(
                name: "IkametAdresleri");

            migrationBuilder.DropTable(
                name: "IllerMdEPosta");

            migrationBuilder.DropTable(
                name: "Komisyonlar");

            migrationBuilder.DropTable(
                name: "MulakatSorulari");

            migrationBuilder.DropTable(
                name: "Notlar");

            migrationBuilder.DropTable(
                name: "Ogrenciler");

            migrationBuilder.DropTable(
                name: "OkulBilgi");

            migrationBuilder.DropTable(
                name: "Personeller");

            migrationBuilder.DropTable(
                name: "SoruBankasiLog");

            migrationBuilder.DropTable(
                name: "SoruDerece");

            migrationBuilder.DropTable(
                name: "SoruKategori");

            migrationBuilder.DropTable(
                name: "SoruOnay");

            migrationBuilder.DropTable(
                name: "SSSCevap");

            migrationBuilder.DropTable(
                name: "Telefonlar");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Duyurular");

            migrationBuilder.DropTable(
                name: "Etkinlikler");

            migrationBuilder.DropTable(
                name: "OkulBinaBolum");

            migrationBuilder.DropTable(
                name: "Ilceler");

            migrationBuilder.DropTable(
                name: "AdayGorevKaydi");

            migrationBuilder.DropTable(
                name: "Siniflar");

            migrationBuilder.DropTable(
                name: "SoruKategoriler");

            migrationBuilder.DropTable(
                name: "SoruBankasi");

            migrationBuilder.DropTable(
                name: "SSS");

            migrationBuilder.DropTable(
                name: "Iller");

            migrationBuilder.DropTable(
                name: "Adaylar");

            migrationBuilder.DropTable(
                name: "Universiteler");

            migrationBuilder.DropTable(
                name: "Subeler");

            migrationBuilder.DropTable(
                name: "Mulakatlar");

            migrationBuilder.DropTable(
                name: "Okullar");

            migrationBuilder.DropTable(
                name: "SoruDereceler");

            migrationBuilder.DropTable(
                name: "Sehirler");

            migrationBuilder.DropTable(
                name: "Eyaletler");

            migrationBuilder.DropTable(
                name: "Temsilcilikler");

            migrationBuilder.DropTable(
                name: "Ulkeler");

            migrationBuilder.DropTable(
                name: "Kitalar");

            migrationBuilder.DropTable(
                name: "UlkeGruplari");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
