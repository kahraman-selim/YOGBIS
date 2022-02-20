using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Common.VModels
{
    public class SoruOnayVM:BaseVM
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid SoruOnayId { get; set; }
        public Guid SoruBankasiId { get; set; }        
        public SoruBankasiVM SoruBankasi { get; set; }
        public int OnayDurumu { get; set; }
        public string OnayAciklama { get; set; }
        public string OnaylayanId { get; set; }
        public string OnaylayanAdi { get; set; }
        public KullaniciVM Onaylayan { get; set; }
    }
}
