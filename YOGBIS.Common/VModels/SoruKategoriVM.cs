using System;

namespace YOGBIS.Common.VModels
{
    public class SoruKategoriVM
    {
        public Guid SoruId { get; set; }
        public SoruBankasiVM SoruBankasi { get; set; }
        public Guid KategoriId { get; set; }
        public SoruKategorilerVM SoruKategoriler { get; set; }
    }
}
