using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class SoruBankasiLog
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid SoruBankasiLogId { get; set; }
        public Guid SoruBankasiId { get; set; }
        [ForeignKey("SoruBankasiId")]
        public SoruBankasi SoruBankasi { get; set; }
        public string Soru { get; set; }
        public string Cevap { get; set; }
        public Guid DereceId { get; set; }
        public Guid SoruKategoriId { get; set; }
        public int SorulmaSayisi { get; set; }
        public bool SoruDurumu { get; set; }
        public int KayitTuru { get; set; }
        public DateTime KayitTarihi { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kaydeden { get; set; }
    }
}
