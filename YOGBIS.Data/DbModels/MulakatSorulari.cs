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
        public int SoruNo { get; set; }
        public Guid DereceId { get; set; }
        [ForeignKey("DereceId")]
        public SoruDereceler SoruDereceler { get; set; }
        public Guid SoruKategorilerId { get; set; }
        [ForeignKey("SoruKategorilerId")]
        public SoruKategoriler SoruKategoriler { get; set; }
        public int SoruKategoriSiraNo { get; set; }
        public string Soru { get; set; }
        public string Cevap { get; set; }
        public int SinavKateogoriTurId { get; set; }
        public string SinavKategoriTurAdi { get; set; }
        public Guid MulakatId { get; set; }
        [ForeignKey("MulakatId")]
        public Mulakatlar Mulakatlar { get; set; }       
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
    }
}
