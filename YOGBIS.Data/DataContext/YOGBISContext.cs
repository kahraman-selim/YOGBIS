using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Data.DataContext
{
    public class YOGBISContext : IdentityDbContext
    {
        public YOGBISContext(DbContextOptions options)
            :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            base.OnModelCreating(builder.EnableAutoHistory(null));            
            
            #region MySqlSet
            builder.Entity<IdentityRole>(entity => entity.Property(m => m.Id).HasMaxLength(127));
            builder.Entity<IdentityRole>(entity => entity.Property(m => m.ConcurrencyStamp).HasColumnType("varchar(256)"));

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.Property(m => m.LoginProvider).HasMaxLength(127);
                entity.Property(m => m.ProviderKey).HasMaxLength(127);
            });

            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.Property(m => m.UserId).HasMaxLength(127);
                entity.Property(m => m.RoleId).HasMaxLength(127);
            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.Property(m => m.UserId).HasMaxLength(127);
                entity.Property(m => m.LoginProvider).HasMaxLength(127);
                entity.Property(m => m.Name).HasMaxLength(127);
            });
            #endregion

            //#region Kıtalar
            //builder.Entity<Kitalar>().HasData(
            //new Kitalar()
            //{ KitaId = Guid.NewGuid(), KitaAdi = "Afrika", KitaAciklama = "Afrika Kıtası"},
            //new Kitalar()
            //{ KitaId = Guid.NewGuid(), KitaAdi = "Asya", KitaAciklama = "Asya Kıtası"},
            //new Kitalar()
            //{ KitaId = Guid.NewGuid(), KitaAdi = "Avrupa", KitaAciklama = "Avrupa Kıtası"},
            //new Kitalar()
            //{ KitaId = Guid.NewGuid(), KitaAdi = "Avustralya", KitaAciklama = "Avustralya Kıtası"},
            //new Kitalar()
            //{ KitaId = Guid.NewGuid(), KitaAdi = "Güney Amerika", KitaAciklama = "Güney Amerika Kıtası"},
            //new Kitalar()
            //{ KitaId = Guid.NewGuid(), KitaAdi = "Kuzey Amerika", KitaAciklama = "Kuzey Amerika Kıtası"}
            //);
            //#endregion

            #region FP
            builder.Entity<SoruKategori>()
                .HasKey(o => new { o.SoruId, o.KategoriId });

            builder.Entity<SoruDerece>()
                .HasKey(o => new { o.SoruId, o.DereceId });

            builder.Entity<SoruDerece>()
                .HasOne<SoruBankasi>(s => s.SoruBankasi)
                .WithMany(g => g.SoruDereces)
                .HasForeignKey(f => f.SoruId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<SoruDerece>()
                .HasOne<SoruDereceler>(s => s.SoruDereceler)
                .WithMany(g => g.SoruDereces)
                .HasForeignKey(f => f.DereceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<SoruKategori>()
                .HasOne<SoruBankasi>(s => s.SoruBankasi)
                .WithMany(g => g.SoruKategoris)
                .HasForeignKey(f => f.SoruId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<SoruKategori>()
                .HasOne<SoruKategoriler>(s => s.SoruKategoriler)
                .WithMany(g => g.SoruKategoris)
                .HasForeignKey(f => f.KategoriId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }

        #region DbSets
        public DbSet<AdayDDK> AdayDDKs { get; set; }
        public DbSet<AdayGorevKaydi> AdayGorevKaydis { get; set; }
        public DbSet<Adaylar> Adaylars { get; set; }
        public DbSet<AdayNot> AdayNots { get; set; }
        public DbSet<Branslar> Branslars { get; set; }
        public DbSet<DosyaGaleri> DosyaGaleris { get; set; }
        public DbSet<Duyurular> Duyurulars { get; set; }
        public DbSet<EPostaAdresleri> EPostaAdresleris { get; set; }
        public DbSet<Etkinlikler> Etkinliklers { get; set; }
        public DbSet<Eyaletler> Eyaletlers { get; set; }
        public DbSet<FotoGaleri> FotoGaleris { get; set; }
        public DbSet<GorevKararPdfGaleri> GorevKararPdfGaleris { get; set; }
        public DbSet<IkametAdresleri> IkametAdresleris { get; set; }
        public DbSet<Ilceler> Ilcelers { get; set; }
        public DbSet<Iller> Illers { get; set; }
        public DbSet<IllerMdEPosta> IllerMdEPostas { get; set; }
        public DbSet<Kitalar> Kitalars { get; set; }
        public DbSet<Komisyonlar> Komisyonlars { get; set; }
        public DbSet<Kullanici> Kullanicis { get; set; }
        public DbSet<Mulakatlar> Mulakatlars { get; set; }
        public DbSet<MulakatSorulari> MulakatSorularis { get; set; }
        public DbSet<Notlar> Notlars { get; set; }
        public DbSet<Ogrenciler> Ogrencilers { get; set; }
        public DbSet<OkulBilgi> OkulBilgis { get; set; }
        public DbSet<OkulBinaBolum> OkulBinaBolums { get; set; }
        public DbSet<Okullar> Okullars { get; set; }
        public DbSet<Personeller> Personellers { get; set; }
        public DbSet<Sehirler> Sehirlers { get; set; }
        public DbSet<Siniflar> Siniflars { get; set; }
        public DbSet<SoruBankasi> SoruBankasis { get; set; }
        public DbSet<SoruBankasiLog> SoruBankasiLogs { get; set; }
        public DbSet<SoruDerece> SoruDereces { get; set; }
        public DbSet<SoruDereceler> SoruDerecelers { get; set; }        
        public DbSet<SoruKategori> SoruKategoris { get; set; }
        public DbSet<SoruKategoriler> SoruKategorilers { get; set; }
        public DbSet<SoruOnay> SoruOnays { get; set; }
        public DbSet<SSS> SSSs { get; set; }
        public DbSet<SSSCevap> SSSCevaps { get; set; }
        public DbSet<Subeler> Subelers { get; set; }
        public DbSet<Temsilcilikler> Temsilciliklers { get; set; }
        public DbSet<UlkeGruplari> UlkeGruplaris { get; set; }        
        public DbSet<Ulkeler> Ulkelers { get; set; }
        public DbSet<Universiteler> Universitelers { get; set; }

        #endregion
    }
}
