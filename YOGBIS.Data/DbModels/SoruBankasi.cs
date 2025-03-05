using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class SoruBankasi : Base
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
        public virtual Kullanici Kaydeden { get; set; }

        public virtual ICollection<SoruKategori> SoruKategori { get; set; }
        public virtual ICollection<SoruDerece> SoruDerece { get; set; }
        public virtual ICollection<SoruOnay> SoruOnay { get; set; }
        public virtual ICollection<SoruBankasiLog> SoruBankasiLog { get; set; }
    }
}
