using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Ulkeler : Base
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
        public virtual Kitalar Kita { get; set; }
        
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public virtual Kullanici Kullanici { get; set; }
        
        public virtual ICollection<Ogrenciler> Ogrenciler { get; set; }
        public virtual ICollection<AdayGorevKaydi> Gorevliler { get; set; }
        public virtual ICollection<Universiteler> Universiteler { get; set; }
        public virtual ICollection<Okullar> Okullar { get; set; }
        public virtual ICollection<Sehirler> Sehirler { get; set; }
        public virtual ICollection<Eyaletler> Eyaletler { get; set; }
        public virtual ICollection<Temsilcilikler> Temsilcilikler { get; set; }
        public virtual ICollection<FotoGaleri> FotoGaleri { get; set; }
    }
}
