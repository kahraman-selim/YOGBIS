using System;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class OkulBilgiVM : BaseVM
    {
        public Guid OkulBilgiId { get; set; }

        [Required(ErrorMessage = "Okul bilgi adı zorunludur")]
        [Display(Name = "Okul Bilgi Adı")]
        public string OkulBilgiAdi { get; set; }

        [Required(ErrorMessage = "Telefon zorunlu bir alandır")]
        [Display(Name = "Okul Telefonu")]
        public string OkulTelefon { get; set; }
        
        [Required(ErrorMessage = "Okul adresi zorunlu bir alandır")]
        [Display(Name = "Okul Adresi")]
        public string OkulAdres { get; set; }

        [Required(ErrorMessage = "Müdür adı ve soyadı zorunlu bir alandır")]
        [Display(Name = "Müdür Adı Soyadı")]
        public string MudurAdiSoyadi { get; set; }

        [Required(ErrorMessage = "Müdür telefonu zorunlu bir alandır")]
        [Display(Name = "Müdür Telefonu")]
        public string MudurTelefon { get; set; }

        [EmailAddress(ErrorMessage = "Girilen bilgi E-Posta adresi değil")]
        [Required(ErrorMessage = "Müdür E-Posta zorunlu bir alandır")]
        [Display(Name = "Müdür E-Posta")]
        public string MudurEPosta { get; set; }

        [Required(ErrorMessage = "Dönüş Yılı zorunlu bir alandır")]
        [Display(Name = "Müdür Dönüş Yılı")]
        public string MudurDonusYil { get; set; }
        
        [Display(Name = "Müdür Yardımcısı Adı Soyadı")]
        public string MdYrdAdiSoyadi { get; set; }

        [Display(Name = "Müdür Yardımcısı Telefonu")]
        public string MdYrdTelefon { get; set; }

        [EmailAddress(ErrorMessage = "Girilen bilgi E-Posta adresi değil")]
        [Display(Name = "Müdür Yardımcısı E-Posta")]
        public string MdYrdEPosta { get; set; }

        [Display(Name = "Müdür Yardımcısı Dönüş Yılı")]
        public string MdYrdDonusYil { get; set; }

        [Required(ErrorMessage = "Okul seçimi zorunludur")]
        public Guid OkulId { get; set; }

        [Display(Name = "Okul")]
        public string OkulAdi { get; set; }
        public OkullarVM Okullar { get; set; }

        [Required(ErrorMessage = "Ülke seçimi zorunludur")]
        public string UlkeAdi { get; set; }
        public Guid UlkeId { get; set; }

        [Display(Name = "Ülke")]
        public UlkelerVM Ulkeler { get; set; }

        [Required(ErrorMessage = "Kaydeden kişi zorunludur")]
        public string KaydedenId { get; set; }

        [Display(Name = "Kaydeden")]
        public string KaydedenAdi { get; set; }

        public KullaniciVM Kullanici { get; set; }
    }
}
