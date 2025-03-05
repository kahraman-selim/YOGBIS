using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class SoruBankasiLog : Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid SoruBankasiLogId { get; set; }
        public string Soru { get; set; }
        public string Cevap { get; set; }
        public int SorulmaSayisi { get; set; }
        public bool SoruDurumu { get; set; }
        public int KayitTuru { get; set; }
       

        public Guid SoruBankasiId { get; set; }
        [ForeignKey("SoruBankasiId")]
        public virtual SoruBankasi SoruBankasi { get; set; }

        public Guid DereceId { get; set; }
        [ForeignKey("DereceId")]
        public virtual SoruDereceler SoruDerece { get; set; }

        public Guid SoruKategoriId { get; set; }
        [ForeignKey("SoruKategoriId")]
        public virtual SoruKategoriler SoruKategori { get; set; }

        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public virtual Kullanici Kaydeden { get; set; }
    }
}
