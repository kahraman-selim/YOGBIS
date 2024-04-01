using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Common.VModels
{
    public class MulakatlarVM:BaseVM
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid MulakatId { get; set; }
        [Required(ErrorMessage ="Onay Sayısı zorunlu bir alandır !")]
        public string OnaySayisi { get; set; }
        [Required(ErrorMessage = "Onay Tarihi zorunlu bir alandır !")]
        public DateTime OnayTarihi { get; set; }
        [Required(ErrorMessage = "Onay Tarihi zorunlu bir alandır !")]
        public string KararSayisi { get; set; }
        [Required(ErrorMessage = "Karar Sayısı zorunlu bir alandır !")]
        public DateTime KararTarihi { get; set; }
        [Required(ErrorMessage = "Karar Tarihi zorunlu bir alandır !")]
        public DateTime YazılıSinavTarihi { get; set; }
        public int MulakatKategoriId { get; set; }
        public string MulakatAdi { get; set; }
        public string MulakatDonemi { get; set; }
        [Required(ErrorMessage = "Derece seçimi yapınız !")]
        public Guid DereceId { get; set; }
        public string DereceAdi { get; set; }
        [Required(ErrorMessage = "Derece seçimi yapınız !")]
        public SoruDerecelerVM Dereceler { get; set; }
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
        public List<KomisyonlarVM> Komisyonlars { get; set; }
    }
}
