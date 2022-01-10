using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class MulakatlarVM:BaseVM
    {
        [Key]
        public int MulakatId { get; set; }
        [Required(ErrorMessage ="Onay Sayısı zorunlu bir alandır !")]
        public string OnaySayisi { get; set; }        
        public string MulakatAdi { get; set; }
        [Required(ErrorMessage = "Derece seçimi yapınız !")]
        public int DereceId { get; set; }
        public string DereceAdi { get; set; }
        [Required(ErrorMessage = "Derece seçimi yapınız !")]
        public DerecelerVM Dereceler { get; set; }

        [Required(ErrorMessage = "Başlama Tarihini belirtiniz !")]
        public DateTime BaslamaTarihi { get; set; }
        
        [Required(ErrorMessage = "Bitiş Tarihini belirtiniz !")]
        public DateTime BitisTarihi { get; set; }
        public int? AdaySayisi { get; set; } = 0;
        [Required(ErrorMessage = "Adaylar için hazırlanacak soru sayısını belirtiniz !")]
        public int SorulanSoruSayisi { get; set; } = 0;
        [Required(ErrorMessage = "Onay Durumunu belirtiniz !")]
        public bool? Durumu { get; set; } = true;
        public string MulakatAciklama { get; set; }
        public string KaydedenId { get; set; }
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
        public List<MulakatSorulariVM> MulakatSorularis { get; set; }
        public List<AdaylarVM> Adaylars { get; set; }
    }
}
