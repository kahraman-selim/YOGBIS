using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class SoruKategoriler:Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid SoruKategorilerId { get; set; }
        public string SoruKategorilerAdi { get; set; }
        public string SoruKategorilerKullanimi { get; set; }
        public int SoruKategorilerPuan { get; set; }
        public Guid DereceId { get; set; }
        public int SinavKateogoriTurId { get; set; }
        public string SinavKategoriAdi { get; set; }
        public string SinavKategoriTakmaAdi { get; set; }
        public string SinavKategoriTamAdi { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
        public List<SoruKategori> SoruKategori { get; set; }
    }
}
