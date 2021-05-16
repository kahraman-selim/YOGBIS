using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YOGBIS.Data.DbModels
{
    public class SoruBankasiLog
    {
        [Key]
        public int SoruBankasiLogId { get; set; }
        public string Soru { get; set; }
        public string Cevap { get; set; }
        public string KullaniciAdi { get; set; }
        public string Islem { get; set; }
        public DateTime KayitTarihi { get; set; }
        [ForeignKey("SoruBankasiId")]
        public int SoruBankasiId { get; set; }
        public SoruBankasi SoruBankasi { get; set; }
    }
}
