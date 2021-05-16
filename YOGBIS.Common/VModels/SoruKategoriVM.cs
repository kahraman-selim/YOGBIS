using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Common.VModels
{
    public class SoruKategoriVM
    {
        [Key]
        public int SoruKategoriId { get; set; }
        public int SoruId { get; set; }
        public SoruBankasi SoruBankasi { get; set; }
        public int KategoriId { get; set; }
        public Kategoriler Kategoriler { get; set; }
    }
}
