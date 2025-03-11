using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Adaylar : Base
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
        public string DogumYeri { get; set; }
        public string Cinsiyet { get; set; }
        public int MulakatYil { get; set; }

        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public virtual Kullanici Kullanici { get; set; }

        public virtual ICollection<FotoGaleri> FotoGaleri { get; set; }
        public virtual ICollection<DosyaGaleri> DosyaGaleri { get; set; }
        public virtual ICollection<AdaySinavNotlar> AdaySinavNotlar { get; set; }
        public virtual ICollection<AdayDDK> AdayDDK { get; set; }
        public virtual ICollection<AdayGorevKaydi> AdayGorevKaydi { get; set; }
        public virtual ICollection<EPostaAdresleri> EpostaAdresleri { get; set; }
        public virtual ICollection<Telefonlar> Telefonlar { get; set; }
        public virtual ICollection<IkametAdresleri> IkametAdresleri { get; set; }
        public virtual ICollection<AdayBasvuruBilgileri> AdayBasvuruBilgileri { get; set; }
        public virtual ICollection<AdayIletisimBilgileri> AdayIletisimBilgileri { get; set; }
        public virtual ICollection<AdayMYSS> AdayMYSS { get; set; }
        public virtual ICollection<AdayTYS> AdayTYS { get; set; }
    }
}
