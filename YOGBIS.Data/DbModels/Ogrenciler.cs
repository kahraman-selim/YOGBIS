using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Ogrenciler
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
        public Guid? SinifId { get; set; }
        public Guid? SubeId { get; set; }
        public Guid? OkulId { get; set; }
        public Guid? UniversiteId { get; set; }
        public Guid? SehirId { get; set; }
        public Guid? EyaletId { get; set; }
        public Guid? TemsilcilikId { get; set; }
        public Guid UlkeId { get; set; }
        [ForeignKey("UlkeId")]
        public Ulkeler Ulkeler { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
    }
}
