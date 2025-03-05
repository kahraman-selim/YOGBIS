using System;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class FotoGaleriVM : BaseVM
    {
        public Guid FotoGaleriId { get; set; }

        [Required(ErrorMessage = "Fotoğraf adı zorunludur")]
        [StringLength(255, ErrorMessage = "Fotoğraf adı en fazla 255 karakter olabilir")]
        [Display(Name = "Fotoğraf Adı")]
        public string FotoAdi { get; set; }

        [Required(ErrorMessage = "Fotoğraf URL zorunludur")]
        [StringLength(1000, ErrorMessage = "Fotoğraf URL en fazla 1000 karakter olabilir")]
        [Display(Name = "Fotoğraf URL")]
        public string FotoURL { get; set; }

        [Required(ErrorMessage = "Kaydeden kişi zorunludur")]
        public string KaydedenId { get; set; }

        [Display(Name = "Kaydeden")]
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
    }
}
