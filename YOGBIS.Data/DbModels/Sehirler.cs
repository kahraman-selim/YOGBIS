using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Sehirler:Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid SehirId { get; set; }
        public string SehirAdi { get; set; }
        public bool? Baskent { get; set; }
        public string SehirAciklama { get; set; }
        public int SehirVatandas { get; set; }
        public string TemsilciId { get; set; }
        public Guid UlkeId { get; set; }
        public Guid? EyaletId { get; set; }
        [ForeignKey("EyaletId")]
        public Eyaletler Eyaletler { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
        public List<Okullar> Okullars { get; set; }
        public List<Universiteler> Universitelers { get; set; }
        public List<AdayGorevKaydi> AdayGorevKaydis { get; set; }
        public ICollection<FotoGaleri> FotoGaleri { get; set; }
    }
}
