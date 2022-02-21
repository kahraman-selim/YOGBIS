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
        public string OkulLogoURL { get; set; } 
        public string OkulBilgi { get; set; }
        public DateTime? OkulAcilisTarihi { get; set; }
        public bool OkulDurumu { get; set; }
        public string OkulMudurId { get; set; }
        public string OkulHizmetGecisDonem { get; set; }
        public string OkulKapaliAlan { get; set; }
        public string OkulAcikAlan { get; set; }
        public bool? OkulMulkiDurum { get; set; }
        public string OkulInternetAdresi { get; set; }
        public string OkulEPostaAdresi { get; set; }
        public string OkulTelefon { get; set; }
        public Guid? OkulUlkeId { get; set; }
        ///////////////////////////////////////////////        
        public Guid? SehirId { get; set; }
        [ForeignKey("SehirId")]
        public Sehirler Sehirler { get; set; }
        public Guid? EyaletId { get; set; }
        [ForeignKey("EyaletId")]
        public Eyaletler Eyaletler { get; set; }      
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
