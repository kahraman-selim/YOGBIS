using System;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class SSSCevapVM : BaseVM
    {
        public Guid SSSCevapId { get; set; }

        [Required(ErrorMessage = "Cevap zorunludur")]
        [Display(Name = "Cevap")]
        public string Cevap { get; set; }

        [Required(ErrorMessage = "Soru zorunludur")]
        public Guid SSSId { get; set; }

        [Display(Name = "Soru")]
        public SSSVM SSS { get; set; }

        [Required(ErrorMessage = "Kaydeden kişi zorunludur")]
        public string KaydedenId { get; set; }

        [Display(Name = "Kaydeden")]
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
    }
}
