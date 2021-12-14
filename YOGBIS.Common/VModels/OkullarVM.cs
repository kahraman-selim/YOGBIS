using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class OkullarVM:BaseVM
    {
        [Key]
        public int OkulId { get; set; }

        [Required(ErrorMessage = "Okul kodu zorunlu bir alandır")]
        public int OkulKodu { get; set; }

        [Required(ErrorMessage = "Okul adı zorunlu bir alandır")]
        public string OkulAdi { get; set; }

        [Required(ErrorMessage = "Ülke seçimi yapınız")]
        public int UlkeId { get; set; }        

        [Required(ErrorMessage = "Ülke seçimi yapınız")]
        public string UlkeAdi { get; set; }
        public UlkelerVM Ulkeler { get; set; }
        public string KullaniciId { get; set; }
        public string KullaniciAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
    }
}
