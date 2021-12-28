namespace YOGBIS.Data.DbModels
{
    public class SoruKategori
    {
        public int SoruId { get; set; }
        public SoruBankasi SoruBankasi { get; set; }
        public int KategoriId { get; set; }
        public SoruKategoriler SoruKategoriler { get; set; }
    }
}
