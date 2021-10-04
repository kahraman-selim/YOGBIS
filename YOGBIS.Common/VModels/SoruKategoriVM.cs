using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Common.VModels
{
    public class SoruKategoriVM
    {
        public int SoruKategoriId { get; set; }
        public int SoruId { get; set; }
        public SoruBankasiVM SoruBankasiVm { get; set; }
        public int KategoriId { get; set; }
        public SoruKategorilerVM SoruKategorilerVm { get; set; }
    }
}
