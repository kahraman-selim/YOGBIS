using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Dereceler:Base
    {
        [Key]
        public int DereceId { get; set; }
        public string DereceAdi { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
        public List<SoruDerece> SoruDereces { get; set; }
        public List<SoruKategoriler> SoruKategorilers { get; set; }
    }
}
