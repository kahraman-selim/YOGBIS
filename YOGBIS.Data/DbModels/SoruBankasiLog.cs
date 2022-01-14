using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class SoruBankasiLog
    {
        [Key]
        public int SoruBankasiLogId { get; set; }
        public int SoruBankasiId { get; set; }
        [ForeignKey("SoruBankasiId")]
        public SoruBankasi SoruBankasi { get; set; }
        public string Soru { get; set; }
        public string Cevap { get; set; }
        public int DereceId { get; set; }
        public int SoruKategoriId { get; set; }
        public int SorulmaSayisi { get; set; }
        public bool SoruDurumu { get; set; }
        public int KayitTuru { get; set; }
        public DateTime KayitTarihi { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kaydeden { get; set; }
    }
}
