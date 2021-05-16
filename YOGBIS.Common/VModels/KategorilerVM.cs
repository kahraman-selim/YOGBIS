using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Common.VModels
{
    public class KategorilerVM
    {
        [Key]
        public int KategoriId { get; set; }
        public string KategoriAdi { get; set; }
        public string KategoriKullanimi { get; set; }
        public int KategoriPuan { get; set; }
        public string KategoriDerece { get; set; }
        public List<SoruKategori> SoruKategoris { get; set; }
    }
}
