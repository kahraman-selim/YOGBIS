﻿namespace YOGBIS.Common.VModels
{
    public class SoruKategoriVM
    {
        public int SoruKategoriId { get; set; }
        public int SoruId { get; set; }
        public SoruBankasiVM SoruBankasi { get; set; }
        public int KategoriId { get; set; }
        public SoruKategorilerVM SoruKategoriler { get; set; }
    }
}
