using System;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class IkametAdresleriVM : BaseVM
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "İkamet adresi zorunludur")]
        [StringLength(500, ErrorMessage = "İkamet adresi en fazla 500 karakter olabilir")]
        [Display(Name = "İkamet Adresi")]
        public string IkametAdresi { get; set; }

        [Required(ErrorMessage = "Kaydeden kişi zorunludur")]
        public string KaydedenId { get; set; }

        [Display(Name = "Kaydeden")]
        public KullaniciVM Kullanici { get; set; }
    }
}
