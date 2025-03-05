using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Eyaletler : Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid EyaletId { get; set; }
        public string EyaletAdi { get; set; }
        public string EyaletAciklama { get; set; }
        
        public Guid UlkeId { get; set; }
        [ForeignKey("UlkeId")]
        public virtual Ulkeler Ulke { get; set; }
        
        public Guid TemsilciId { get; set; }
        [ForeignKey("TemsilciId")]
        public virtual Temsilcilikler Temsilcilik { get; set; }

        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public virtual Kullanici Kullanici { get; set; }
        
        public virtual ICollection<Sehirler> Sehirler { get; set; }
        public virtual ICollection<Okullar> Okullar { get; set; }
        public virtual ICollection<Universiteler> Universiteler { get; set; }
        public virtual ICollection<AdayGorevKaydi> AdayGorevKaydi { get; set; }
        public virtual ICollection<Ogrenciler> Ogrenciler { get; set; }
    }
}
