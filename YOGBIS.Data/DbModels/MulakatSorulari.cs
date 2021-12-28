using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class MulakatSorulari:Base
    {
        [Key]
        public int MulakatSorulariId { get; set; }
        public int SoruSiraNo { get; set; }
        public int SoruId { get; set; }
        public int SoruKategoriId { get; set; }
        public string SoruKategoriAdi { get; set; }
        public int DereceId { get; set; }
        public string DereceAdi { get; set; }
        public string Soru { get; set; }
        public string Cevap { get; set; }
        public int MulakatId { get; set; }

        [ForeignKey("MulakatId")]
        public Mulakatlar Mulakatlar { get; set; }
        public string KaydedenId { get; set; }

        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
    }
}
