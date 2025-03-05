using System;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class DosyaGaleriVM : BaseVM
    {
        public Guid DosyaGaleriId { get; set; }

        [Required(ErrorMessage = "Dosya adı zorunludur")]
        [StringLength(255, ErrorMessage = "Dosya adı en fazla 255 karakter olabilir")]
        [Display(Name = "Dosya Adı")]
        public string DosyaAdi { get; set; }

        [Required(ErrorMessage = "Dosya URL zorunludur")]
        [StringLength(1000, ErrorMessage = "Dosya URL en fazla 1000 karakter olabilir")]
        [Display(Name = "Dosya URL")]
        public string DosyaURL { get; set; }

        [Required(ErrorMessage = "Kaydeden kişi zorunludur")]
        public string KaydedenAdi { get; set; }
        public string KaydedenId { get; set; }

        [Display(Name = "Kaydeden")]
        public KullaniciVM Kullanici { get; set; }
    }
}
