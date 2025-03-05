using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Okullar : Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid OkulId { get; set; }
        public int OkulKodu { get; set; }
        public string OkulAdi { get; set; }
        public string OkulTuru { get; set; }
        public string OgretimTuru { get; set; }
        public string OkulLogoURL { get; set; }
        public string OkulBilgi { get; set; }
        public DateTime? OkulAcilisTarihi { get; set; }
        public bool OkulDurumu { get; set; }
        public string OkulMudurId { get; set; }
        public string OkulHizmetGecisDonem { get; set; }
        public string OkulKapaliAlan { get; set; }
        public string OkulAcikAlan { get; set; }
        public bool? OkulMulkiDurum { get; set; }
        public string OkulMulkiDurumAciklama { get; set; }
        public string OkulInternetAdresi { get; set; }
        public string OkulEPostaAdresi { get; set; }
        public string OkulTelefon { get; set; }

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

        public virtual ICollection<OkulBinaBolum> OkulBinaBolum { get; set; }
        public virtual ICollection<Siniflar> Siniflar { get; set; }
        public virtual ICollection<Subeler> Subeler { get; set; }
        public virtual ICollection<Ogrenciler> Ogrenciler { get; set; }
        public virtual ICollection<AdayGorevKaydi> AdayGorevKaydi { get; set; }
        public virtual ICollection<Etkinlikler> Etkinlikler { get; set; }
        public virtual ICollection<EPostaAdresleri> EpostaAdresleri { get; set; }
        public virtual ICollection<Telefonlar> Telefonlar { get; set; }
        public virtual ICollection<FotoGaleri> FotoGaleri { get; set; }
    }
}
