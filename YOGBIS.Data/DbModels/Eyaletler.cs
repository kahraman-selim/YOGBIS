using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Eyaletler:Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid EyaletId { get; set; }
        public string EyaletAdi { get; set; }
        public string EyaletAciklama { get; set; }
        public Guid UlkeId { get; set; }
        public string TemsilciId { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
        public List<Sehirler> Sehirlers { get; set; }
        public List<Okullar> Okullar { get; set; }
        public List<Universiteler> Universiteler { get; set; }
        public List<AdayGorevKaydi> AdayGorevKaydi { get; set; }
        public List<Ogrenciler> Ogrenciler { get; set; }
    }
}
