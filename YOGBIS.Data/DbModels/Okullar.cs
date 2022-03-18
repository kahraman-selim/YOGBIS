using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Okullar:Base
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
        ///////////////////////////////////////////////        
        public Guid? SehirId { get; set; }
        //[ForeignKey("SehirId")]
        //public Sehirler Sehirler { get; set; }
        public Guid? EyaletId { get; set; }
        //public Eyaletler Eyaletler { get; set; }
        public Guid? TemsilcilikId { get; set; }
        public Guid UlkeId { get; set; }
        //public Ulkeler Ulkeler { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
        public List<OkulBinaBolum> OkulBinaBolum { get; set; }
        public List<Siniflar> Siniflar { get; set; }
        public List<Subeler> Subeler { get; set; }
        public List<Ogrenciler> Ogrenciler { get; set; }
        public List<AdayGorevKaydi> AdayGorevKaydi { get; set; }
        public List<Etkinlikler> Etkinlikler { get; set; }
        public List<EPostaAdresleri> EpostaAdresleri { get; set; }
        public List<Telefonlar> Telefonlar { get; set; }
        public ICollection<FotoGaleri> FotoGaleri { get; set; }
    }
}
