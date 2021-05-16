using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Common.VModels
{
    public class SoruBankasiLogVM
    {
        [Key]
        public int SoruBankasiLogId { get; set; }
        public string Soru { get; set; }
        public string Cevap { get; set; }
        public string KullaniciAdi { get; set; }
        public string Islem { get; set; }
        public DateTime KayitTarihi { get; set; }
        public int SoruBankasiId { get; set; }
        public SoruBankasi SoruBankasi { get; set; }
    }
}
