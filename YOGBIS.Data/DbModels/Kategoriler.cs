using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YOGBIS.Data.DbModels
{
    public class Kategoriler
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
