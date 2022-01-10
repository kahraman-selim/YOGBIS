using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class GorevKaydi:Base
    {
        [Key]
        public int GorevId { get; set; }
        public int GorevliTC { get; set; }
        public string GörevAdi { get; set; }
        public string GorevTuru { get; set; }
        public string GorevUnvanı { get; set; }
        public string Gorevi { get; set; }
        public DateTime? GorevBasTarihi { get; set; }
        public DateTime? GorevBitisTarihi { get; set; }
        public DateTime? GorevlendirilmeTarihi { get; set; }
        public string GorevOnaySayi { get; set; }
        public DateTime GorevlOnayTarihi { get; set; }
        public int? OkulId { get; set; }
        public int? UniId { get; set; }
        public int? SehirId { get; set; }        
        public int? EyaletId { get; set; }
        public int UlkeId { get; set; }
        public int KitaId { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
    }
}
