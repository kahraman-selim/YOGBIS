using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Common.VModels
{
    public class SoruKategorilerVM:BaseVM
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid SoruKategorilerId { get; set; }
        [Required(ErrorMessage = "Kategori adını yazınız...")]
        public string SoruKategorilerAdi { get; set; }

        [Required(ErrorMessage = "Kategori kullanımını seçiniz...")]
        public string SoruKategorilerKullanimi { get; set; }
        
        [Required(ErrorMessage = "Kategori puanını yazınız...")]
        [Range(0, 30, ErrorMessage = "Girdiğiniz sayı 30 dan büyük olamaz !")]
        public int SoruKategorilerPuan { get; set; }

        [Required(ErrorMessage = "Kategori derecesini seçiniz...")]
        public Guid DereceId { get; set; }
        public string DereceAdi { get; set; }
        public SoruDerecelerVM SoruDereceler { get; set; }
        public string KaydedenId { get; set; }
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
        public List<SoruKategoriVM> SoruKategoris { get; set; }
    }
}
