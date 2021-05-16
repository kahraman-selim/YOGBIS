using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YOGBIS.Data.DbModels
{
    public class SoruKategori
    {
        [Key]
        public int SoruKategoriId { get; set; }
        public int SoruId { get; set; }
        public SoruBankasi SoruBankasi { get; set; }
        public int KategoriId { get; set; }
        public Kategoriler Kategoriler { get; set; }
    }
}
