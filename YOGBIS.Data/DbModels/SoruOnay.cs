using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class SoruOnay : Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid SoruOnayId { get; set; }
        public int OnayDurumu { get; set; }
        public string OnayAciklama { get; set; }

        public Guid SoruBankasiId { get; set; }
        [ForeignKey("SoruBankasiId")]
        public virtual SoruBankasi SoruBankasi { get; set; }

        public string OnaylayanId { get; set; }
        [ForeignKey("OnaylayanId")]
        public virtual Kullanici Onaylayan { get; set; }
    }
}
