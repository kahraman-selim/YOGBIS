using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Common.VModels
{
    public class SoruKategorilerVM
    {
        [Key]
        [Required(ErrorMessage = "Kategori numarasını yazınız...")]
        public int SoruKategoriId { get; set; }
        [Required(ErrorMessage = "Kategori adını yazınız...")]
        public string SoruKategoriAdi { get; set; }

        [Required(ErrorMessage = "Kategori kullanımını seçiniz...")]
        public string SoruKategoriKullanimi { get; set; }

        [Range(0, 25, ErrorMessage = "25 den fazla bir sayı giremezsiniz !")]
        [Required(ErrorMessage = "Kategori puanını yazınız...")]
        public int SoruKategoriPuan { get; set; }

        [Required(ErrorMessage = "Kategori derecesini seçiniz...")]
        public string SoruKategoriDerece { get; set; }
        public List<SoruKategori> SoruKategoris { get; set; }
    }
}
