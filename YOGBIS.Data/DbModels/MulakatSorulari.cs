using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YOGBIS.Data.DbModels
{
    public class MulakatSorulari:Base
    {
        [Key]
        public int MulakatSorulariId { get; set; }
        public int SoruSiraNo{ get; set; }
        public int SoruId { get; set; }
        [ForeignKey("SoruId")]
        public SoruBankasi  SoruBankasi { get; set; }
        public int SoruKategoriId { get; set; }
        [ForeignKey("SoruKategoriId")]
        public SoruKategoriler SoruKategoriler { get; set; }
        public string SoruKategoriAdi { get; set; }
        public string Derecesi { get; set; }
        public string Soru { get; set; }
        public string Cevap { get; set; }
        [ForeignKey("MulakatId")]
        public int MulakatId { get; set; }
        public Mulakatlar Mulakatlar { get; set; }
        public string KullaniciId { get; set; }
        [ForeignKey("KullaniciId")]
        public Kullanici Kullanici { get; set; }
    }
}
