using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Common.VModels
{
    public class SoruBankasiVM:BaseVM
    {
        [Key]
        public int SoruBankasiId { get; set; }
        [Required(ErrorMessage = "Kategoriyi seçiniz...")]
        public int SoruKategoriId { get; set; }
        public string SoruKategorilerAdi { get; set; }
        public SoruKategorilerVM SoruKategorilerVm { get; set; }
        [Required(ErrorMessage = "Soruyu yazınız...")]
        public string Soru { get; set; }
        [Required(ErrorMessage = "Cevabı yazınız...")]
        public string Cevap { get; set; }

        [Required(ErrorMessage = "Derecesini seçiniz...")]
        public int Id { get; set; }
        public DerecelerVM DerecelerVm { get; set; }
        public int SorulmaSayisi { get; set; } = 0;

        [Required(ErrorMessage = "Soru durumunu seçiniz...")]
        public bool SoruDurumu { get; set; } = true;
        public string KaydedenId { get; set; }
        public KullaniciVM Kaydeden { get; set; }
        public string OnaylayanId { get; set; }  
        public KullaniciVM Onaylayan { get; set; }
        public int OnayDurumu { get; set; } = 1;
        public string OnayAciklama { get; set; }
        public List<SoruKategori> SoruKategoris { get; set; }
    }
}
