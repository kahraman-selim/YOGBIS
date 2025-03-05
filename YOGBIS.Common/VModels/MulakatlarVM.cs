using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class MulakatlarVM : BaseVM
    {
        public Guid MulakatId { get; set; }

        [Required(ErrorMessage = "Onay sayısı zorunludur")]
        [Display(Name = "Onay Sayısı")]
        public string OnaySayisi { get; set; }

        [Required(ErrorMessage = "Onay tarihi zorunludur")]
        [Display(Name = "Onay Tarihi")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime OnayTarihi { get; set; }

        [Required(ErrorMessage = "Karar sayısı zorunludur")]
        [Display(Name = "Karar Sayısı")]
        public string KararSayisi { get; set; }

        [Required(ErrorMessage = "Karar tarihi zorunludur")]
        [Display(Name = "Karar Tarihi")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime KararTarihi { get; set; }

        [Required(ErrorMessage = "Yazılı sınav tarihi zorunludur")]
        [Display(Name = "Yazılı Sınav Tarihi")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime YazılıSinavTarihi { get; set; }

        [Required(ErrorMessage = "Mülakat kategori ID zorunludur")]
        [Display(Name = "Mülakat Kategori")]
        public int MulakatKategoriId { get; set; }

        [Required(ErrorMessage = "Mülakat adı zorunludur")]
        [StringLength(100, ErrorMessage = "Mülakat adı en fazla 100 karakter olabilir")]
        [Display(Name = "Mülakat Adı")]
        public string MulakatAdi { get; set; }

        [Required(ErrorMessage = "Mülakat dönemi zorunludur")]
        [Display(Name = "Mülakat Dönemi")]
        public string MulakatDonemi { get; set; }

        [Required(ErrorMessage = "Mülakat yılı zorunludur")]
        [Display(Name = "Mülakat Yılı")]
        [Range(2000, 2100, ErrorMessage = "Geçerli bir yıl giriniz")]
        public int MulakatYil { get; set; }

        [Required(ErrorMessage = "Başlama tarihi zorunludur")]
        [Display(Name = "Başlama Tarihi")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BaslamaTarihi { get; set; }

        [Required(ErrorMessage = "Bitiş tarihi zorunludur")]
        [Display(Name = "Bitiş Tarihi")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BitisTarihi { get; set; }

        [Display(Name = "Aday Sayısı")]
        [Range(0, int.MaxValue, ErrorMessage = "Aday sayısı 0'dan küçük olamaz")]
        public int? AdaySayisi { get; set; } = 0;

        [Required(ErrorMessage = "Sorulan soru sayısı zorunludur")]
        [Display(Name = "Sorulan Soru Sayısı")]
        [Range(1, int.MaxValue, ErrorMessage = "Sorulan soru sayısı en az 1 olmalıdır")]
        public int SorulanSoruSayisi { get; set; } = 0;

        [Required(ErrorMessage = "Durum zorunludur")]
        [Display(Name = "Durum")]
        public bool? Durumu { get; set; } = true;

        [Display(Name = "Mülakat Açıklaması")]
        public string MulakatAciklama { get; set; }

        [Required(ErrorMessage = "Kaydeden kişi zorunludur")]
        public string KaydedenId { get; set; }

        [Display(Name = "Kaydeden")]
        public string KaydedenAdi { get; set; }

        public KullaniciVM Kullanici { get; set; }

        [Display(Name = "Mülakat Soruları")]
        public virtual ICollection<MulakatSorulariVM> MulakatSorularis { get; set; }

        [Display(Name = "Aday Başvuru Bilgileri")]
        public virtual ICollection<AdayBasvuruBilgileriVM> AdayBasvuruBilgileris { get; set; }
    }
}
