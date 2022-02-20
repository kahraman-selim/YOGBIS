using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class MulakatSorulari:Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid MulakatSorulariId { get; set; }
        public int SoruSiraNo { get; set; }
        public Guid SoruId { get; set; }
        public Guid DereceId { get; set; }
        public string DereceAdi { get; set; }
        public Guid SoruKategoriId { get; set; }
        public string SoruKategoriAdi { get; set; } 
        public string Soru { get; set; }
        public string Cevap { get; set; }
        public Guid MulakatId { get; set; }
        [ForeignKey("MulakatId")]
        public Mulakatlar Mulakatlar { get; set; }
        public int? SorulanAdayTC { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
    }
}
