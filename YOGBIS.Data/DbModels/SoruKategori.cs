using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YOGBIS.Data.DbModels
{
    public class SoruKategori
    {
        public int Id { get; set; }
        public int SoruId { get; set; }
        public SoruBankasi SoruBankasi { get; set; }
        public int KategoriId { get; set; }
        public SoruKategoriler SoruKategoriler { get; set; }
    }
}
