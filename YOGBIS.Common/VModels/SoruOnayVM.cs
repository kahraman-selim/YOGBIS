using System;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class SoruOnayVM : BaseVM
    {
        public Guid SoruOnayId { get; set; }

        [Required(ErrorMessage = "Onay durumu zorunludur")]
        [Display(Name = "Onay Durumu")]
        public bool OnayDurumu { get; set; }

        [Display(Name = "Onay Açıklaması")]
        public string OnayAciklama { get; set; }

        [Required(ErrorMessage = "Soru bankası seçimi zorunludur")]
        public Guid SoruBankasiId { get; set; }

        [Display(Name = "Soru Bankası")]
        public SoruBankasiVM SoruBankasi { get; set; }

        [Required(ErrorMessage = "Onaylayan kişi zorunludur")]
        public string OnaylayanId { get; set; }

        [Display(Name = "Onaylayan")]
        public KullaniciVM Onaylayan { get; set; }

        [Required(ErrorMessage = "Kaydeden kişi zorunludur")]
        public string KaydedenId { get; set; }

        [Display(Name = "Kaydeden")]
        public string KaydedenAdi { get; set; }
    }
}
