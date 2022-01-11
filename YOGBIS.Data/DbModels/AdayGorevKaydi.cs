using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class AdayGorevKaydi:Base
    {
        [Key]
        public int AdayGorevKaydiId { get; set; }
        public int GorevliTC { get; set; }
        public int DereceId { get; set; }
        [ForeignKey("DereceId")]
        public SoruDereceler SoruDereceler { get; set; }
        public string Gorevi { get; set; }
        public int BransId { get; set; }
        [ForeignKey("BransId")]
        public Branslar Branslar { get; set; }        
        public string GorevOnaySayi { get; set; }
        public DateTime GorevlOnayTarihi { get; set; }
        public string KararPdfUrl { get; set; }
        public DateTime? GorevlendirilmeTarihi { get; set; }
        public DateTime? GorevBasTarihi { get; set; }
        public DateTime? GorevBitisTarihi { get; set; }
        public int GorevDurumu { get; set; }
        public string GorevAciklama { get; set; }        
        public int? OkulId { get; set; }
        public int? UniId { get; set; }
        public int? SehirId { get; set; }        
        public int? EyaletId { get; set; }
        public int UlkeId { get; set; }
        [ForeignKey("UlkeId")]
        public Ulkeler Ulkeler { get; set; }
        public int KitaId { get; set; }
        [ForeignKey("KitaId")]
        public Kitalar Kitalar { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
        public ICollection<GorevKararPdfGaleri> GorevKararPdfGaleri { get; set; }
    }
}
