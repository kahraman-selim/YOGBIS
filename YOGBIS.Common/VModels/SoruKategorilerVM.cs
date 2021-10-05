using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Common.VModels
{
    public class SoruKategorilerVM:BaseVM
    {
        [Key]
        [Required(ErrorMessage = "Kategori numarasını yazınız...")]
        [Range(1, int.MaxValue, ErrorMessage = "{0} dan küçük bir sayı giremezsiniz !")]
        public int SoruKategorilerId { get; set; }
        [Required(ErrorMessage = "Kategori adını yazınız...")]
        public string SoruKategorilerAdi { get; set; }

        [Required(ErrorMessage = "Kategori kullanımını seçiniz...")]
        public string SoruKategorilerKullanimi { get; set; }

        [Range(0, 30, ErrorMessage = "30 dan fazla bir sayı giremezsiniz !")]
        [Required(ErrorMessage = "Kategori puanını yazınız...")]
        public int SoruKategorilerPuan { get; set; }

        [Required(ErrorMessage = "Kategori derecesini seçiniz...")]
        public int DereceId { get; set; }

        [Required(ErrorMessage = "Kategori derecesini seçiniz...")]
        public string DereceAdi { get; set; }
        public DerecelerVM DerecelerVm { get; set; }
        public string KullaniciId { get; set; }
        public KullaniciVM KullaniciVm { get; set; }
        public List<SoruKategori> SoruKategoris { get; set; }
    }
}
