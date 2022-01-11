using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class SoruKategorilerVM:BaseVM
    {
        [Key]
        public int SoruKategorilerId { get; set; }
        [Required(ErrorMessage = "Kategori adını yazınız...")]
        public string SoruKategorilerAdi { get; set; }

        [Required(ErrorMessage = "Kategori kullanımını seçiniz...")]
        public string SoruKategorilerKullanimi { get; set; }
        
        [Required(ErrorMessage = "Kategori puanını yazınız...")]
        [Range(0, 30, ErrorMessage = "Girdiğiniz sayı 30 dan büyük olamaz !")]
        public int SoruKategorilerPuan { get; set; }

        [Required(ErrorMessage = "Kategori derecesini seçiniz...")]
        public int DereceId { get; set; }

        [Required(ErrorMessage = "Kategori derecesini seçiniz...")]
        public string DereceAdi { get; set; }
        public SoruDerecelerVM Dereceler { get; set; }
        public string KaydedenId { get; set; }
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
        public List<SoruKategoriVM> SoruKategoris { get; set; }
    }
}
