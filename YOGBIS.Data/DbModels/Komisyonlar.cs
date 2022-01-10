using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Komisyonlar:Base
    {
        [Key]
        public int KomisyonId { get; set; }
        public string KomisyonAdi { get; set; }
        public int KomisyonUyeSiraId { get; set; }
        public string KomisyonGorevi { get; set; }
        public string KomisyonUyeGorevYeri { get; set; }
        public string KomisyonUyeAdiSoyadi { get; set; }
        public DateTime KomisyonGorevTarihi { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }

    }
}
