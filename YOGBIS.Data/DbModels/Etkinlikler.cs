using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Etkinlikler:Base
    {
        [Key]
        public int EtkinlikId { get; set; }
        public string EtkinlikAdi { get; set; }
        public int OkulId { get; set; }
        [ForeignKey("OkulId")]
        public Okullar Okullar { get; set; }
        public int TemsilcilikId { get; set; }
        [ForeignKey("TemsilcilikId")]
        public Temsilcilikler Temsilcilikler { get; set; }
        public DateTime BasTarihi { get; set; }
        public DateTime BitTarihi { get; set; }
        public string EtkinlikBilgi { get; set; }
        public int KatilimciSayisi { get; set; }
        public string DuzenleyenAdiSoyadi { get; set; }
        public string EtkinlikKapakResimUrl { get; set; }           
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
        public ICollection<DosyaGaleri> DosyaGaleri { get; set; }
        public ICollection<FotoGaleri> FotoGaleri { get; set; }

    }
}
