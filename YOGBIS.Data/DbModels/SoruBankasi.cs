using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YOGBIS.Data.DbModels
{
    public class SoruBankasi:Base
    {
        [Key]
        public int SoruBankasiId { get; set; }
        public int SoruKategorilerId { get; set; }        
        [ForeignKey ("SoruKategorilerId")]
        public SoruKategoriler SoruKategoriler { get; set; }
        public string Soru { get; set; }
        public string Cevap { get; set; }
        public int DereceId { get; set; }
        [ForeignKey("DereceId")]
        public Dereceler Dereceler { get; set; }
        public int SorulmaSayisi { get; set; } = 0;
        public bool SoruDurumu { get; set; } = true;      
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kaydeden { get; set; }
        public string OnaylayanId { get; set; }
        [ForeignKey("OnaylayanId")]
        public Kullanici Onaylayan { get; set; }
        public int OnayDurumu { get; set; } = 1;
        public string OnayAciklama { get; set; }
        public List<SoruKategori> SoruKategoris { get; set; }

    }
}
