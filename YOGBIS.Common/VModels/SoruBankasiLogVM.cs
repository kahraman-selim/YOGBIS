using System;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class SoruBankasiLogVM
    {
        [Key]
        public int SoruBankasiLogId { get; set; }
        public int SoruBankasiId { get; set; }
        public SoruBankasiVM SoruBankasi { get; set; }
        public string Soru { get; set; }
        public string Cevap { get; set; }
        public int DereceId { get; set; }
        public int SoruKategoriId { get; set; }
        public int SorulmaSayisi { get; set; }
        public bool SoruDurumu { get; set; }
        public int KayitTuru { get; set; }
        public DateTime KayitTarihi { get; set; }
        public string KaydedenId { get; set; }
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kaydeden { get; set; }
    }
}
