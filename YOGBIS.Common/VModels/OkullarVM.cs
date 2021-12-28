using System;
using System.Collections.Generic;
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
        public string OkulLogo { get; set; }
        public string OkulFoto { get; set; }
        public bool? OkulLab { get; set; }
        public bool? OkulKutuphane { get; set; }
        public string OkulBilgi { get; set; }
        public DateTime OkulAcilisTarihi { get; set; }
        public bool? OkulDurumu { get; set; }

        [Required(ErrorMessage = "Şehir seçimi yapınız")]
        public int SehirId { get; set; }
        public SehirlerVM Sehirler { get; set; }
        public string KaydedenId { get; set; }
        public KullaniciVM Kullanici { get; set; }
        public List<SubelerVM> Subelers { get; set; }
        public List<OgretmenlerVM> Ogretmenlers { get; set; }
    }
}
