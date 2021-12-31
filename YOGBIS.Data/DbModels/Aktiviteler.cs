using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Aktiviteler:Base
    {
        [Key]
        public int AktiviteId { get; set; }
        public string AktiviteAdi { get; set; }
        public int OkulId { get; set; }
        [ForeignKey("OkulId")]
        public Okullar Okullar { get; set; }
        public DateTime BasTarihi { get; set; }
        public DateTime BitTarihi { get; set; }
        public string AktiviteBilgi { get; set; }
        public int KatilimciSayisi { get; set; }
        public string DuzenleyenAdiSoyadi { get; set; }
        public string Resim1Yol { get; set; }
        public string Resim2Yol { get; set; }
        public string Resim3Yol { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
        public ICollection<FotoGaleri> FotoGaleri { get; set; }

    }
}
