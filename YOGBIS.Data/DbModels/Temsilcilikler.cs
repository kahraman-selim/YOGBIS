using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Temsilcilikler : Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid TemsilcilikId { get; set; }
        public string TemsilcilikAdi { get; set; }
        public string TemsilcilikTuru { get; set; }
        public string TemsilciId { get; set; }
        public string TemsilciGorevi { get; set; }
        public string TemsilcilikTel { get; set; }
        public string TemsilcilikEPosta { get; set; }
        public string TemsilcilikWebAdres { get; set; }

        public Guid UlkeId { get; set; }
        [ForeignKey("UlkeId")]
        public virtual Ulkeler Ulke { get; set; }

        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public virtual Kullanici Kullanici { get; set; }

        public virtual ICollection<EPostaAdresleri> EpostaAdresleri { get; set; }
        public virtual ICollection<Telefonlar> Telefonlar { get; set; }
        public virtual ICollection<Eyaletler> Eyaletler { get; set; }
        public virtual ICollection<Sehirler> Sehirler { get; set; }
        public virtual ICollection<Okullar> Okullar { get; set; }
        public virtual ICollection<Personeller> Personeller { get; set; }
        public virtual ICollection<AdayGorevKaydi> Gorevliler { get; set; }
        public virtual ICollection<Etkinlikler> Etkinlikler { get; set; }
        public virtual ICollection<Ogrenciler> Ogrenciler { get; set; }
    }
}
