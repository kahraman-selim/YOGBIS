using System;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class AdayDDKVM : BaseVM
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Aday seçimi zorunludur")]
        public Guid AdayId { get; set; }

        [Display(Name = "Aday")]
        public AdaylarVM Adaylar { get; set; }

        [Required(ErrorMessage = "TC Kimlik No zorunludur")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "TC Kimlik No 11 haneli olmalıdır")]
        [Display(Name = "TC Kimlik No")]
        public string TC { get; set; }

        [Required(ErrorMessage = "Kaydeden kişi zorunludur")]
        public string KaydedenId { get; set; }

        public string KaydedenAdi { get; set; }

        [Display(Name = "Kaydeden")]
        public KullaniciVM Kullanici { get; set; }
    }
}
