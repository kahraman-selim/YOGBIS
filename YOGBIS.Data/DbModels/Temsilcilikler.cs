using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Temsilcilikler:Base
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
        public List<EPostaAdresleri> EpostaAdresleri { get; set; }
        public List<Telefonlar> Telefonlar { get; set; }
        public List<Eyaletler> Eyaletler { get; set; }
        public List<Sehirler> Sehirler { get; set; }
        public List<Okullar> Okullar { get; set; }
        public List<Personeller> Personeller { get; set; }
        public List<AdayGorevKaydi> Gorevliler { get; set; }
        public List<Etkinlikler> Etkinlikler { get; set; }
        public List<Ogrenciler> Ogrenciler { get; set; }
        public Guid UlkeId { get; set; }
        [ForeignKey("UlkeId")]
        public Ulkeler  Ulkeler { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
    }
}
