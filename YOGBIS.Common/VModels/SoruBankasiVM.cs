using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Common.VModels
{
    public class SoruBankasiVM
    {
        [Key]
        public int SoruBankasiId { get; set; }
        public int SoruKategoriId { get; set; }
        public string Soru { get; set; }
        public string Cevap { get; set; }
        public string Derecesi { get; set; }
        public int SorulmaSayisi { get; set; }
        public string SoruDurumu { get; set; }
        public List<SoruKategori> SoruKategoris { get; set; }
    }
}
