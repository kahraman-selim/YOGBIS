using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace YOGBIS.Data.Migrations
{
    public partial class tabloyukleme1 : Migration
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
                    TcKimlikNo = table.Column<int>(nullable: true),
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
                    KaydedenId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Derecelers", x => x.DereceId);
                    table.ForeignKey(
                        name: "FK_Derecelers_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notlars",
                columns: table => new
                {
                    NotId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
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
                    table.PrimaryKey("PK_Notlars", x => x.NotId);
                    table.ForeignKey(
                        name: "FK_Notlars_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SoruBankasis",
                columns: table => new
                {
                    SoruBankasiId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    Soru = table.Column<string>(nullable: true),
                    Cevap = table.Column<string>(nullable: true),
                    SorulmaSayisi = table.Column<int>(nullable: false),
                    SoruDurumu = table.Column<bool>(nullable: false),
                    OnayDurumu = table.Column<int>(nullable: true),
                    OnayAciklama = table.Column<string>(nullable: true),
                    KaydedenId = table.Column<string>(nullable: true),
                    OnaylayanId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoruBankasis", x => x.SoruBankasiId);
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
                    KaydedenId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UlkeGruplaris", x => x.UlkeGrupId);
                    table.ForeignKey(
                        name: "FK_UlkeGruplaris_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
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
                    DereceId = table.Column<int>(nullable: false),
                    BaslamaTarihi = table.Column<DateTime>(nullable: false),
                    BitisTarihi = table.Column<DateTime>(nullable: false),
                    AdaySayisi = table.Column<int>(nullable: false),
                    SorulanSoruSayisi = table.Column<int>(nullable: false),
                    Durumu = table.Column<bool>(nullable: true),
                    MulakatAciklama = table.Column<string>(nullable: true),
                    KaydedenId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mulakatlars", x => x.MulakatId);
                    table.ForeignKey(
                        name: "FK_Mulakatlars_Derecelers_DereceId",
                        column: x => x.DereceId,
                        principalTable: "Derecelers",
                        principalColumn: "DereceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mulakatlars_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
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
                    KaydedenId = table.Column<string>(nullable: true)
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
                        name: "FK_SoruKategorilers_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SoruDereces",
                columns: table => new
                {
                    SoruId = table.Column<int>(nullable: false),
                    DereceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoruDereces", x => new { x.SoruId, x.DereceId });
                    table.ForeignKey(
                        name: "FK_SoruDereces_Derecelers_SoruId",
                        column: x => x.SoruId,
                        principalTable: "Derecelers",
                        principalColumn: "DereceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SoruDereces_SoruBankasis_SoruId",
                        column: x => x.SoruId,
                        principalTable: "SoruBankasis",
                        principalColumn: "SoruBankasiId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UlkeGruplariKitalars",
                columns: table => new
                {
                    UlkeGrupId = table.Column<int>(nullable: false),
                    KitaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UlkeGruplariKitalars", x => new { x.KitaId, x.UlkeGrupId });
                    table.ForeignKey(
                        name: "FK_UlkeGruplariKitalars_Kitalars_KitaId",
                        column: x => x.KitaId,
                        principalTable: "Kitalars",
                        principalColumn: "KitaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UlkeGruplariKitalars_UlkeGruplaris_UlkeGrupId",
                        column: x => x.UlkeGrupId,
                        principalTable: "UlkeGruplaris",
                        principalColumn: "UlkeGrupId",
                        onDelete: ReferentialAction.Cascade);
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
                    UlkeGrupId = table.Column<int>(nullable: false),
                    KaydedenId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ulkelers", x => x.UlkeId);
                    table.ForeignKey(
                        name: "FK_Ulkelers_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ulkelers_Kitalars_KitaId",
                        column: x => x.KitaId,
                        principalTable: "Kitalars",
                        principalColumn: "KitaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ulkelers_UlkeGruplaris_UlkeGrupId",
                        column: x => x.UlkeGrupId,
                        principalTable: "UlkeGruplaris",
                        principalColumn: "UlkeGrupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Adaylars",
                columns: table => new
                {
                    AdayId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    AdayTC = table.Column<int>(nullable: false),
                    KaydedenId = table.Column<string>(nullable: true),
                    AdayAd = table.Column<string>(nullable: true),
                    AdayAd2 = table.Column<string>(nullable: true),
                    AdaySoyad = table.Column<string>(nullable: true),
                    AdaySoyad2 = table.Column<string>(nullable: true),
                    MulakatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adaylars", x => x.AdayId);
                    table.ForeignKey(
                        name: "FK_Adaylars_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Adaylars_Mulakatlars_MulakatId",
                        column: x => x.MulakatId,
                        principalTable: "Mulakatlars",
                        principalColumn: "MulakatId",
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
                    DereceAdi = table.Column<string>(nullable: true),
                    Soru = table.Column<string>(nullable: true),
                    Cevap = table.Column<string>(nullable: true),
                    MulakatId = table.Column<int>(nullable: false),
                    KaydedenId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MulakatSorularis", x => x.MulakatSorulariId);
                    table.ForeignKey(
                        name: "FK_MulakatSorularis_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MulakatSorularis_Mulakatlars_MulakatId",
                        column: x => x.MulakatId,
                        principalTable: "Mulakatlars",
                        principalColumn: "MulakatId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SoruKategoris",
                columns: table => new
                {
                    SoruId = table.Column<int>(nullable: false),
                    KategoriId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoruKategoris", x => new { x.SoruId, x.KategoriId });
                    table.ForeignKey(
                        name: "FK_SoruKategoris_SoruKategorilers_KategoriId",
                        column: x => x.KategoriId,
                        principalTable: "SoruKategorilers",
                        principalColumn: "SoruKategorilerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SoruKategoris_SoruBankasis_SoruId",
                        column: x => x.SoruId,
                        principalTable: "SoruBankasis",
                        principalColumn: "SoruBankasiId",
                        onDelete: ReferentialAction.Cascade);
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
                    KaydedenId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eyaletlers", x => x.EyaletId);
                    table.ForeignKey(
                        name: "FK_Eyaletlers_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
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
                    KaydedenId = table.Column<string>(nullable: true)
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
                        name: "FK_Sehirlers_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    OkulLogo = table.Column<string>(nullable: true),
                    OkulFoto = table.Column<string>(nullable: true),
                    OkulLab = table.Column<bool>(nullable: true),
                    OkulKutuphane = table.Column<bool>(nullable: true),
                    OkulBilgi = table.Column<string>(nullable: true),
                    OkulAcilisTarihi = table.Column<DateTime>(nullable: false),
                    OkulDurumu = table.Column<bool>(nullable: true),
                    SehirId = table.Column<int>(nullable: false),
                    KaydedenId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Okullars", x => x.OkulId);
                    table.ForeignKey(
                        name: "FK_Okullars_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Okullars_Sehirlers_SehirId",
                        column: x => x.SehirId,
                        principalTable: "Sehirlers",
                        principalColumn: "SehirId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Universitelers",
                columns: table => new
                {
                    UniId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    UniAdi = table.Column<string>(nullable: true),
                    UniLogo = table.Column<string>(nullable: true),
                    UniBilgi = table.Column<string>(nullable: true),
                    SehirId = table.Column<int>(nullable: false),
                    KaydedenId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Universitelers", x => x.UniId);
                    table.ForeignKey(
                        name: "FK_Universitelers_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Universitelers_Sehirlers_SehirId",
                        column: x => x.SehirId,
                        principalTable: "Sehirlers",
                        principalColumn: "SehirId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ogretmenlers",
                columns: table => new
                {
                    OgretmenId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    OgretmenTC = table.Column<int>(nullable: false),
                    KaydedenId = table.Column<string>(nullable: true),
                    OgretmenAd = table.Column<string>(nullable: true),
                    OgretmenAd2 = table.Column<string>(nullable: true),
                    OgretmenSoyad = table.Column<string>(nullable: true),
                    OgretmenSoyad2 = table.Column<string>(nullable: true),
                    OkulId = table.Column<int>(nullable: true),
                    SehirId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ogretmenlers", x => x.OgretmenId);
                    table.ForeignKey(
                        name: "FK_Ogretmenlers_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ogretmenlers_Okullars_OkulId",
                        column: x => x.OkulId,
                        principalTable: "Okullars",
                        principalColumn: "OkulId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ogretmenlers_Sehirlers_SehirId",
                        column: x => x.SehirId,
                        principalTable: "Sehirlers",
                        principalColumn: "SehirId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subelers",
                columns: table => new
                {
                    SubeId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    OkulAdi = table.Column<string>(nullable: true),
                    SubeAcilisTarihi = table.Column<DateTime>(nullable: false),
                    OkulId = table.Column<int>(nullable: false),
                    KaydedenId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subelers", x => x.SubeId);
                    table.ForeignKey(
                        name: "FK_Subelers_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Subelers_Okullars_OkulId",
                        column: x => x.OkulId,
                        principalTable: "Okullars",
                        principalColumn: "OkulId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Okutmanlars",
                columns: table => new
                {
                    OkutmanId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    OkutmanTC = table.Column<int>(nullable: false),
                    KaydedenId = table.Column<string>(nullable: true),
                    OkutmanAd = table.Column<string>(nullable: true),
                    OkutmanAd2 = table.Column<string>(nullable: true),
                    OkutmanSoyad = table.Column<string>(nullable: true),
                    OkutmanSoyad2 = table.Column<string>(nullable: true),
                    UniId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Okutmanlars", x => x.OkutmanId);
                    table.ForeignKey(
                        name: "FK_Okutmanlars_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Okutmanlars_Universitelers_UniId",
                        column: x => x.UniId,
                        principalTable: "Universitelers",
                        principalColumn: "UniId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Siniflars",
                columns: table => new
                {
                    SinifId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    SinifAdi = table.Column<string>(nullable: true),
                    SinifAcilisTarihi = table.Column<DateTime>(nullable: false),
                    SubeId = table.Column<int>(nullable: false),
                    KaydedenId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Siniflars", x => x.SinifId);
                    table.ForeignKey(
                        name: "FK_Siniflars_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Siniflars_Subelers_SubeId",
                        column: x => x.SubeId,
                        principalTable: "Subelers",
                        principalColumn: "SubeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GorevKaydis",
                columns: table => new
                {
                    GorevId = table.Column<int>(nullable: false),
                    GorevliTC = table.Column<int>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    GörevAdi = table.Column<string>(nullable: true),
                    GorevBasTarihi = table.Column<DateTime>(nullable: true),
                    GorevBitisTarihi = table.Column<DateTime>(nullable: true),
                    GorevTarihi = table.Column<DateTime>(nullable: true),
                    GorevOnaySayi = table.Column<string>(nullable: true),
                    GorevYeri = table.Column<string>(nullable: true),
                    KaydedenId = table.Column<string>(nullable: true),
                    OgretmenlerOgretmenId = table.Column<int>(nullable: true),
                    OkutmanlarOkutmanId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GorevKaydis", x => new { x.GorevId, x.GorevliTC });
                    table.ForeignKey(
                        name: "FK_GorevKaydis_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GorevKaydis_Ogretmenlers_OgretmenlerOgretmenId",
                        column: x => x.OgretmenlerOgretmenId,
                        principalTable: "Ogretmenlers",
                        principalColumn: "OgretmenId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GorevKaydis_Okutmanlars_OkutmanlarOkutmanId",
                        column: x => x.OkutmanlarOkutmanId,
                        principalTable: "Okutmanlars",
                        principalColumn: "OkutmanId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ogrencilers",
                columns: table => new
                {
                    OgrencilerId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    SinifId = table.Column<int>(nullable: false),
                    Uyruk = table.Column<string>(nullable: true),
                    Cinsiyet = table.Column<bool>(nullable: false),
                    OkulKayitTarihi = table.Column<DateTime>(nullable: false),
                    KaydedenId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ogrencilers", x => x.OgrencilerId);
                    table.ForeignKey(
                        name: "FK_Ogrencilers_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ogrencilers_Siniflars_SinifId",
                        column: x => x.SinifId,
                        principalTable: "Siniflars",
                        principalColumn: "SinifId",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_Adaylars_KaydedenId",
                table: "Adaylars",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_Adaylars_MulakatId",
                table: "Adaylars",
                column: "MulakatId");

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
                name: "IX_Derecelers_KaydedenId",
                table: "Derecelers",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_Eyaletlers_KaydedenId",
                table: "Eyaletlers",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_Eyaletlers_UlkeId",
                table: "Eyaletlers",
                column: "UlkeId");

            migrationBuilder.CreateIndex(
                name: "IX_GorevKaydis_KaydedenId",
                table: "GorevKaydis",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_GorevKaydis_OgretmenlerOgretmenId",
                table: "GorevKaydis",
                column: "OgretmenlerOgretmenId");

            migrationBuilder.CreateIndex(
                name: "IX_GorevKaydis_OkutmanlarOkutmanId",
                table: "GorevKaydis",
                column: "OkutmanlarOkutmanId");

            migrationBuilder.CreateIndex(
                name: "IX_Mulakatlars_DereceId",
                table: "Mulakatlars",
                column: "DereceId");

            migrationBuilder.CreateIndex(
                name: "IX_Mulakatlars_KaydedenId",
                table: "Mulakatlars",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_MulakatSorularis_KaydedenId",
                table: "MulakatSorularis",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_MulakatSorularis_MulakatId",
                table: "MulakatSorularis",
                column: "MulakatId");

            migrationBuilder.CreateIndex(
                name: "IX_Notlars_KaydedenId",
                table: "Notlars",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_Ogrencilers_KaydedenId",
                table: "Ogrencilers",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_Ogrencilers_SinifId",
                table: "Ogrencilers",
                column: "SinifId");

            migrationBuilder.CreateIndex(
                name: "IX_Ogretmenlers_KaydedenId",
                table: "Ogretmenlers",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_Ogretmenlers_OkulId",
                table: "Ogretmenlers",
                column: "OkulId");

            migrationBuilder.CreateIndex(
                name: "IX_Ogretmenlers_SehirId",
                table: "Ogretmenlers",
                column: "SehirId");

            migrationBuilder.CreateIndex(
                name: "IX_Okullars_KaydedenId",
                table: "Okullars",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_Okullars_SehirId",
                table: "Okullars",
                column: "SehirId");

            migrationBuilder.CreateIndex(
                name: "IX_Okutmanlars_KaydedenId",
                table: "Okutmanlars",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_Okutmanlars_UniId",
                table: "Okutmanlars",
                column: "UniId");

            migrationBuilder.CreateIndex(
                name: "IX_Sehirlers_EyaletId",
                table: "Sehirlers",
                column: "EyaletId");

            migrationBuilder.CreateIndex(
                name: "IX_Sehirlers_KaydedenId",
                table: "Sehirlers",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_Siniflars_KaydedenId",
                table: "Siniflars",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_Siniflars_SubeId",
                table: "Siniflars",
                column: "SubeId");

            migrationBuilder.CreateIndex(
                name: "IX_SoruBankasis_KaydedenId",
                table: "SoruBankasis",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_SoruBankasis_OnaylayanId",
                table: "SoruBankasis",
                column: "OnaylayanId");

            migrationBuilder.CreateIndex(
                name: "IX_SoruKategorilers_DereceId",
                table: "SoruKategorilers",
                column: "DereceId");

            migrationBuilder.CreateIndex(
                name: "IX_SoruKategorilers_KaydedenId",
                table: "SoruKategorilers",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_SoruKategoris_KategoriId",
                table: "SoruKategoris",
                column: "KategoriId");

            migrationBuilder.CreateIndex(
                name: "IX_Subelers_KaydedenId",
                table: "Subelers",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_Subelers_OkulId",
                table: "Subelers",
                column: "OkulId");

            migrationBuilder.CreateIndex(
                name: "IX_UlkeGruplariKitalars_UlkeGrupId",
                table: "UlkeGruplariKitalars",
                column: "UlkeGrupId");

            migrationBuilder.CreateIndex(
                name: "IX_UlkeGruplaris_KaydedenId",
                table: "UlkeGruplaris",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_Ulkelers_KaydedenId",
                table: "Ulkelers",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_Ulkelers_KitaId",
                table: "Ulkelers",
                column: "KitaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ulkelers_UlkeGrupId",
                table: "Ulkelers",
                column: "UlkeGrupId");

            migrationBuilder.CreateIndex(
                name: "IX_Universitelers_KaydedenId",
                table: "Universitelers",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_Universitelers_SehirId",
                table: "Universitelers",
                column: "SehirId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Adaylars");

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
                name: "GorevKaydis");

            migrationBuilder.DropTable(
                name: "MulakatSorularis");

            migrationBuilder.DropTable(
                name: "Notlars");

            migrationBuilder.DropTable(
                name: "Ogrencilers");

            migrationBuilder.DropTable(
                name: "SoruDereces");

            migrationBuilder.DropTable(
                name: "SoruKategoris");

            migrationBuilder.DropTable(
                name: "UlkeGruplariKitalars");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Ogretmenlers");

            migrationBuilder.DropTable(
                name: "Okutmanlars");

            migrationBuilder.DropTable(
                name: "Mulakatlars");

            migrationBuilder.DropTable(
                name: "Siniflars");

            migrationBuilder.DropTable(
                name: "SoruKategorilers");

            migrationBuilder.DropTable(
                name: "SoruBankasis");

            migrationBuilder.DropTable(
                name: "Universitelers");

            migrationBuilder.DropTable(
                name: "Subelers");

            migrationBuilder.DropTable(
                name: "Derecelers");

            migrationBuilder.DropTable(
                name: "Okullars");

            migrationBuilder.DropTable(
                name: "Sehirlers");

            migrationBuilder.DropTable(
                name: "Eyaletlers");

            migrationBuilder.DropTable(
                name: "Ulkelers");

            migrationBuilder.DropTable(
                name: "Kitalars");

            migrationBuilder.DropTable(
                name: "UlkeGruplaris");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
