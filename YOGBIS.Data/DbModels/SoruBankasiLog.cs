using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class SoruBankasiLog:Base
    {
        public int SoruBankasiLogId { get; set; }
        public int SoruId { get; set; }
        [ForeignKey("SoruId")]
        public SoruBankasi SoruBankasi { get; set; }
        public string Soru { get; set; }
        public string Cevap { get; set; }
        public int SorulmaSayisi { get; set; }
        public bool SoruDurumu { get; set; }
        public int KayitTuru { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kaydeden { get; set; }
        public List<SoruKategori> SoruKategoris { get; set; }
        public List<SoruDerece> SoruDereces { get; set; }

    }
}
