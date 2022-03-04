using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Ulkeler:Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid UlkeId { get; set; }
        public string UlkeKodu { get; set; }
        public string UlkeAdi { get; set; }
        public string UlkeBayrakURL { get; set; }
        public string UlkeBayrakAdi { get; set; }
        public string UlkeAciklama { get; set; }
        public bool Aktif { get; set; }
        public Guid KitaId { get; set; }
        [ForeignKey("KitaId")]
        public Kitalar Kitalar { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
        public List<Ogrenciler> Ogrenciler { get; set; }
        public List<AdayGorevKaydi> Gorevliler { get; set; }
        public List<Universiteler> Universiteler { get; set; }
        public List<Okullar> Okullar { get; set; }
        public List<Sehirler> Sehirler { get; set; }
        public List<Eyaletler> Eyaletler { get; set; }        
        public List<Temsilcilikler> Temsilcilikler { get; set; }
        public ICollection<FotoGaleri> FotoGaleri { get; set; }
    }
}
