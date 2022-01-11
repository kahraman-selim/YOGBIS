using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class SoruOnay:Base
    {
        [Key]
        public int SoruOnayId { get; set; }
        public int SoruId { get; set; }
        [ForeignKey("SoruId")]
        public SoruBankasi SoruBankasi { get; set; }
        public int OnayDurumu { get; set; }
        public string OnayAciklama { get; set; }
        public string OnaylayanId { get; set; }
        [ForeignKey("OnaylayanId")]
        public Kullanici Onaylayan { get; set; }

    }
}
