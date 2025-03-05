using System;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class IlcelerVM : BaseVM
    {
        public Guid IlceId { get; set; }

        [Required(ErrorMessage = "İlçe adı zorunludur")]
        [Display(Name = "İlçe Adı")]
        public string IlceAdi { get; set; }

        [Required(ErrorMessage = "İl zorunludur")]
        public Guid IlId { get; set; }

        [Display(Name = "İl")]
        public IllerVM Iller { get; set; }

        [Required(ErrorMessage = "Kaydeden kişi zorunludur")]
        public string KaydedenId { get; set; }

        [Display(Name = "Kaydeden")]
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
    }
}
