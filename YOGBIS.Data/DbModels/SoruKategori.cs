using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Data.DbModels
{
    public class SoruKategori
    {
        [Key]
        public int SoruKategoriId { get; set; }
        public int SoruId { get; set; }
        public SoruBankasi SoruBankasi { get; set; }
        public int KategoriId { get; set; }
        public SoruKategoriler SoruKategoriler { get; set; }
    }
}
