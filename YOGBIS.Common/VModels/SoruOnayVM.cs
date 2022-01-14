using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class SoruOnayVM:BaseVM
    {
        [Key]
        public int SoruOnayId { get; set; }
        public int SoruBankasiId { get; set; }        
        public SoruBankasiVM SoruBankasi { get; set; }
        public int OnayDurumu { get; set; }
        public string OnayAciklama { get; set; }
        public string OnaylayanId { get; set; }
        public string OnaylayanAdi { get; set; }
        public KullaniciVM Onaylayan { get; set; }
    }
}
