using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Adaylar:Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid AdayId { get; set; }
        public string TC { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string BabaAd { get; set; }
        public string AnaAd { get; set; }
        public string DogumTarihi { get; set; }
        public string DogumTarihi2 { get; set; }
        //public int? Yasi { get; set; }
        public string DogumYeri { get; set; }
        public string Cinsiyet { get; set; }
        //Kayıt yapan kullanıcı
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }        
        //Bağlı tablolar
        public ICollection<FotoGaleri> FotoGaleri { get; set; }
        public ICollection<DosyaGaleri> DosyaGaleri { get; set; }
        public List<AdaySinavNotlar> AdaySinavNotlar { get; set; }
        public List<AdayDDK> AdayDDK { get; set; }
        public List<AdayGorevKaydi> AdayGorevKaydi { get; set; }
        public List<EPostaAdresleri> EpostaAdresleri { get; set; }
        public List<Telefonlar> Telefonlar { get; set; }
        public List<IkametAdresleri> IkametAdresleri { get; set; }
        public List<AdayBasvuruBilgileri> AdayBasvuruBilgileri { get; set; }
        public List<AdayIletisimBilgileri> AdayIletisimBilgileri { get; set; }
        public List<AdayMYSS> AdayMYSS { get; set; }
        public List<AdayTYS> AdayTYS { get; set; }    
    }
    
}
