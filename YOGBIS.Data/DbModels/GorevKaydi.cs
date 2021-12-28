using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class GorevKaydi:Base
    {
        public int GorevId { get; set; }
        public int GorevliTC { get; set; }
        public string GörevAdi { get; set; }
        public DateTime? GorevBasTarihi { get; set; }
        public DateTime? GorevBitisTarihi { get; set; }
        public DateTime? GorevTarihi { get; set; }
        public string GorevOnaySayi { get; set; }
        public string GorevYeri { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
    }
}
