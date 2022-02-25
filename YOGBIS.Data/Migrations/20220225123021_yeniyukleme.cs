using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace YOGBIS.Data.Migrations
{
    public partial class yeniyukleme : Migration
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
                name: "Kitalars",
                columns: table => new
                {
                    KitaId = table.Column<byte[]>(nullable: false),
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
                name: "Branslars",
                columns: table => new
                {
                    BransId = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    BransAdi = table.Column<string>(nullable: true),
                    KaydedenId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branslars", x => x.BransId);
                    table.ForeignKey(
                        name: "FK_Branslars_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Duyurulars",
                columns: table => new
                {
                    DuyurularId = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    DuyuruBaslık = table.Column<string>(nullable: true),
                    DuyuruDetay = table.Column<string>(nullable: true),
                    KaydedenId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Duyurulars", x => x.DuyurularId);
                    table.ForeignKey(
                        name: "FK_Duyurulars_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Illers",
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
                    table.PrimaryKey("PK_Illers", x => x.IlId);
                    table.ForeignKey(
                        name: "FK_Illers_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notlars",
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
                    table.PrimaryKey("PK_Notlars", x => x.NotId);
                    table.ForeignKey(
                        name: "FK_Notlars_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Personellers",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    PersonelGrupAdi = table.Column<string>(nullable: true),
                    PersonelAdiSoyadi = table.Column<string>(nullable: true),
                    PersonelUnvan = table.Column<string>(nullable: true),
                    PersonelTelefon = table.Column<string>(nullable: true),
                    PersonelEPosta = table.Column<string>(nullable: true),
                    PersonelGorevAlani = table.Column<string>(nullable: true),
                    KaydedenId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personellers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Personellers_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SoruBankasis",
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
                    table.PrimaryKey("PK_SoruBankasis", x => x.SoruBankasiId);
                    table.ForeignKey(
                        name: "FK_SoruBankasis_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SoruDerecelers",
                columns: table => new
                {
                    DereceId = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    DereceAdi = table.Column<string>(nullable: true),
                    KaydedenId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoruDerecelers", x => x.DereceId);
                    table.ForeignKey(
                        name: "FK_SoruDerecelers_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SoruKategorilers",
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
                    table.PrimaryKey("PK_SoruKategorilers", x => x.SoruKategorilerId);
                    table.ForeignKey(
                        name: "FK_SoruKategorilers_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SSSs",
                columns: table => new
                {
                    SSSId = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    Soru = table.Column<string>(nullable: true),
                    KaydedenId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SSSs", x => x.SSSId);
                    table.ForeignKey(
                        name: "FK_SSSs_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UlkeGruplaris",
                columns: table => new
                {
                    UlkeGrupId = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    UlkeGrupAdi = table.Column<string>(nullable: true),
                    UlkeGrupAciklama = table.Column<string>(nullable: true),
                    KaydedenId = table.Column<string>(nullable: true),
                    KitaId = table.Column<byte[]>(nullable: false)
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
                name: "Ulkelers",
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
                });

            migrationBuilder.CreateTable(
                name: "Ilcelers",
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
                    table.PrimaryKey("PK_Ilcelers", x => x.IlceId);
                    table.ForeignKey(
                        name: "FK_Ilcelers_Illers_IlId",
                        column: x => x.IlId,
                        principalTable: "Illers",
                        principalColumn: "IlId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ilcelers_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IllerMdEPostas",
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
                    table.PrimaryKey("PK_IllerMdEPostas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IllerMdEPostas_Illers_IlId",
                        column: x => x.IlId,
                        principalTable: "Illers",
                        principalColumn: "IlId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IllerMdEPostas_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SoruBankasiLogs",
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
                    table.PrimaryKey("PK_SoruBankasiLogs", x => x.SoruBankasiLogId);
                    table.ForeignKey(
                        name: "FK_SoruBankasiLogs_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SoruBankasiLogs_SoruBankasis_SoruBankasiId",
                        column: x => x.SoruBankasiId,
                        principalTable: "SoruBankasis",
                        principalColumn: "SoruBankasiId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SoruOnays",
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
                    table.PrimaryKey("PK_SoruOnays", x => x.SoruOnayId);
                    table.ForeignKey(
                        name: "FK_SoruOnays_AspNetUsers_OnaylayanId",
                        column: x => x.OnaylayanId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SoruOnays_SoruBankasis_SoruBankasiId",
                        column: x => x.SoruBankasiId,
                        principalTable: "SoruBankasis",
                        principalColumn: "SoruBankasiId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mulakatlars",
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
                    table.PrimaryKey("PK_Mulakatlars", x => x.MulakatId);
                    table.ForeignKey(
                        name: "FK_Mulakatlars_SoruDerecelers_DereceId",
                        column: x => x.DereceId,
                        principalTable: "SoruDerecelers",
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
                name: "SoruDereces",
                columns: table => new
                {
                    SoruId = table.Column<byte[]>(nullable: false),
                    DereceId = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoruDereces", x => new { x.SoruId, x.DereceId });
                    table.ForeignKey(
                        name: "FK_SoruDereces_SoruDerecelers_DereceId",
                        column: x => x.DereceId,
                        principalTable: "SoruDerecelers",
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
                name: "SoruKategoris",
                columns: table => new
                {
                    SoruId = table.Column<byte[]>(nullable: false),
                    KategoriId = table.Column<byte[]>(nullable: false)
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
                name: "SSSCevaps",
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
                    table.PrimaryKey("PK_SSSCevaps", x => x.SSSCevapId);
                    table.ForeignKey(
                        name: "FK_SSSCevaps_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SSSCevaps_SSSs_SSSId",
                        column: x => x.SSSId,
                        principalTable: "SSSs",
                        principalColumn: "SSSId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Eyaletlers",
                columns: table => new
                {
                    EyaletId = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    EyaletAdi = table.Column<string>(nullable: true),
                    EyaletAciklama = table.Column<string>(nullable: true),
                    TemsilciId = table.Column<string>(nullable: true),
                    UlkeId = table.Column<byte[]>(nullable: false),
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
                name: "IkametAdresleris",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    IkametIlId = table.Column<byte[]>(nullable: false),
                    IkametIlceId = table.Column<byte[]>(nullable: false),
                    PostaKodu = table.Column<string>(nullable: true),
                    AdresTuru = table.Column<string>(nullable: true),
                    IkametAdresi = table.Column<string>(nullable: true),
                    KaydedenId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IkametAdresleris", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IkametAdresleris_Ilcelers_IkametIlceId",
                        column: x => x.IkametIlceId,
                        principalTable: "Ilcelers",
                        principalColumn: "IlceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IkametAdresleris_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Adaylars",
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
                    KaydedenId = table.Column<string>(nullable: true),
                    AdayFotoURL = table.Column<string>(nullable: true)
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
                name: "Komisyonlars",
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
                    table.PrimaryKey("PK_Komisyonlars", x => x.KomisyonId);
                    table.ForeignKey(
                        name: "FK_Komisyonlars_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Komisyonlars_Mulakatlars_MulakatId",
                        column: x => x.MulakatId,
                        principalTable: "Mulakatlars",
                        principalColumn: "MulakatId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MulakatSorularis",
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
                name: "Sehirlers",
                columns: table => new
                {
                    SehirId = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    SehirAdi = table.Column<string>(nullable: true),
                    Baskent = table.Column<bool>(nullable: true),
                    SehirAciklama = table.Column<string>(nullable: true),
                    SehirVatandas = table.Column<int>(nullable: false),
                    TemsilciId = table.Column<string>(nullable: true),
                    UlkeId = table.Column<byte[]>(nullable: false),
                    EyaletId = table.Column<byte[]>(nullable: false),
                    KaydedenId = table.Column<string>(nullable: true),
                    UlkelerUlkeId = table.Column<byte[]>(nullable: true)
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
                    table.ForeignKey(
                        name: "FK_Sehirlers_Ulkelers_UlkelerUlkeId",
                        column: x => x.UlkelerUlkeId,
                        principalTable: "Ulkelers",
                        principalColumn: "UlkeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AdayDDKs",
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
                    table.PrimaryKey("PK_AdayDDKs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdayDDKs_Adaylars_AdayId",
                        column: x => x.AdayId,
                        principalTable: "Adaylars",
                        principalColumn: "AdayId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdayDDKs_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AdayNots",
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
                    table.PrimaryKey("PK_AdayNots", x => x.AdayNotId);
                    table.ForeignKey(
                        name: "FK_AdayNots_Adaylars_AdayId",
                        column: x => x.AdayId,
                        principalTable: "Adaylars",
                        principalColumn: "AdayId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdayNots_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Okullars",
                columns: table => new
                {
                    OkulId = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    OkulKodu = table.Column<int>(nullable: false),
                    OkulAdi = table.Column<string>(nullable: true),
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
                    OkulUlkeId = table.Column<byte[]>(nullable: true),
                    SehirId = table.Column<byte[]>(nullable: true),
                    EyaletId = table.Column<byte[]>(nullable: true),
                    KaydedenId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Okullars", x => x.OkulId);
                    table.ForeignKey(
                        name: "FK_Okullars_Eyaletlers_EyaletId",
                        column: x => x.EyaletId,
                        principalTable: "Eyaletlers",
                        principalColumn: "EyaletId",
                        onDelete: ReferentialAction.Restrict);
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Temsilciliklers",
                columns: table => new
                {
                    TemsilcilikId = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    TemsilcilikAdi = table.Column<string>(nullable: true),
                    TemsilciId = table.Column<string>(nullable: true),
                    TemsilcilikTel = table.Column<string>(nullable: true),
                    TemsilcilikEPosta = table.Column<string>(nullable: true),
                    TemsilcilikWebAdres = table.Column<string>(nullable: true),
                    SehirId = table.Column<byte[]>(nullable: false),
                    KaydedenId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Temsilciliklers", x => x.TemsilcilikId);
                    table.ForeignKey(
                        name: "FK_Temsilciliklers_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Temsilciliklers_Sehirlers_SehirId",
                        column: x => x.SehirId,
                        principalTable: "Sehirlers",
                        principalColumn: "SehirId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Universitelers",
                columns: table => new
                {
                    UniId = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    UniAdi = table.Column<string>(nullable: true),
                    YurtIciMi = table.Column<bool>(nullable: false),
                    UniStatu = table.Column<string>(nullable: true),
                    UniLogo = table.Column<string>(nullable: true),
                    UniBilgi = table.Column<string>(nullable: true),
                    SehirId = table.Column<byte[]>(nullable: false),
                    EyaletId = table.Column<byte[]>(nullable: false),
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
                name: "OkulBilgis",
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
                });

            migrationBuilder.CreateTable(
                name: "OkulBinaBolums",
                columns: table => new
                {
                    OkulBinaBolumId = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    BolumAdi = table.Column<string>(nullable: true),
                    OkulId = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OkulBinaBolums", x => x.OkulBinaBolumId);
                    table.ForeignKey(
                        name: "FK_OkulBinaBolums_Okullars_OkulId",
                        column: x => x.OkulId,
                        principalTable: "Okullars",
                        principalColumn: "OkulId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subelers",
                columns: table => new
                {
                    SubeId = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    OkulAdi = table.Column<string>(nullable: true),
                    SubeAcilisTarihi = table.Column<DateTime>(nullable: false),
                    OkulId = table.Column<byte[]>(nullable: false),
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
                name: "Etkinliklers",
                columns: table => new
                {
                    EtkinlikId = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    EtkinlikAdi = table.Column<string>(nullable: true),
                    OkulId = table.Column<byte[]>(nullable: false),
                    TemsilcilikId = table.Column<byte[]>(nullable: false),
                    BasTarihi = table.Column<DateTime>(nullable: false),
                    BitTarihi = table.Column<DateTime>(nullable: false),
                    EtkinlikBilgi = table.Column<string>(nullable: true),
                    KatilimciSayisi = table.Column<int>(nullable: false),
                    DuzenleyenAdiSoyadi = table.Column<string>(nullable: true),
                    EtkinlikKapakResimUrl = table.Column<string>(nullable: true),
                    KaydedenId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etkinliklers", x => x.EtkinlikId);
                    table.ForeignKey(
                        name: "FK_Etkinliklers_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Etkinliklers_Okullars_OkulId",
                        column: x => x.OkulId,
                        principalTable: "Okullars",
                        principalColumn: "OkulId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Etkinliklers_Temsilciliklers_TemsilcilikId",
                        column: x => x.TemsilcilikId,
                        principalTable: "Temsilciliklers",
                        principalColumn: "TemsilcilikId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdayGorevKaydis",
                columns: table => new
                {
                    AdayGorevKaydiId = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    GorevliTC = table.Column<string>(nullable: true),
                    DereceId = table.Column<byte[]>(nullable: false),
                    Gorevi = table.Column<string>(nullable: true),
                    BransId = table.Column<byte[]>(nullable: false),
                    GorevOnaySayi = table.Column<string>(nullable: true),
                    GorevlOnayTarihi = table.Column<DateTime>(nullable: false),
                    KararPdfUrl = table.Column<string>(nullable: true),
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
                    OkullarOkulId = table.Column<byte[]>(nullable: true),
                    SehirlerSehirId = table.Column<byte[]>(nullable: true),
                    UniversitelerUniId = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdayGorevKaydis", x => x.AdayGorevKaydiId);
                    table.ForeignKey(
                        name: "FK_AdayGorevKaydis_Branslars_BransId",
                        column: x => x.BransId,
                        principalTable: "Branslars",
                        principalColumn: "BransId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdayGorevKaydis_SoruDerecelers_DereceId",
                        column: x => x.DereceId,
                        principalTable: "SoruDerecelers",
                        principalColumn: "DereceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdayGorevKaydis_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdayGorevKaydis_Okullars_OkullarOkulId",
                        column: x => x.OkullarOkulId,
                        principalTable: "Okullars",
                        principalColumn: "OkulId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdayGorevKaydis_Sehirlers_SehirlerSehirId",
                        column: x => x.SehirlerSehirId,
                        principalTable: "Sehirlers",
                        principalColumn: "SehirId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdayGorevKaydis_Ulkelers_UlkeId",
                        column: x => x.UlkeId,
                        principalTable: "Ulkelers",
                        principalColumn: "UlkeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdayGorevKaydis_Universitelers_UniversitelerUniId",
                        column: x => x.UniversitelerUniId,
                        principalTable: "Universitelers",
                        principalColumn: "UniId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Siniflars",
                columns: table => new
                {
                    SinifId = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    SinifAdi = table.Column<string>(nullable: true),
                    SinifAcilisTarihi = table.Column<DateTime>(nullable: false),
                    SubeId = table.Column<byte[]>(nullable: false),
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
                name: "DosyaGaleris",
                columns: table => new
                {
                    DosyaGaleriId = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    DosyaAdi = table.Column<string>(nullable: true),
                    DosyaURL = table.Column<string>(nullable: true),
                    KaydedenId = table.Column<string>(nullable: true),
                    EtkinliklerEtkinlikId = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DosyaGaleris", x => x.DosyaGaleriId);
                    table.ForeignKey(
                        name: "FK_DosyaGaleris_Etkinliklers_EtkinliklerEtkinlikId",
                        column: x => x.EtkinliklerEtkinlikId,
                        principalTable: "Etkinliklers",
                        principalColumn: "EtkinlikId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DosyaGaleris_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EPostaAdresleris",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    EpostaAdresi = table.Column<string>(nullable: true),
                    KaydedenId = table.Column<string>(nullable: true),
                    AdayGorevKaydiId = table.Column<byte[]>(nullable: true),
                    OkullarOkulId = table.Column<byte[]>(nullable: true),
                    TemsilciliklerTemsilcilikId = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EPostaAdresleris", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EPostaAdresleris_AdayGorevKaydis_AdayGorevKaydiId",
                        column: x => x.AdayGorevKaydiId,
                        principalTable: "AdayGorevKaydis",
                        principalColumn: "AdayGorevKaydiId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EPostaAdresleris_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EPostaAdresleris_Okullars_OkullarOkulId",
                        column: x => x.OkullarOkulId,
                        principalTable: "Okullars",
                        principalColumn: "OkulId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EPostaAdresleris_Temsilciliklers_TemsilciliklerTemsilcilikId",
                        column: x => x.TemsilciliklerTemsilcilikId,
                        principalTable: "Temsilciliklers",
                        principalColumn: "TemsilcilikId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FotoGaleris",
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
                    UlkelerUlkeId = table.Column<byte[]>(nullable: true),
                    UniversitelerUniId = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FotoGaleris", x => x.FotoGaleriId);
                    table.ForeignKey(
                        name: "FK_FotoGaleris_AdayGorevKaydis_AdayGorevKaydiId",
                        column: x => x.AdayGorevKaydiId,
                        principalTable: "AdayGorevKaydis",
                        principalColumn: "AdayGorevKaydiId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FotoGaleris_Adaylars_AdaylarAdayId",
                        column: x => x.AdaylarAdayId,
                        principalTable: "Adaylars",
                        principalColumn: "AdayId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FotoGaleris_Duyurulars_DuyurularId",
                        column: x => x.DuyurularId,
                        principalTable: "Duyurulars",
                        principalColumn: "DuyurularId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FotoGaleris_Etkinliklers_EtkinliklerEtkinlikId",
                        column: x => x.EtkinliklerEtkinlikId,
                        principalTable: "Etkinliklers",
                        principalColumn: "EtkinlikId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FotoGaleris_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FotoGaleris_OkulBinaBolums_OkulBinaBolumId",
                        column: x => x.OkulBinaBolumId,
                        principalTable: "OkulBinaBolums",
                        principalColumn: "OkulBinaBolumId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FotoGaleris_Okullars_OkullarOkulId",
                        column: x => x.OkullarOkulId,
                        principalTable: "Okullars",
                        principalColumn: "OkulId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FotoGaleris_Sehirlers_SehirlerSehirId",
                        column: x => x.SehirlerSehirId,
                        principalTable: "Sehirlers",
                        principalColumn: "SehirId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FotoGaleris_Ulkelers_UlkelerUlkeId",
                        column: x => x.UlkelerUlkeId,
                        principalTable: "Ulkelers",
                        principalColumn: "UlkeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FotoGaleris_Universitelers_UniversitelerUniId",
                        column: x => x.UniversitelerUniId,
                        principalTable: "Universitelers",
                        principalColumn: "UniId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GorevKararPdfGaleris",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    GorevKararPdfUrl = table.Column<string>(nullable: true),
                    AdayGorevKaydiId = table.Column<byte[]>(nullable: false),
                    KaydedenId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GorevKararPdfGaleris", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GorevKararPdfGaleris_AdayGorevKaydis_AdayGorevKaydiId",
                        column: x => x.AdayGorevKaydiId,
                        principalTable: "AdayGorevKaydis",
                        principalColumn: "AdayGorevKaydiId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GorevKararPdfGaleris_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Telefonlars",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    TelefonNumarası = table.Column<string>(nullable: true),
                    KaydedenId = table.Column<string>(nullable: true),
                    AdayGorevKaydiId = table.Column<byte[]>(nullable: true),
                    OkullarOkulId = table.Column<byte[]>(nullable: true),
                    TemsilciliklerTemsilcilikId = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telefonlars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Telefonlars_AdayGorevKaydis_AdayGorevKaydiId",
                        column: x => x.AdayGorevKaydiId,
                        principalTable: "AdayGorevKaydis",
                        principalColumn: "AdayGorevKaydiId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Telefonlars_AspNetUsers_KaydedenId",
                        column: x => x.KaydedenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Telefonlars_Okullars_OkullarOkulId",
                        column: x => x.OkullarOkulId,
                        principalTable: "Okullars",
                        principalColumn: "OkulId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Telefonlars_Temsilciliklers_TemsilciliklerTemsilcilikId",
                        column: x => x.TemsilciliklerTemsilcilikId,
                        principalTable: "Temsilciliklers",
                        principalColumn: "TemsilcilikId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ogrencilers",
                columns: table => new
                {
                    OgrencilerId = table.Column<byte[]>(nullable: false),
                    KayitTarihi = table.Column<DateTime>(nullable: false),
                    Uyruk = table.Column<string>(nullable: true),
                    Cinsiyet = table.Column<bool>(nullable: false),
                    OkulKayitTarihi = table.Column<DateTime>(nullable: false),
                    OkulId = table.Column<byte[]>(nullable: true),
                    SinifId = table.Column<byte[]>(nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_AdayDDKs_AdayId",
                table: "AdayDDKs",
                column: "AdayId");

            migrationBuilder.CreateIndex(
                name: "IX_AdayDDKs_KaydedenId",
                table: "AdayDDKs",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_AdayGorevKaydis_BransId",
                table: "AdayGorevKaydis",
                column: "BransId");

            migrationBuilder.CreateIndex(
                name: "IX_AdayGorevKaydis_DereceId",
                table: "AdayGorevKaydis",
                column: "DereceId");

            migrationBuilder.CreateIndex(
                name: "IX_AdayGorevKaydis_KaydedenId",
                table: "AdayGorevKaydis",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_AdayGorevKaydis_OkullarOkulId",
                table: "AdayGorevKaydis",
                column: "OkullarOkulId");

            migrationBuilder.CreateIndex(
                name: "IX_AdayGorevKaydis_SehirlerSehirId",
                table: "AdayGorevKaydis",
                column: "SehirlerSehirId");

            migrationBuilder.CreateIndex(
                name: "IX_AdayGorevKaydis_UlkeId",
                table: "AdayGorevKaydis",
                column: "UlkeId");

            migrationBuilder.CreateIndex(
                name: "IX_AdayGorevKaydis_UniversitelerUniId",
                table: "AdayGorevKaydis",
                column: "UniversitelerUniId");

            migrationBuilder.CreateIndex(
                name: "IX_Adaylars_KaydedenId",
                table: "Adaylars",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_Adaylars_MulakatId",
                table: "Adaylars",
                column: "MulakatId");

            migrationBuilder.CreateIndex(
                name: "IX_AdayNots_AdayId",
                table: "AdayNots",
                column: "AdayId");

            migrationBuilder.CreateIndex(
                name: "IX_AdayNots_KaydedenId",
                table: "AdayNots",
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
                name: "IX_Branslars_KaydedenId",
                table: "Branslars",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_DosyaGaleris_EtkinliklerEtkinlikId",
                table: "DosyaGaleris",
                column: "EtkinliklerEtkinlikId");

            migrationBuilder.CreateIndex(
                name: "IX_DosyaGaleris_KaydedenId",
                table: "DosyaGaleris",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_Duyurulars_KaydedenId",
                table: "Duyurulars",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_EPostaAdresleris_AdayGorevKaydiId",
                table: "EPostaAdresleris",
                column: "AdayGorevKaydiId");

            migrationBuilder.CreateIndex(
                name: "IX_EPostaAdresleris_KaydedenId",
                table: "EPostaAdresleris",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_EPostaAdresleris_OkullarOkulId",
                table: "EPostaAdresleris",
                column: "OkullarOkulId");

            migrationBuilder.CreateIndex(
                name: "IX_EPostaAdresleris_TemsilciliklerTemsilcilikId",
                table: "EPostaAdresleris",
                column: "TemsilciliklerTemsilcilikId");

            migrationBuilder.CreateIndex(
                name: "IX_Etkinliklers_KaydedenId",
                table: "Etkinliklers",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_Etkinliklers_OkulId",
                table: "Etkinliklers",
                column: "OkulId");

            migrationBuilder.CreateIndex(
                name: "IX_Etkinliklers_TemsilcilikId",
                table: "Etkinliklers",
                column: "TemsilcilikId");

            migrationBuilder.CreateIndex(
                name: "IX_Eyaletlers_KaydedenId",
                table: "Eyaletlers",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_Eyaletlers_UlkeId",
                table: "Eyaletlers",
                column: "UlkeId");

            migrationBuilder.CreateIndex(
                name: "IX_FotoGaleris_AdayGorevKaydiId",
                table: "FotoGaleris",
                column: "AdayGorevKaydiId");

            migrationBuilder.CreateIndex(
                name: "IX_FotoGaleris_AdaylarAdayId",
                table: "FotoGaleris",
                column: "AdaylarAdayId");

            migrationBuilder.CreateIndex(
                name: "IX_FotoGaleris_DuyurularId",
                table: "FotoGaleris",
                column: "DuyurularId");

            migrationBuilder.CreateIndex(
                name: "IX_FotoGaleris_EtkinliklerEtkinlikId",
                table: "FotoGaleris",
                column: "EtkinliklerEtkinlikId");

            migrationBuilder.CreateIndex(
                name: "IX_FotoGaleris_KaydedenId",
                table: "FotoGaleris",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_FotoGaleris_OkulBinaBolumId",
                table: "FotoGaleris",
                column: "OkulBinaBolumId");

            migrationBuilder.CreateIndex(
                name: "IX_FotoGaleris_OkullarOkulId",
                table: "FotoGaleris",
                column: "OkullarOkulId");

            migrationBuilder.CreateIndex(
                name: "IX_FotoGaleris_SehirlerSehirId",
                table: "FotoGaleris",
                column: "SehirlerSehirId");

            migrationBuilder.CreateIndex(
                name: "IX_FotoGaleris_UlkelerUlkeId",
                table: "FotoGaleris",
                column: "UlkelerUlkeId");

            migrationBuilder.CreateIndex(
                name: "IX_FotoGaleris_UniversitelerUniId",
                table: "FotoGaleris",
                column: "UniversitelerUniId");

            migrationBuilder.CreateIndex(
                name: "IX_GorevKararPdfGaleris_AdayGorevKaydiId",
                table: "GorevKararPdfGaleris",
                column: "AdayGorevKaydiId");

            migrationBuilder.CreateIndex(
                name: "IX_GorevKararPdfGaleris_KaydedenId",
                table: "GorevKararPdfGaleris",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_IkametAdresleris_IkametIlceId",
                table: "IkametAdresleris",
                column: "IkametIlceId");

            migrationBuilder.CreateIndex(
                name: "IX_IkametAdresleris_KaydedenId",
                table: "IkametAdresleris",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_Ilcelers_IlId",
                table: "Ilcelers",
                column: "IlId");

            migrationBuilder.CreateIndex(
                name: "IX_Ilcelers_KaydedenId",
                table: "Ilcelers",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_IllerMdEPostas_IlId",
                table: "IllerMdEPostas",
                column: "IlId");

            migrationBuilder.CreateIndex(
                name: "IX_IllerMdEPostas_KaydedenId",
                table: "IllerMdEPostas",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_Illers_KaydedenId",
                table: "Illers",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_Komisyonlars_KaydedenId",
                table: "Komisyonlars",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_Komisyonlars_MulakatId",
                table: "Komisyonlars",
                column: "MulakatId");

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
                name: "IX_OkulBilgis_KullaniciId",
                table: "OkulBilgis",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_OkulBilgis_OkulId",
                table: "OkulBilgis",
                column: "OkulId");

            migrationBuilder.CreateIndex(
                name: "IX_OkulBinaBolums_OkulId",
                table: "OkulBinaBolums",
                column: "OkulId");

            migrationBuilder.CreateIndex(
                name: "IX_Okullars_EyaletId",
                table: "Okullars",
                column: "EyaletId");

            migrationBuilder.CreateIndex(
                name: "IX_Okullars_KaydedenId",
                table: "Okullars",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_Okullars_SehirId",
                table: "Okullars",
                column: "SehirId");

            migrationBuilder.CreateIndex(
                name: "IX_Personellers_KaydedenId",
                table: "Personellers",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_Sehirlers_EyaletId",
                table: "Sehirlers",
                column: "EyaletId");

            migrationBuilder.CreateIndex(
                name: "IX_Sehirlers_KaydedenId",
                table: "Sehirlers",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_Sehirlers_UlkelerUlkeId",
                table: "Sehirlers",
                column: "UlkelerUlkeId");

            migrationBuilder.CreateIndex(
                name: "IX_Siniflars_KaydedenId",
                table: "Siniflars",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_Siniflars_SubeId",
                table: "Siniflars",
                column: "SubeId");

            migrationBuilder.CreateIndex(
                name: "IX_SoruBankasiLogs_KaydedenId",
                table: "SoruBankasiLogs",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_SoruBankasiLogs_SoruBankasiId",
                table: "SoruBankasiLogs",
                column: "SoruBankasiId");

            migrationBuilder.CreateIndex(
                name: "IX_SoruBankasis_KaydedenId",
                table: "SoruBankasis",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_SoruDerecelers_KaydedenId",
                table: "SoruDerecelers",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_SoruDereces_DereceId",
                table: "SoruDereces",
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
                name: "IX_SoruOnays_OnaylayanId",
                table: "SoruOnays",
                column: "OnaylayanId");

            migrationBuilder.CreateIndex(
                name: "IX_SoruOnays_SoruBankasiId",
                table: "SoruOnays",
                column: "SoruBankasiId");

            migrationBuilder.CreateIndex(
                name: "IX_SSSCevaps_KaydedenId",
                table: "SSSCevaps",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_SSSCevaps_SSSId",
                table: "SSSCevaps",
                column: "SSSId");

            migrationBuilder.CreateIndex(
                name: "IX_SSSs_KaydedenId",
                table: "SSSs",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_Subelers_KaydedenId",
                table: "Subelers",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_Subelers_OkulId",
                table: "Subelers",
                column: "OkulId");

            migrationBuilder.CreateIndex(
                name: "IX_Telefonlars_AdayGorevKaydiId",
                table: "Telefonlars",
                column: "AdayGorevKaydiId");

            migrationBuilder.CreateIndex(
                name: "IX_Telefonlars_KaydedenId",
                table: "Telefonlars",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_Telefonlars_OkullarOkulId",
                table: "Telefonlars",
                column: "OkullarOkulId");

            migrationBuilder.CreateIndex(
                name: "IX_Telefonlars_TemsilciliklerTemsilcilikId",
                table: "Telefonlars",
                column: "TemsilciliklerTemsilcilikId");

            migrationBuilder.CreateIndex(
                name: "IX_Temsilciliklers_KaydedenId",
                table: "Temsilciliklers",
                column: "KaydedenId");

            migrationBuilder.CreateIndex(
                name: "IX_Temsilciliklers_SehirId",
                table: "Temsilciliklers",
                column: "SehirId");

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
                name: "AdayDDKs");

            migrationBuilder.DropTable(
                name: "AdayNots");

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
                name: "DosyaGaleris");

            migrationBuilder.DropTable(
                name: "EPostaAdresleris");

            migrationBuilder.DropTable(
                name: "FotoGaleris");

            migrationBuilder.DropTable(
                name: "GorevKararPdfGaleris");

            migrationBuilder.DropTable(
                name: "IkametAdresleris");

            migrationBuilder.DropTable(
                name: "IllerMdEPostas");

            migrationBuilder.DropTable(
                name: "Komisyonlars");

            migrationBuilder.DropTable(
                name: "MulakatSorularis");

            migrationBuilder.DropTable(
                name: "Notlars");

            migrationBuilder.DropTable(
                name: "Ogrencilers");

            migrationBuilder.DropTable(
                name: "OkulBilgis");

            migrationBuilder.DropTable(
                name: "Personellers");

            migrationBuilder.DropTable(
                name: "SoruBankasiLogs");

            migrationBuilder.DropTable(
                name: "SoruDereces");

            migrationBuilder.DropTable(
                name: "SoruKategoris");

            migrationBuilder.DropTable(
                name: "SoruOnays");

            migrationBuilder.DropTable(
                name: "SSSCevaps");

            migrationBuilder.DropTable(
                name: "Telefonlars");

            migrationBuilder.DropTable(
                name: "UlkeGruplaris");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Adaylars");

            migrationBuilder.DropTable(
                name: "Duyurulars");

            migrationBuilder.DropTable(
                name: "Etkinliklers");

            migrationBuilder.DropTable(
                name: "OkulBinaBolums");

            migrationBuilder.DropTable(
                name: "Ilcelers");

            migrationBuilder.DropTable(
                name: "Siniflars");

            migrationBuilder.DropTable(
                name: "SoruKategorilers");

            migrationBuilder.DropTable(
                name: "SoruBankasis");

            migrationBuilder.DropTable(
                name: "SSSs");

            migrationBuilder.DropTable(
                name: "AdayGorevKaydis");

            migrationBuilder.DropTable(
                name: "Mulakatlars");

            migrationBuilder.DropTable(
                name: "Temsilciliklers");

            migrationBuilder.DropTable(
                name: "Illers");

            migrationBuilder.DropTable(
                name: "Subelers");

            migrationBuilder.DropTable(
                name: "Branslars");

            migrationBuilder.DropTable(
                name: "Universitelers");

            migrationBuilder.DropTable(
                name: "SoruDerecelers");

            migrationBuilder.DropTable(
                name: "Okullars");

            migrationBuilder.DropTable(
                name: "Sehirlers");

            migrationBuilder.DropTable(
                name: "Eyaletlers");

            migrationBuilder.DropTable(
                name: "Ulkelers");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Kitalars");
        }
    }
}
