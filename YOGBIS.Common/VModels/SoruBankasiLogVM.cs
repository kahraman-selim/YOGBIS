using System;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class SoruBankasiLogVM : BaseVM
    {
        public Guid SoruBankasiLogId { get; set; }

        [Required(ErrorMessage = "Soru zorunludur")]
        [Display(Name = "Soru")]
        public string Soru { get; set; }

        [Required(ErrorMessage = "Cevap zorunludur")]
        [Display(Name = "Cevap")]
        public string Cevap { get; set; }

        [Display(Name = "Sorulma Sayısı")]
        public int SorulmaSayisi { get; set; }

        [Display(Name = "Soru Durumu")]
        public bool SoruDurumu { get; set; }

        [Display(Name = "Kayıt Türü")]
        public int KayitTuru { get; set; }

        [Required(ErrorMessage = "Soru bankası seçimi zorunludur")]
        public Guid SoruBankasiId { get; set; }

        [Display(Name = "Soru Bankası")]
        public SoruBankasiVM SoruBankasi { get; set; }

        [Required(ErrorMessage = "Derece seçimi zorunludur")]
        public Guid DereceId { get; set; }

        [Display(Name = "Soru Derecesi")]
        public SoruDerecelerVM SoruDerece { get; set; }

        [Required(ErrorMessage = "Kategori seçimi zorunludur")]
        public Guid SoruKategoriId { get; set; }

        [Display(Name = "Soru Kategorisi")]
        public SoruKategorilerVM SoruKategori { get; set; }

        [Required(ErrorMessage = "Kaydeden kişi zorunludur")]
        public string KaydedenId { get; set; }

        [Display(Name = "Kaydeden")]
        public string KaydedenAdi { get; set; }

        [Display(Name = "Kaydeden")]
        public KullaniciVM Kaydeden { get; set; }
    }
}
