using System;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class GorevKararPdfGaleriVM : BaseVM
    {
        public Guid GorevKararPdfGaleriId { get; set; }

        [Required(ErrorMessage = "Dosya adı zorunludur")]
        [Display(Name = "Dosya Adı")]
        public string DosyaAdi { get; set; }

        [Display(Name = "Dosya URL")]
        public string DosyaURL { get; set; }

        [Required(ErrorMessage = "Kaydeden kişi zorunludur")]
        public string KaydedenId { get; set; }

        [Display(Name = "Kaydeden")]
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
    }
}
