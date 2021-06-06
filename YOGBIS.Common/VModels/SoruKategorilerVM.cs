using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Common.VModels
{
    public class SoruKategorilerVM
    {
        [Key]
        public int SoruKategoriId { get; set; }
        public string SoruKategoriAdi { get; set; }
        public string SoruKategoriKullanimi { get; set; }
        public int SoruKategoriPuan { get; set; }
        public string SoruKategoriDerece { get; set; }
        public List<SoruKategori> SoruKategoris { get; set; }
    }
}
