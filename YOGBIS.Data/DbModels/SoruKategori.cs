using System;

namespace YOGBIS.Data.DbModels
{
    public class SoruKategori
    {
        public Guid SoruId { get; set; }
        public SoruBankasi SoruBankasi { get; set; }
        public Guid KategoriId { get; set; }
        public SoruKategoriler SoruKategoriler { get; set; }
    }
}
