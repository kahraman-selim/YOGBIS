using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Okullar:Base
    {
        [Key]
        public int OkulId { get; set; }
        public int OkulKodu { get; set; }
        public string OkulAdi { get; set; }
        public string OkulLogoURL { get; set; } 
        public string OkulBilgi { get; set; }
        public DateTime? OkulAcilisTarihi { get; set; }
        public bool? OkulDurumu { get; set; }
        public string OkulMudurId { get; set; }
        public string HizmetGecisDonem { get; set; }
        public string KapaliAlan { get; set; }
        public string AcikAlan { get; set; }
        public bool MulkiDurum { get; set; }
        public string InternetAdresi { get; set; }
        public string EPostaAdresi { get; set; }
        public string OkulTelefon { get; set; }

        ///////////////////////////////////////////////        
        public int EyaletId { get; set; }
        [ForeignKey("EyaletId")]
        public Eyaletler Eyaletler { get; set; }
        public int SehirId { get; set; }
        [ForeignKey("SehirId")]
        public Sehirler Sehirler { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
        public List<OkulBinaBolum> OkulBinaBolum { get; set; }
        public List<Subeler> Subelers { get; set; }
        public List<AdayGorevKaydi> AdayGorevKaydis { get; set; }
        public List<Etkinlikler> Etkinliklers { get; set; }
        public ICollection<FotoGaleri> FotoGaleri { get; set; }
    }
}
