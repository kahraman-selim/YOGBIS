using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class SoruBankasi:Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid SoruBankasiId { get; set; }
        public string Soru { get; set; }
        public string Cevap { get; set; }
        public int SorulmaSayisi { get; set; }
        public bool SoruDurumu { get; set; }                
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kaydeden { get; set; }
        public List<SoruKategori> SoruKategori { get; set; }
        public List<SoruDerece> SoruDerece { get; set; }
        public List<SoruOnay> SoruOnay { get; set; }
        public List<SoruBankasiLog> SoruBankasiLog { get; set; }
    }
}
