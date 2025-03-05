using System;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class SoruKategoriVM : BaseVM
    {
        public Guid SoruId { get; set; }
        
        [Display(Name = "Soru")]
        public SoruBankasiVM SoruBankasi { get; set; }
        
        public Guid KategoriId { get; set; }
        
        [Display(Name = "Soru Kategorisi")]
        public SoruKategorilerVM SoruKategoriler { get; set; }

        [Required(ErrorMessage = "Kaydeden kişi zorunludur")]
        public string KaydedenId { get; set; }

        [Display(Name = "Kaydeden")]
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
    }
}
