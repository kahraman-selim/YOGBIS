using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YOGBIS.Data.DbModels
{
    public class MulakatSorulari
    {
        [Key]
        public int MulakatSorulariId { get; set; }
        public int SoruSiraNo{ get; set; }
        public int SoruNo { get; set; }
        public int SoruKategoriId { get; set; }
        public string SoruKategoriAdi { get; set; }
        public string Derecesi { get; set; }
        public string Soru { get; set; }
        public string Cevap { get; set; }
        [ForeignKey("MulakatId")]
        public int MulakatId { get; set; }
        public Mulakatlar Mulakatlar { get; set; }
    }
}
