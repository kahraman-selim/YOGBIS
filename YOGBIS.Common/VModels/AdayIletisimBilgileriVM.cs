using System;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class AdayIletisimBilgileriVM : BaseVM
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "TC Kimlik No zorunludur")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "TC Kimlik No 11 haneli olmalıdır")]
        [Display(Name = "TC Kimlik No")]
        public string TC { get; set; }

        [Required(ErrorMessage = "Cep telefonu numarası zorunludur")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz")]
        [Display(Name = "Cep Telefonu")]
        public string CepTelNo { get; set; }

        [Required(ErrorMessage = "E-posta adresi zorunludur")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
        [Display(Name = "E-posta")]
        public string EPosta { get; set; }

        [Required(ErrorMessage = "Nüfusa kayıtlı il zorunludur")]
        [Display(Name = "Nüfusa Kayıtlı İl")]
        public string NufusIl { get; set; }

        [Required(ErrorMessage = "Nüfusa kayıtlı ilçe zorunludur")]
        [Display(Name = "Nüfusa Kayıtlı İlçe")]
        public string NufusIlce { get; set; }

        [Required(ErrorMessage = "İkamet adresi zorunludur")]
        [Display(Name = "İkamet Adresi")]
        public string IkametAdres { get; set; }

        [Required(ErrorMessage = "İkamet ili zorunludur")]
        [Display(Name = "İkamet İli")]
        public string IkametIl { get; set; }

        [Required(ErrorMessage = "İkamet ilçesi zorunludur")]
        [Display(Name = "İkamet İlçesi")]
        public string IkametIlce { get; set; }

        public Guid? MulakatId { get; set; }

        [Display(Name = "Mülakat")]
        public MulakatlarVM Mulakat { get; set; }

        [Display(Name = "Mülakat Yılı")]
        public int MulakatYil { get; set; }

        [Required(ErrorMessage = "Kaydeden kişi zorunludur")]
        public string KaydedenId { get; set; }

        [Display(Name = "Kaydeden")]
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
    }
}
