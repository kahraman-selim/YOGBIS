using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Common.VModels
{
    public class MulakatSorulariVM
    {
        [Key]
        public int MulakatSorulariId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Girilen değer bir sayı değil !")]
        [Required(ErrorMessage = "Soru Sıra Numarası yazınız...")]
        public int SoruSiraNo { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Girilen değer bir sayı değil !")]
        [Required(ErrorMessage = "Soru Numarası yazınız...")]
        public int SoruNo { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Girilen değer bir sayı değil !")]
        [Required(ErrorMessage = "Kategori Numarasını yazınız...")]
        public int SoruKategoriId { get; set; }

        [Required(ErrorMessage = "Kategori Adını yazınız...")]
        public string SoruKategoriAdi { get; set; }
        [Required(ErrorMessage = "Soru Derecesini seçiniz...")]
        public string Derecesi { get; set; }
        [Required(ErrorMessage = "Soruyu yazınız...")]
        public string Soru { get; set; }
        [Required(ErrorMessage = "Cevabı yazınız...")]
        public string Cevap { get; set; }
        public int MulakatId { get; set; }
        public Mulakatlar Mulakatlar { get; set; }
    }
}
