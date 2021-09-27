using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Common.VModels
{
    public class SoruBankasiVM
    {
        [Key]
        [Range(0, int.MaxValue, ErrorMessage = "Girilen sayı uygun değil !")]
        [Required(ErrorMessage = "Soru numarasını yazınız...")]
        public int SoruBankasiId { get; set; }

        [Required(ErrorMessage = "Kategoriyi seçiniz...")]
        public int SoruKategoriId { get; set; }
        [Required(ErrorMessage = "Soruyu yazınız...")]
        public string Soru { get; set; }
        [Required(ErrorMessage = "Cevabı yazınız...")]
        public string Cevap { get; set; }
        [Required(ErrorMessage = "Derecesini seçiniz...")]
        public string Derecesi { get; set; }        
        public int SorulmaSayisi { get; set; }
        [Required(ErrorMessage = "Soru durumunu seçiniz...")]
        public string SoruDurumu { get; set; }
        public List<SoruKategori> SoruKategoris { get; set; }
    }
}
