using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace YOGBIS.Data.Migrations
{
    public partial class ogrenciler : Migration
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
                name: "Kitalars",
                columns: table => new
                {
                    KitaId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    KitaAdi = table.Column<string>(nullable: true),
                    KitaAciklama = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kitalars", x => x.KitaId);
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
                name: "Derecelers",
                columns: table => new
                {
                    DereceId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    DereceAdi = table.Column<string>(nullable: true),
                    KullaniciId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Derecelers", x => x.DereceId);
                    table.ForeignKey(
                        name: "FK_Derecelers_AspNetUsers_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Mulakatlars",
                columns: table => new
                {
                    MulakatId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    OnaySayisi = table.Column<string>(nullable: true),
                    MulakatAdi = table.Column<string>(nullable: true),
                    Derecesi = table.Column<string>(nullable: true),
                    BaslamaTarihi = table.Column<DateTime>(nullable: false),
                    BitisTarihi = table.Column<DateTime>(nullable: false),
                    AdaySayisi = table.Column<int>(nullable: false),
                    SorulanSoruSayisi = table.Column<int>(nullable: false),
                    Durumu = table.Column<bool>(nullable: true),
                    MulakatAciklama = table.Column<string>(nullable: true),
                    KullaniciId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mulakatlars", x => x.MulakatId);
                    table.ForeignKey(
                        name: "FK_Mulakatlars_AspNetUsers_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UlkeGruplaris",
                columns: table => new
                {
                    UlkeGrupId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    UlkeGrupAdi = table.Column<string>(nullable: true),
                    UlkeGrupAciklama = table.Column<string>(nullable: true),
                    KullaniciId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UlkeGruplaris", x => x.UlkeGrupId);
                    table.ForeignKey(
                        name: "FK_UlkeGruplaris_AspNetUsers_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ulkelers",
                columns: table => new
                {
                    UlkeId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    UlkeAdi = table.Column<string>(nullable: true),
                    UlkeBayrak = table.Column<string>(nullable: true),
                    UlkeAciklama = table.Column<string>(nullable: true),
                    KitaId = table.Column<int>(nullable: false),
                    KullaniciId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ulkelers", x => x.UlkeId);
                    table.ForeignKey(
                        name: "FK_Ulkelers_Kitalars_KitaId",
                        column: x => x.KitaId,
                        principalTable: "Kitalars",
                        principalColumn: "KitaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ulkelers_AspNetUsers_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SoruKategorilers",
                columns: table => new
                {
                    SoruKategorilerId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    SoruKategorilerAdi = table.Column<string>(nullable: true),
                    SoruKategorilerKullanimi = table.Column<string>(nullable: true),
                    SoruKategorilerPuan = table.Column<int>(nullable: false),
                    DereceId = table.Column<int>(nullable: false),
                    KullaniciId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoruKategorilers", x => x.SoruKategorilerId);
                    table.ForeignKey(
                        name: "FK_SoruKategorilers_Derecelers_DereceId",
                        column: x => x.DereceId,
                        principalTable: "Derecelers",
                        principalColumn: "DereceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SoruKategorilers_AspNetUsers_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UlkeGruplariKitalars",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UlkeGrupId = table.Column<int>(nullable: false),
                    UlkeGruplariUlkeGrupId = table.Column<int>(nullable: true),
                    KitaId = table.Column<int>(nullable: false),
                    KitalarKitaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UlkeGruplariKitalars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UlkeGruplariKitalars_Kitalars_KitalarKitaId",
                        column: x => x.KitalarKitaId,
                        principalTable: "Kitalars",
                        principalColumn: "KitaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UlkeGruplariKitalars_UlkeGruplaris_UlkeGruplariUlkeGrupId",
                        column: x => x.UlkeGruplariUlkeGrupId,
                        principalTable: "UlkeGruplaris",
                        principalColumn: "UlkeGrupId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Eyaletlers",
                columns: table => new
                {
                    EyaletId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    EyaletAdi = table.Column<string>(nullable: true),
                    EyaletAciklama = table.Column<string>(nullable: true),
                    UlkeId = table.Column<int>(nullable: false),
                    KullaniciId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eyaletlers", x => x.EyaletId);
                    table.ForeignKey(
                        name: "FK_Eyaletlers_AspNetUsers_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Eyaletlers_Ulkelers_UlkeId",
                        column: x => x.UlkeId,
                        principalTable: "Ulkelers",
                        principalColumn: "UlkeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Okullars",
                columns: table => new
                {
                    OkulId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    OkulKodu = table.Column<int>(nullable: false),
                    OkulAdi = table.Column<string>(nullable: true),
                    UlkeId = table.Column<int>(nullable: false),
                    KullaniciId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Okullars", x => x.OkulId);
                    table.ForeignKey(
                        name: "FK_Okullars_AspNetUsers_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Okullars_Ulkelers_UlkeId",
                        column: x => x.UlkeId,
                        principalTable: "Ulkelers",
                        principalColumn: "UlkeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SoruBankasis",
                columns: table => new
                {
                    SoruBankasiId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    SoruKategorilerId = table.Column<int>(nullable: false),
                    Soru = table.Column<string>(nullable: true),
                    Cevap = table.Column<string>(nullable: true),
                    DereceId = table.Column<int>(nullable: false),
                    SorulmaSayisi = table.Column<int>(nullable: false),
                    SoruDurumu = table.Column<bool>(nullable: false),
                    KaydedenId = table.Column<string>(nullable: true),
                    OnaylayanId = table.Column<string>(nullable: true),
                    OnayDurumu = table.Column<int>(nullable: true),
                    OnayAciklama = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoruBankasis", x => x.SoruBankasiId);
                    table.ForeignKey(
                        name: "FK_SoruBankasis_Derecelers_DereceId",
                        column: x => x.DereceId,
                        principalTable: "Derecelers",
                        principalColumn: "DereceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SoruBankasis_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SoruBankasis_AspNetUsers_OnaylayanId",
                        column: x => x.OnaylayanId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SoruBankasis_SoruKategorilers_SoruKategorilerId",
                        column: x => x.SoruKategorilerId,
                        principalTable: "SoruKategorilers",
                        principalColumn: "SoruKategorilerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sehirlers",
                columns: table => new
                {
                    SehirId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    SehirAdi = table.Column<string>(nullable: true),
                    Baskent = table.Column<bool>(nullable: true),
                    SehirAciklama = table.Column<string>(nullable: true),
                    EyaletId = table.Column<int>(nullable: false),
                    KullaniciId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sehirlers", x => x.SehirId);
                    table.ForeignKey(
                        name: "FK_Sehirlers_Eyaletlers_EyaletId",
                        column: x => x.EyaletId,
                        principalTable: "Eyaletlers",
                        principalColumn: "EyaletId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sehirlers_AspNetUsers_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ogrencilers",
                columns: table => new
                {
                    OgrencilerId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    OkulId = table.Column<int>(nullable: false),
                    UlkeId = table.Column<int>(nullable: false),
                    TCEOg = table.Column<int>(nullable: false),
                    TCKOg = table.Column<int>(nullable: false),
                    DEOg = table.Column<int>(nullable: false),
                    DKOg = table.Column<int>(nullable: false),
                    Ay = table.Column<string>(nullable: true),
                    Yil = table.Column<string>(nullable: true),
                    KullaniciId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ogrencilers", x => x.OgrencilerId);
                    table.ForeignKey(
                        name: "FK_Ogrencilers_AspNetUsers_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ogrencilers_Okullars_OkulId",
                        column: x => x.OkulId,
                        principalTable: "Okullars",
                        principalColumn: "OkulId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ogrencilers_Ulkelers_UlkeId",
                        column: x => x.UlkeId,
                        principalTable: "Ulkelers",
                        principalColumn: "UlkeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OkulBilgis",
                columns: table => new
                {
                    OkulBilgiId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
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
                    OkulId = table.Column<int>(nullable: false),
                    UlkeId = table.Column<int>(nullable: false),
                    KullaniciId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OkulBilgis", x => x.OkulBilgiId);
                    table.ForeignKey(
                        name: "FK_OkulBilgis_AspNetUsers_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OkulBilgis_Okullars_OkulId",
                        column: x => x.OkulId,
                        principalTable: "Okullars",
                        principalColumn: "OkulId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OkulBilgis_Ulkelers_UlkeId",
                        column: x => x.UlkeId,
                        principalTable: "Ulkelers",
                        principalColumn: "UlkeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MulakatSorularis",
                columns: table => new
                {
                    MulakatSorulariId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    SoruSiraNo = table.Column<int>(nullable: false),
                    SoruId = table.Column<int>(nullable: false),
                    SoruKategoriId = table.Column<int>(nullable: false),
                    SoruKategoriAdi = table.Column<string>(nullable: true),
                    DereceId = table.Column<int>(nullable: false),
                    Soru = table.Column<string>(nullable: true),
                    Cevap = table.Column<string>(nullable: true),
                    MulakatId = table.Column<int>(nullable: false),
                    KullaniciId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MulakatSorularis", x => x.MulakatSorulariId);
                    table.ForeignKey(
                        name: "FK_MulakatSorularis_Derecelers_DereceId",
                        column: x => x.DereceId,
                        principalTable: "Derecelers",
                        principalColumn: "DereceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MulakatSorularis_AspNetUsers_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MulakatSorularis_Mulakatlars_MulakatId",
                        column: x => x.MulakatId,
                        principalTable: "Mulakatlars",
                        principalColumn: "MulakatId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MulakatSorularis_SoruBankasis_SoruId",
                        column: x => x.SoruId,
                        principalTable: "SoruBankasis",
                        principalColumn: "SoruBankasiId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MulakatSorularis_SoruKategorilers_SoruKategoriId",
                        column: x => x.SoruKategoriId,
                        principalTable: "SoruKategorilers",
                        principalColumn: "SoruKategorilerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SoruKategoris",
                columns: table => new
                {
                    SoruKategoriId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    SoruId = table.Column<int>(nullable: false),
                    SoruBankasiId = table.Column<int>(nullable: true),
                    KategoriId = table.Column<int>(nullable: false),
                    SoruKategorilerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoruKategoris", x => x.SoruKategoriId);
                    table.ForeignKey(
                        name: "FK_SoruKategoris_SoruBankasis_SoruBankasiId",
                        column: x => x.SoruBankasiId,
                        principalTable: "SoruBankasis",
                        principalColumn: "SoruBankasiId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SoruKategoris_SoruKategorilers_SoruKategorilerId",
                        column: x => x.SoruKategorilerId,
                        principalTable: "SoruKategorilers",
                        principalColumn: "SoruKategorilerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Kitalars",
                columns: new[] { "KitaId", "KitaAciklama", "KitaAdi" },
                values: new object[,]
                {
                    { 1, "Afrika Kıtası", "Afrika" },
                    { 2, "Antartika Kıtası", "Antartika" },
                    { 3, "Asya Kıtası", "Asya" },
                    { 4, "Avrupa Kıtası", "Avrupa" },
                    { 5, "Avustralya Kıtası", "Avustralya" },
                    { 6, "Güney Amerika Kıtası", "Güney Amerika" },
                    { 7, "Kuzey Amerika Kıtası", "Kuzey Amerika" }
                });

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
                name: "IX_Derecelers_KullaniciId",
                table: "Derecelers",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_Eyaletlers_KullaniciId",
                table: "Eyaletlers",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_Eyaletlers_UlkeId",
                table: "Eyaletlers",
                column: "UlkeId");

            migrationBuilder.CreateIndex(
                name: "IX_Mulakatlars_KullaniciId",
                table: "Mulakatlars",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_MulakatSorularis_DereceId",
                table: "MulakatSorularis",
                column: "DereceId");

            migrationBuilder.CreateIndex(
                name: "IX_MulakatSorularis_KullaniciId",
                table: "MulakatSorularis",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_MulakatSorularis_MulakatId",
                table: "MulakatSorularis",
                column: "MulakatId");

            migrationBuilder.CreateIndex(
                name: "IX_MulakatSorularis_SoruId",
                table: "MulakatSorularis",
                column: "SoruId");

            migrationBuilder.CreateIndex(
                name: "IX_MulakatSorularis_SoruKategoriId",
                table: "MulakatSorularis",
                column: "SoruKategoriId");

            migrationBuilder.CreateIndex(
                name: "IX_Ogrencilers_KullaniciId",
                table: "Ogrencilers",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_Ogrencilers_OkulId",
                table: "Ogrencilers",
                column: "OkulId");

            migrationBuilder.CreateIndex(
                name: "IX_Ogrencilers_UlkeId",
                table: "Ogrencilers",
                column: "UlkeId");

            migrationBuilder.CreateIndex(
                name: "IX_OkulBilgis_KullaniciId",
                table: "OkulBilgis",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_OkulBilgis_OkulId",
                table: "OkulBilgis",
                column: "OkulId");

            migrationBuilder.CreateIndex(
                name: "IX_OkulBilgis_UlkeId",
                table: "OkulBilgis",
                column: "UlkeId");

            migrationBuilder.CreateIndex(
                name: "IX_Okullars_KullaniciId",
                table: "Okullars",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_Okullars_UlkeId",
                table: "Okullars",
                column: "UlkeId");

            migrationBuilder.CreateIndex(
                name: "IX_Sehirlers_EyaletId",
                table: "Sehirlers",
                column: "EyaletId");

            migrationBuilder.CreateIndex(
                name: "IX_Sehirlers_KullaniciId",
                table: "Sehirlers",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_SoruBankasis_DereceId",
                table: "SoruBankasis",
                column: "DereceId");

            migrationBuilder.CreateIndex(
                name: "IX_SoruBankasis_KaydedenId",
                table: "SoruBankasis",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_SoruBankasis_OnaylayanId",
                table: "SoruBankasis",
                column: "OnaylayanId");

            migrationBuilder.CreateIndex(
                name: "IX_SoruBankasis_SoruKategorilerId",
                table: "SoruBankasis",
                column: "SoruKategorilerId");

            migrationBuilder.CreateIndex(
                name: "IX_SoruKategorilers_DereceId",
                table: "SoruKategorilers",
                column: "DereceId");

            migrationBuilder.CreateIndex(
                name: "IX_SoruKategorilers_KullaniciId",
                table: "SoruKategorilers",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_SoruKategoris_SoruBankasiId",
                table: "SoruKategoris",
                column: "SoruBankasiId");

            migrationBuilder.CreateIndex(
                name: "IX_SoruKategoris_SoruKategorilerId",
                table: "SoruKategoris",
                column: "SoruKategorilerId");

            migrationBuilder.CreateIndex(
                name: "IX_UlkeGruplariKitalars_KitalarKitaId",
                table: "UlkeGruplariKitalars",
                column: "KitalarKitaId");

            migrationBuilder.CreateIndex(
                name: "IX_UlkeGruplariKitalars_UlkeGruplariUlkeGrupId",
                table: "UlkeGruplariKitalars",
                column: "UlkeGruplariUlkeGrupId");

            migrationBuilder.CreateIndex(
                name: "IX_UlkeGruplaris_KullaniciId",
                table: "UlkeGruplaris",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_Ulkelers_KitaId",
                table: "Ulkelers",
                column: "KitaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ulkelers_KullaniciId",
                table: "Ulkelers",
                column: "KullaniciId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "MulakatSorularis");

            migrationBuilder.DropTable(
                name: "Ogrencilers");

            migrationBuilder.DropTable(
                name: "OkulBilgis");

            migrationBuilder.DropTable(
                name: "Sehirlers");

            migrationBuilder.DropTable(
                name: "SoruKategoris");

            migrationBuilder.DropTable(
                name: "UlkeGruplariKitalars");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Mulakatlars");

            migrationBuilder.DropTable(
                name: "Okullars");

            migrationBuilder.DropTable(
                name: "Eyaletlers");

            migrationBuilder.DropTable(
                name: "SoruBankasis");

            migrationBuilder.DropTable(
                name: "UlkeGruplaris");

            migrationBuilder.DropTable(
                name: "Ulkelers");

            migrationBuilder.DropTable(
                name: "SoruKategorilers");

            migrationBuilder.DropTable(
                name: "Kitalars");

            migrationBuilder.DropTable(
                name: "Derecelers");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
