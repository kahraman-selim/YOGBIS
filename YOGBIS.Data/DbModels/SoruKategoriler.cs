using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class SoruKategoriler:Base
    {
        [Key]
        public int SoruKategorilerId { get; set; }
        public string SoruKategorilerAdi { get; set; }
        public string SoruKategorilerKullanimi { get; set; }
        public int SoruKategorilerPuan { get; set; }
        public int DereceId { get; set; }        
        [ForeignKey("DereceId")]
        public Dereceler Dereceler { get; set; }
        public string KullaniciId { get; set; }
        [ForeignKey("KullaniciId")]
        public Kullanici Kullanici { get; set; }
        public List<SoruKategori> SoruKategoris { get; set; }
    }
}
