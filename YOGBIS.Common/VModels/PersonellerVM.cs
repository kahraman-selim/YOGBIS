using System;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class PersonellerVM : BaseVM
    {
        public Guid PersonellerId { get; set; }

        [Required(ErrorMessage = "Personel adı zorunludur")]
        [Display(Name = "Personel Adı")]
        public string PersonelAdi { get; set; }

        [Required(ErrorMessage = "Personel soyadı zorunludur")]
        [Display(Name = "Personel Soyadı")]
        public string PersonelSoyadi { get; set; }

        [Display(Name = "Personel Durumu")]
        public bool? Durumu { get; set; }

        [Required(ErrorMessage = "Kaydeden kişi zorunludur")]
        public string KaydedenId { get; set; }

        [Display(Name = "Kaydeden")]
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
    }
}
