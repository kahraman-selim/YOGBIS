using System;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class UlkeTercihBranslarVM : BaseVM
    {
        public Guid TercihBransId { get; set; }

        [Required(ErrorMessage = "Branş adı zorunludur")]
        [Display(Name = "Branş Adı")]
        public string BransAdi { get; set; }

        [Required(ErrorMessage = "Branş seçimi zorunludur")]
        public Guid BransId { get; set; }

        [Display(Name = "Branş")]
        public BranslarVM Brans { get; set; }

        [Display(Name = "Cinsiyet")]
        public string BransCinsiyet { get; set; }

        [Display(Name = "Kontenjan Sayısı")]
        public int BransKontSayi { get; set; }

        [Display(Name = "Eşit Branş")]
        public bool EsitBrans { get; set; }

        [Display(Name = "Yabancı Dil")]
        public string YabanciDil { get; set; }

        [Required(ErrorMessage = "Ülke tercihi zorunludur")]
        public string UlkeTercihAdi { get; set; }
        public Guid UlkeTercihId { get; set; }

        [Display(Name = "Ülke Tercihi")]
        public UlkeTercihVM UlkeTercih { get; set; }

        [Required(ErrorMessage = "Kaydeden kişi zorunludur")]
        public string KaydedenId { get; set; }

        [Display(Name = "Kaydeden")]
        public string KaydedenAdi { get; set; }

        public KullaniciVM Kullanici { get; set; }
    }
}
