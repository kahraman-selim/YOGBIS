﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using YOGBIS.Data.DataContext;

namespace YOGBIS.Data.Migrations
{
    [DbContext(typeof(YOGBISContext))]
    partial class YOGBISContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(127)")
                        .HasMaxLength(127);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("varchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(127)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(767)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("varchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("varchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("varchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(767)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(127)")
                        .HasMaxLength(127);

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(127)")
                        .HasMaxLength(127);

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(767)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(127)")
                        .HasMaxLength(127);

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(127)")
                        .HasMaxLength(127);

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(127)")
                        .HasMaxLength(127);

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(127)")
                        .HasMaxLength(127);

                    b.Property<string>("Name")
                        .HasColumnType("varchar(127)")
                        .HasMaxLength(127);

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Microsoft.EntityFrameworkCore.AutoHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Changed")
                        .HasColumnType("text");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime");

                    b.Property<int>("Kind")
                        .HasColumnType("int");

                    b.Property<string>("RowId")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("TableName")
                        .IsRequired()
                        .HasColumnType("varchar(128)")
                        .HasMaxLength(128);

                    b.HasKey("Id");

                    b.ToTable("AutoHistory");
                });

            modelBuilder.Entity("YOGBIS.Data.DbModels.Dereceler", b =>
                {
                    b.Property<int>("DereceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("DereceAdi")
                        .HasColumnType("text");

                    b.Property<DateTime>("KayitTarihi")
                        .HasColumnType("datetime");

                    b.Property<string>("KullaniciId")
                        .HasColumnType("varchar(767)");

                    b.HasKey("DereceId");

                    b.HasIndex("KullaniciId");

                    b.ToTable("Derecelers");
                });

            modelBuilder.Entity("YOGBIS.Data.DbModels.Eyaletler", b =>
                {
                    b.Property<int>("EyaletId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("EyaletAciklama")
                        .HasColumnType("text");

                    b.Property<string>("EyaletAdi")
                        .HasColumnType("text");

                    b.Property<DateTime>("KayitTarihi")
                        .HasColumnType("datetime");

                    b.Property<string>("KullaniciId")
                        .HasColumnType("varchar(767)");

                    b.Property<int>("UlkeId")
                        .HasColumnType("int");

                    b.HasKey("EyaletId");

                    b.HasIndex("KullaniciId");

                    b.HasIndex("UlkeId");

                    b.ToTable("Eyaletlers");
                });

            modelBuilder.Entity("YOGBIS.Data.DbModels.Kitalar", b =>
                {
                    b.Property<int>("KitaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("KitaAciklama")
                        .HasColumnType("text");

                    b.Property<string>("KitaAdi")
                        .HasColumnType("text");

                    b.HasKey("KitaId");

                    b.ToTable("Kitalars");

                    b.HasData(
                        new
                        {
                            KitaId = 1,
                            KitaAciklama = "Afrika Kıtası",
                            KitaAdi = "Afrika"
                        },
                        new
                        {
                            KitaId = 2,
                            KitaAciklama = "Antartika Kıtası",
                            KitaAdi = "Antartika"
                        },
                        new
                        {
                            KitaId = 3,
                            KitaAciklama = "Asya Kıtası",
                            KitaAdi = "Asya"
                        },
                        new
                        {
                            KitaId = 4,
                            KitaAciklama = "Avrupa Kıtası",
                            KitaAdi = "Avrupa"
                        },
                        new
                        {
                            KitaId = 5,
                            KitaAciklama = "Avustralya Kıtası",
                            KitaAdi = "Avustralya"
                        },
                        new
                        {
                            KitaId = 6,
                            KitaAciklama = "Güney Amerika Kıtası",
                            KitaAdi = "Güney Amerika"
                        },
                        new
                        {
                            KitaId = 7,
                            KitaAciklama = "Kuzey Amerika Kıtası",
                            KitaAdi = "Kuzey Amerika"
                        });
                });

            modelBuilder.Entity("YOGBIS.Data.DbModels.MulakatSorulari", b =>
                {
                    b.Property<int>("MulakatSorulariId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Cevap")
                        .HasColumnType("text");

                    b.Property<int>("DereceId")
                        .HasColumnType("int");

                    b.Property<DateTime>("KayitTarihi")
                        .HasColumnType("datetime");

                    b.Property<string>("KullaniciId")
                        .HasColumnType("varchar(767)");

                    b.Property<int>("MulakatId")
                        .HasColumnType("int");

                    b.Property<string>("Soru")
                        .HasColumnType("text");

                    b.Property<int>("SoruId")
                        .HasColumnType("int");

                    b.Property<string>("SoruKategoriAdi")
                        .HasColumnType("text");

                    b.Property<int>("SoruKategoriId")
                        .HasColumnType("int");

                    b.Property<int>("SoruSiraNo")
                        .HasColumnType("int");

                    b.HasKey("MulakatSorulariId");

                    b.HasIndex("DereceId");

                    b.HasIndex("KullaniciId");

                    b.HasIndex("MulakatId");

                    b.HasIndex("SoruId");

                    b.HasIndex("SoruKategoriId");

                    b.ToTable("MulakatSorularis");
                });

            modelBuilder.Entity("YOGBIS.Data.DbModels.Mulakatlar", b =>
                {
                    b.Property<int>("MulakatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AdaySayisi")
                        .HasColumnType("int");

                    b.Property<DateTime>("BaslamaTarihi")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("BitisTarihi")
                        .HasColumnType("datetime");

                    b.Property<string>("Derecesi")
                        .HasColumnType("text");

                    b.Property<bool?>("Durumu")
                        .HasColumnType("bit");

                    b.Property<DateTime>("KayitTarihi")
                        .HasColumnType("datetime");

                    b.Property<string>("KullaniciId")
                        .HasColumnType("varchar(767)");

                    b.Property<string>("MulakatAciklama")
                        .HasColumnType("text");

                    b.Property<string>("MulakatAdi")
                        .HasColumnType("text");

                    b.Property<string>("OnaySayisi")
                        .HasColumnType("text");

                    b.Property<int>("SorulanSoruSayisi")
                        .HasColumnType("int");

                    b.HasKey("MulakatId");

                    b.HasIndex("KullaniciId");

                    b.ToTable("Mulakatlars");
                });

            modelBuilder.Entity("YOGBIS.Data.DbModels.Ogrenciler", b =>
                {
                    b.Property<int>("OgrencilerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Ay")
                        .HasColumnType("text");

                    b.Property<int>("DEOg")
                        .HasColumnType("int");

                    b.Property<int>("DKOg")
                        .HasColumnType("int");

                    b.Property<DateTime>("KayitTarihi")
                        .HasColumnType("datetime");

                    b.Property<string>("KullaniciId")
                        .HasColumnType("varchar(767)");

                    b.Property<int>("OkulId")
                        .HasColumnType("int");

                    b.Property<int>("TCEOg")
                        .HasColumnType("int");

                    b.Property<int>("TCKOg")
                        .HasColumnType("int");

                    b.Property<int>("UlkeId")
                        .HasColumnType("int");

                    b.Property<string>("Yil")
                        .HasColumnType("text");

                    b.HasKey("OgrencilerId");

                    b.HasIndex("KullaniciId");

                    b.HasIndex("OkulId");

                    b.HasIndex("UlkeId");

                    b.ToTable("Ogrencilers");
                });

            modelBuilder.Entity("YOGBIS.Data.DbModels.OkulBilgi", b =>
                {
                    b.Property<int>("OkulBilgiId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("KayitTarihi")
                        .HasColumnType("datetime");

                    b.Property<string>("KullaniciId")
                        .HasColumnType("varchar(767)");

                    b.Property<string>("MdYrdAdiSoyadi")
                        .HasColumnType("text");

                    b.Property<string>("MdYrdDonusYil")
                        .HasColumnType("text");

                    b.Property<string>("MdYrdEPosta")
                        .HasColumnType("text");

                    b.Property<string>("MdYrdTelefon")
                        .HasColumnType("text");

                    b.Property<string>("MudurAdiSoyadi")
                        .HasColumnType("text");

                    b.Property<string>("MudurDonusYil")
                        .HasColumnType("text");

                    b.Property<string>("MudurEPosta")
                        .HasColumnType("text");

                    b.Property<string>("MudurTelefon")
                        .HasColumnType("text");

                    b.Property<string>("OkulAdres")
                        .HasColumnType("text");

                    b.Property<int>("OkulId")
                        .HasColumnType("int");

                    b.Property<string>("OkulTelefon")
                        .HasColumnType("text");

                    b.Property<int>("UlkeId")
                        .HasColumnType("int");

                    b.HasKey("OkulBilgiId");

                    b.HasIndex("KullaniciId");

                    b.HasIndex("OkulId");

                    b.HasIndex("UlkeId");

                    b.ToTable("OkulBilgis");
                });

            modelBuilder.Entity("YOGBIS.Data.DbModels.Okullar", b =>
                {
                    b.Property<int>("OkulId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("KayitTarihi")
                        .HasColumnType("datetime");

                    b.Property<string>("KullaniciId")
                        .HasColumnType("varchar(767)");

                    b.Property<string>("OkulAdi")
                        .HasColumnType("text");

                    b.Property<int>("OkulKodu")
                        .HasColumnType("int");

                    b.Property<int>("UlkeId")
                        .HasColumnType("int");

                    b.HasKey("OkulId");

                    b.HasIndex("KullaniciId");

                    b.HasIndex("UlkeId");

                    b.ToTable("Okullars");
                });

            modelBuilder.Entity("YOGBIS.Data.DbModels.Sehirler", b =>
                {
                    b.Property<int>("SehirId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool?>("Baskent")
                        .HasColumnType("bit");

                    b.Property<int>("EyaletId")
                        .HasColumnType("int");

                    b.Property<DateTime>("KayitTarihi")
                        .HasColumnType("datetime");

                    b.Property<string>("KullaniciId")
                        .HasColumnType("varchar(767)");

                    b.Property<string>("SehirAciklama")
                        .HasColumnType("text");

                    b.Property<string>("SehirAdi")
                        .HasColumnType("text");

                    b.HasKey("SehirId");

                    b.HasIndex("EyaletId");

                    b.HasIndex("KullaniciId");

                    b.ToTable("Sehirlers");
                });

            modelBuilder.Entity("YOGBIS.Data.DbModels.SoruBankasi", b =>
                {
                    b.Property<int>("SoruBankasiId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Cevap")
                        .HasColumnType("text");

                    b.Property<int>("DereceId")
                        .HasColumnType("int");

                    b.Property<string>("KaydedenId")
                        .HasColumnType("varchar(767)");

                    b.Property<DateTime>("KayitTarihi")
                        .HasColumnType("datetime");

                    b.Property<string>("OnayAciklama")
                        .HasColumnType("text");

                    b.Property<int?>("OnayDurumu")
                        .HasColumnType("int");

                    b.Property<string>("OnaylayanId")
                        .HasColumnType("varchar(767)");

                    b.Property<string>("Soru")
                        .HasColumnType("text");

                    b.Property<bool>("SoruDurumu")
                        .HasColumnType("bit");

                    b.Property<int>("SoruKategorilerId")
                        .HasColumnType("int");

                    b.Property<int>("SorulmaSayisi")
                        .HasColumnType("int");

                    b.HasKey("SoruBankasiId");

                    b.HasIndex("DereceId");

                    b.HasIndex("KaydedenId");

                    b.HasIndex("OnaylayanId");

                    b.HasIndex("SoruKategorilerId");

                    b.ToTable("SoruBankasis");
                });

            modelBuilder.Entity("YOGBIS.Data.DbModels.SoruKategori", b =>
                {
                    b.Property<int>("SoruKategoriId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("KategoriId")
                        .HasColumnType("int");

                    b.Property<int?>("SoruBankasiId")
                        .HasColumnType("int");

                    b.Property<int>("SoruId")
                        .HasColumnType("int");

                    b.Property<int?>("SoruKategorilerId")
                        .HasColumnType("int");

                    b.HasKey("SoruKategoriId");

                    b.HasIndex("SoruBankasiId");

                    b.HasIndex("SoruKategorilerId");

                    b.ToTable("SoruKategoris");
                });

            modelBuilder.Entity("YOGBIS.Data.DbModels.SoruKategoriler", b =>
                {
                    b.Property<int>("SoruKategorilerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("DereceId")
                        .HasColumnType("int");

                    b.Property<DateTime>("KayitTarihi")
                        .HasColumnType("datetime");

                    b.Property<string>("KullaniciId")
                        .HasColumnType("varchar(767)");

                    b.Property<string>("SoruKategorilerAdi")
                        .HasColumnType("text");

                    b.Property<string>("SoruKategorilerKullanimi")
                        .HasColumnType("text");

                    b.Property<int>("SoruKategorilerPuan")
                        .HasColumnType("int");

                    b.HasKey("SoruKategorilerId");

                    b.HasIndex("DereceId");

                    b.HasIndex("KullaniciId");

                    b.ToTable("SoruKategorilers");
                });

            modelBuilder.Entity("YOGBIS.Data.DbModels.UlkeGruplari", b =>
                {
                    b.Property<int>("UlkeGrupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("KayitTarihi")
                        .HasColumnType("datetime");

                    b.Property<string>("KullaniciId")
                        .HasColumnType("varchar(767)");

                    b.Property<string>("UlkeGrupAciklama")
                        .HasColumnType("text");

                    b.Property<string>("UlkeGrupAdi")
                        .HasColumnType("text");

                    b.HasKey("UlkeGrupId");

                    b.HasIndex("KullaniciId");

                    b.ToTable("UlkeGruplaris");
                });

            modelBuilder.Entity("YOGBIS.Data.DbModels.UlkeGruplariKitalar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("KitaId")
                        .HasColumnType("int");

                    b.Property<int?>("KitalarKitaId")
                        .HasColumnType("int");

                    b.Property<int>("UlkeGrupId")
                        .HasColumnType("int");

                    b.Property<int?>("UlkeGruplariUlkeGrupId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("KitalarKitaId");

                    b.HasIndex("UlkeGruplariUlkeGrupId");

                    b.ToTable("UlkeGruplariKitalars");
                });

            modelBuilder.Entity("YOGBIS.Data.DbModels.Ulkeler", b =>
                {
                    b.Property<int>("UlkeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("KayitTarihi")
                        .HasColumnType("datetime");

                    b.Property<int>("KitaId")
                        .HasColumnType("int");

                    b.Property<string>("KullaniciId")
                        .HasColumnType("varchar(767)");

                    b.Property<string>("UlkeAciklama")
                        .HasColumnType("text");

                    b.Property<string>("UlkeAdi")
                        .HasColumnType("text");

                    b.Property<string>("UlkeBayrak")
                        .HasColumnType("text");

                    b.HasKey("UlkeId");

                    b.HasIndex("KitaId");

                    b.HasIndex("KullaniciId");

                    b.ToTable("Ulkelers");
                });

            modelBuilder.Entity("YOGBIS.Data.DbModels.Kullanici", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("Ad")
                        .HasColumnType("text");

                    b.Property<bool?>("Aktif")
                        .HasColumnType("bit");

                    b.Property<DateTime>("KayitTarihi")
                        .HasColumnType("datetime");

                    b.Property<int>("KulaniciAdDegLimiti")
                        .HasColumnType("int");

                    b.Property<byte[]>("KullaniciResim")
                        .HasColumnType("varbinary(4000)");

                    b.Property<string>("Soyad")
                        .HasColumnType("text");

                    b.Property<string>("TcKimlikNo")
                        .HasColumnType("text");

                    b.HasDiscriminator().HasValue("Kullanici");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("YOGBIS.Data.DbModels.Dereceler", b =>
                {
                    b.HasOne("YOGBIS.Data.DbModels.Kullanici", "Kullanici")
                        .WithMany()
                        .HasForeignKey("KullaniciId");
                });

            modelBuilder.Entity("YOGBIS.Data.DbModels.Eyaletler", b =>
                {
                    b.HasOne("YOGBIS.Data.DbModels.Kullanici", "Kullanici")
                        .WithMany()
                        .HasForeignKey("KullaniciId");

                    b.HasOne("YOGBIS.Data.DbModels.Ulkeler", "Ulkeler")
                        .WithMany()
                        .HasForeignKey("UlkeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("YOGBIS.Data.DbModels.MulakatSorulari", b =>
                {
                    b.HasOne("YOGBIS.Data.DbModels.Dereceler", "Dereceler")
                        .WithMany()
                        .HasForeignKey("DereceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("YOGBIS.Data.DbModels.Kullanici", "Kullanici")
                        .WithMany()
                        .HasForeignKey("KullaniciId");

                    b.HasOne("YOGBIS.Data.DbModels.Mulakatlar", "Mulakatlar")
                        .WithMany()
                        .HasForeignKey("MulakatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("YOGBIS.Data.DbModels.SoruBankasi", "SoruBankasi")
                        .WithMany()
                        .HasForeignKey("SoruId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("YOGBIS.Data.DbModels.SoruKategoriler", "SoruKategoriler")
                        .WithMany()
                        .HasForeignKey("SoruKategoriId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("YOGBIS.Data.DbModels.Mulakatlar", b =>
                {
                    b.HasOne("YOGBIS.Data.DbModels.Kullanici", "Kullanici")
                        .WithMany()
                        .HasForeignKey("KullaniciId");
                });

            modelBuilder.Entity("YOGBIS.Data.DbModels.Ogrenciler", b =>
                {
                    b.HasOne("YOGBIS.Data.DbModels.Kullanici", "Kullanici")
                        .WithMany()
                        .HasForeignKey("KullaniciId");

                    b.HasOne("YOGBIS.Data.DbModels.Okullar", "Okullar")
                        .WithMany()
                        .HasForeignKey("OkulId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("YOGBIS.Data.DbModels.Ulkeler", "Ulkeler")
                        .WithMany()
                        .HasForeignKey("UlkeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("YOGBIS.Data.DbModels.OkulBilgi", b =>
                {
                    b.HasOne("YOGBIS.Data.DbModels.Kullanici", "Kullanici")
                        .WithMany()
                        .HasForeignKey("KullaniciId");

                    b.HasOne("YOGBIS.Data.DbModels.Okullar", "Okullar")
                        .WithMany()
                        .HasForeignKey("OkulId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("YOGBIS.Data.DbModels.Ulkeler", "Ulkeler")
                        .WithMany()
                        .HasForeignKey("UlkeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("YOGBIS.Data.DbModels.Okullar", b =>
                {
                    b.HasOne("YOGBIS.Data.DbModels.Kullanici", "Kullanici")
                        .WithMany()
                        .HasForeignKey("KullaniciId");

                    b.HasOne("YOGBIS.Data.DbModels.Ulkeler", "Ulkeler")
                        .WithMany()
                        .HasForeignKey("UlkeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("YOGBIS.Data.DbModels.Sehirler", b =>
                {
                    b.HasOne("YOGBIS.Data.DbModels.Eyaletler", "Eyaletler")
                        .WithMany()
                        .HasForeignKey("EyaletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("YOGBIS.Data.DbModels.Kullanici", "Kullanici")
                        .WithMany()
                        .HasForeignKey("KullaniciId");
                });

            modelBuilder.Entity("YOGBIS.Data.DbModels.SoruBankasi", b =>
                {
                    b.HasOne("YOGBIS.Data.DbModels.Dereceler", "Dereceler")
                        .WithMany()
                        .HasForeignKey("DereceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("YOGBIS.Data.DbModels.Kullanici", "Kaydeden")
                        .WithMany()
                        .HasForeignKey("KaydedenId");

                    b.HasOne("YOGBIS.Data.DbModels.Kullanici", "Onaylayan")
                        .WithMany()
                        .HasForeignKey("OnaylayanId");

                    b.HasOne("YOGBIS.Data.DbModels.SoruKategoriler", "SoruKategoriler")
                        .WithMany()
                        .HasForeignKey("SoruKategorilerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("YOGBIS.Data.DbModels.SoruKategori", b =>
                {
                    b.HasOne("YOGBIS.Data.DbModels.SoruBankasi", "SoruBankasi")
                        .WithMany("SoruKategoris")
                        .HasForeignKey("SoruBankasiId");

                    b.HasOne("YOGBIS.Data.DbModels.SoruKategoriler", "SoruKategoriler")
                        .WithMany("SoruKategoris")
                        .HasForeignKey("SoruKategorilerId");
                });

            modelBuilder.Entity("YOGBIS.Data.DbModels.SoruKategoriler", b =>
                {
                    b.HasOne("YOGBIS.Data.DbModels.Dereceler", "Dereceler")
                        .WithMany()
                        .HasForeignKey("DereceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("YOGBIS.Data.DbModels.Kullanici", "Kullanici")
                        .WithMany()
                        .HasForeignKey("KullaniciId");
                });

            modelBuilder.Entity("YOGBIS.Data.DbModels.UlkeGruplari", b =>
                {
                    b.HasOne("YOGBIS.Data.DbModels.Kullanici", "Kullanici")
                        .WithMany()
                        .HasForeignKey("KullaniciId");
                });

            modelBuilder.Entity("YOGBIS.Data.DbModels.UlkeGruplariKitalar", b =>
                {
                    b.HasOne("YOGBIS.Data.DbModels.Kitalar", "Kitalar")
                        .WithMany("UlkeGruplariKitalars")
                        .HasForeignKey("KitalarKitaId");

                    b.HasOne("YOGBIS.Data.DbModels.UlkeGruplari", "UlkeGruplari")
                        .WithMany("UlkeGruplariKitalars")
                        .HasForeignKey("UlkeGruplariUlkeGrupId");
                });

            modelBuilder.Entity("YOGBIS.Data.DbModels.Ulkeler", b =>
                {
                    b.HasOne("YOGBIS.Data.DbModels.Kitalar", "Kitalar")
                        .WithMany()
                        .HasForeignKey("KitaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("YOGBIS.Data.DbModels.Kullanici", "Kullanici")
                        .WithMany()
                        .HasForeignKey("KullaniciId");
                });
#pragma warning restore 612, 618
        }
    }
}
