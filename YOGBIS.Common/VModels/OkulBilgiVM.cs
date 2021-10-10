using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YOGBIS.Common.VModels
{
    public class OkulBilgiVM:BaseVM
    {
        [Key]
        public int OkulBilgiId { get; set; }

        [Required(ErrorMessage = "Telefon zorunlu bir alandır")]
        public string OkulTelefon { get; set; }

        [Required(ErrorMessage = "Görev seçimini yapınız")]
        public string YoneticiGorev { get; set; }

        [Required(ErrorMessage = "Yönetici adı ve soyadı zorunlu bir alandır")]
        public string YoneticiAdiSoyadi { get; set; }

        [Required(ErrorMessage = "Yönetici telefonu zorunlu bir alandır")]
        public string YoneticiTelefon { get; set; }

        [Required(ErrorMessage = "Okul adı zorunlu bir alandır")]
        public int OkulId { get; set; }

        [Required(ErrorMessage = "Okul adı zorunlu bir alandır")]
        public string OkulAdi { get; set; }
        public OkullarVM Okullar { get; set; }

        [Required(ErrorMessage = "Bulunduğunuz ülkeyi seçiniz")]
        public int UlkeId { get; set; }

        [Required(ErrorMessage = "Bulunduğunuz ülkeyi seçiniz")]
        public string UlkeAdi { get; set; }
        public UlkelerVM Ulkeler { get; set; }
        public string KullaniciId { get; set; }
        public KullaniciVM Kullanici { get; set; }
    }
}
