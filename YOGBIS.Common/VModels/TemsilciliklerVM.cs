using System;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class TemsilciliklerVM : BaseVM
    {
        public Guid TemsilcilikId { get; set; }

        [Required(ErrorMessage = "Temsilcilik adı zorunludur")]
        [Display(Name = "Temsilcilik Adı")]
        public string TemsilcilikAdi { get; set; }

        [Display(Name = "Temsilcilik Açıklaması")]
        public string TemsilcilikAciklama { get; set; }

        [Display(Name = "Temsilcilik Durumu")]
        public bool? Durumu { get; set; }

        [Required(ErrorMessage = "Şehir zorunludur")]
        public Guid SehirId { get; set; }

        [Display(Name = "Şehir")]
        public SehirlerVM Sehirler { get; set; }

        [Required(ErrorMessage = "Kaydeden kişi zorunludur")]
        public string KaydedenId { get; set; }

        [Display(Name = "Kaydeden")]
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
    }
}
