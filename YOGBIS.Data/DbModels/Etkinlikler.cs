using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Etkinlikler : Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid EtkinlikId { get; set; }
        public string EtkinlikAdi { get; set; }
        public DateTime BasTarihi { get; set; }
        public DateTime BitTarihi { get; set; }
        public string EtkinlikBilgi { get; set; }
        public string HedefKitle { get; set; }
        public int KatilimciSayisi { get; set; }
        public string Sonuc { get; set; }
        public string DuzenleyenAdiSoyadi { get; set; }
        public string EtkinlikKapakResimUrl { get; set; }

        public Guid? OkulId { get; set; }
        [ForeignKey("OkulId")]
        public virtual Okullar Okul { get; set; }

        public Guid? TemsilcilikId { get; set; }
        [ForeignKey("TemsilcilikId")]
        public virtual Temsilcilikler Temsilcilik { get; set; }

        public Guid? UlkeId { get; set; }
        [ForeignKey("UlkeId")]
        public virtual Ulkeler Ulke { get; set; }

        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public virtual Kullanici Kullanici { get; set; }

        public virtual ICollection<DosyaGaleri> DosyaGaleri { get; set; }
        public virtual ICollection<FotoGaleri> FotoGaleri { get; set; }
    }
}
