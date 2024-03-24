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

            /*#region Kıtalar
                builder.Entity<Kitalar>().HasData(
                new Kitalar()
                { KitaId = Guid.NewGuid(), KitaAdi = "Afrika", KitaAciklama = "Afrika Kıtası"},
                new Kitalar()
                { KitaId = Guid.NewGuid(), KitaAdi = "Asya", KitaAciklama = "Asya Kıtası"},
                new Kitalar()
                { KitaId = Guid.NewGuid(), KitaAdi = "Avrupa", KitaAciklama = "Avrupa Kıtası"},
                new Kitalar()
                { KitaId = Guid.NewGuid(), KitaAdi = "Avustralya", KitaAciklama = "Avustralya Kıtası"},
                new Kitalar()
                { KitaId = Guid.NewGuid(), KitaAdi = "Güney Amerika", KitaAciklama = "Güney Amerika Kıtası"},
                new Kitalar()
                { KitaId = Guid.NewGuid(), KitaAdi = "Kuzey Amerika", KitaAciklama = "Kuzey Amerika Kıtası"}
                );
            #endregion*/

            #region FP
            builder.Entity<SoruKategori>()
                .HasKey(o => new { o.SoruId, o.KategoriId });

            builder.Entity<SoruDerece>()
                .HasKey(o => new { o.SoruId, o.DereceId });

            builder.Entity<SoruDerece>()
                .HasOne<SoruBankasi>(s => s.SoruBankasi)
                .WithMany(g => g.SoruDerece)
                .HasForeignKey(f => f.SoruId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<SoruDerece>()
                .HasOne<SoruDereceler>(s => s.SoruDereceler)
                .WithMany(g => g.SoruDerece)
                .HasForeignKey(f => f.DereceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<SoruKategori>()
                .HasOne<SoruBankasi>(s => s.SoruBankasi)
                .WithMany(g => g.SoruKategori)
                .HasForeignKey(f => f.SoruId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<SoruKategori>()
                .HasOne<SoruKategoriler>(s => s.SoruKategoriler)
                .WithMany(g => g.SoruKategori)
                .HasForeignKey(f => f.KategoriId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }

        #region DbSets
        public DbSet<AdayDDK> AdayDDK { get; set; }
        public DbSet<AdayGorevKaydi> AdayGorevKaydi { get; set; }
        public DbSet<Adaylar> Adaylar { get; set; }
        public DbSet<AdayNot> AdayNot { get; set; }
        public DbSet<Branslar> Branslar { get; set; }
        public DbSet<DosyaGaleri> DosyaGaleri { get; set; }
        public DbSet<Duyurular> Duyurular { get; set; }
        public DbSet<EPostaAdresleri> EPostaAdresleri { get; set; }
        public DbSet<Etkinlikler> Etkinlikler { get; set; }
        public DbSet<Eyaletler> Eyaletler { get; set; }
        public DbSet<FotoGaleri> FotoGaleri { get; set; }
        public DbSet<GorevKararPdfGaleri> GorevKararPdfGaleri { get; set; }
        public DbSet<IkametAdresleri> IkametAdresleri { get; set; }
        public DbSet<Ilceler> Ilceler { get; set; }
        public DbSet<Iller> Iller { get; set; }
        public DbSet<IllerMdEPosta> IllerMdEPosta { get; set; }
        public DbSet<Kitalar> Kitalar { get; set; }
        public DbSet<Komisyonlar> Komisyonlar { get; set; }
        public DbSet<Kullanici> Kullanici { get; set; }
        public DbSet<Mulakatlar> Mulakatlar { get; set; }
        public DbSet<MulakatSorulari> MulakatSorulari { get; set; }
        public DbSet<Notlar> Notlar { get; set; }
        public DbSet<Ogrenciler> Ogrenciler { get; set; }
        public DbSet<OkulBilgi> OkulBilgi { get; set; }
        public DbSet<OkulBinaBolum> OkulBinaBolum { get; set; }
        public DbSet<Okullar> Okullar { get; set; }
        public DbSet<Personeller> Personeller { get; set; }
        public DbSet<Sehirler> Sehirler { get; set; }
        public DbSet<Siniflar> Siniflar { get; set; }
        public DbSet<SoruBankasi> SoruBankasi { get; set; }
        public DbSet<SoruBankasiLog> SoruBankasiLog { get; set; }
        public DbSet<SoruDerece> SoruDerece { get; set; }
        public DbSet<SoruDereceler> SoruDereceler { get; set; }        
        public DbSet<SoruKategori> SoruKategori { get; set; }
        public DbSet<SoruKategoriler> SoruKategoriler { get; set; }
        public DbSet<SoruOnay> SoruOnay { get; set; }
        public DbSet<SSS> SSS { get; set; }
        public DbSet<SSSCevap> SSSCevap { get; set; }
        public DbSet<Subeler> Subeler { get; set; }
        public DbSet<Telefonlar> Telefonlar { get; set; }
        public DbSet<Temsilcilikler> Temsilcilikler { get; set; }
        public DbSet<UlkeGruplari> UlkeGruplari { get; set; }        
        public DbSet<Ulkeler> Ulkeler { get; set; }
        public DbSet<Universiteler> Universiteler { get; set; }

        #endregion
    }
}
