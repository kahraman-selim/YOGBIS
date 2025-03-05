using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Ogrenciler : Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid OgrencilerId { get; set; }
        public DateTime KayitTarihi { get; set; }
        public string OgrenciTuru { get; set; }
        public string Uyruk { get; set; }
        public bool Cinsiyet { get; set; }
        public DateTime? BaslamaKayitTarihi { get; set; }
        public string KayitNedeni { get; set; }
        public int KayitSayisi { get; set; }
        public DateTime? AyrilmaTarihi { get; set; }
        public string AyrilmaNedeni { get; set; }
        public int AyrilanSayisi { get; set; }

        public Guid? EgitimciId { get; set; }
        [ForeignKey("EgitimciId")]
        public virtual Personeller Egitimci { get; set; }

        public Guid? SinifId { get; set; }
        [ForeignKey("SinifId")]
        public virtual Siniflar Sinif { get; set; }

        public Guid? SubeId { get; set; }
        [ForeignKey("SubeId")]
        public virtual Subeler Sube { get; set; }

        public Guid? OkulId { get; set; }
        [ForeignKey("OkulId")]
        public virtual Okullar Okul { get; set; }

        public Guid? UniversiteId { get; set; }
        [ForeignKey("UniversiteId")]
        public virtual Universiteler Universite { get; set; }

        public Guid? SehirId { get; set; }
        [ForeignKey("SehirId")]
        public virtual Sehirler Sehir { get; set; }

        public Guid? EyaletId { get; set; }
        [ForeignKey("EyaletId")]
        public virtual Eyaletler Eyalet { get; set; }

        public Guid? TemsilcilikId { get; set; }
        [ForeignKey("TemsilcilikId")]
        public virtual Temsilcilikler Temsilcilik { get; set; }

        public Guid UlkeId { get; set; }
        [ForeignKey("UlkeId")]
        public virtual Ulkeler Ulke { get; set; }

        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public virtual Kullanici Kullanici { get; set; }
    }
}
