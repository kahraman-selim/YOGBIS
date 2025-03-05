using System;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class IllerMdEPostaVM : BaseVM
    {
        public Guid IllerMdEPostaId { get; set; }

        [Required(ErrorMessage = "İl Milli Eğitim e-posta adresi zorunludur")]
        [Display(Name = "İl MEM E-posta")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
        public string EPostaAdresi { get; set; }

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
