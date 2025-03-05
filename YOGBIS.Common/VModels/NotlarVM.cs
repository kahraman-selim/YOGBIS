using System;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class NotlarVM : BaseVM
    {
        public Guid NotId { get; set; }

        [Required(ErrorMessage = "Not başlığı zorunludur")]
        [StringLength(100, ErrorMessage = "Not başlığı en fazla 100 karakter olabilir")]
        [Display(Name = "Not Başlığı")]
        public string NotAdi { get; set; }

        [Display(Name = "Not Detayı")]
        public string NotDetay { get; set; }

        [Required(ErrorMessage = "Başlangıç tarihi zorunludur")]
        [Display(Name = "Başlangıç Tarihi")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BaTarihi { get; set; }

        [Required(ErrorMessage = "Bitiş tarihi zorunludur")]
        [Display(Name = "Bitiş Tarihi")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BiTarihi { get; set; }

        [Display(Name = "Not Rengi")]
        public string NotRenk { get; set; }

        [Required(ErrorMessage = "Kaydeden kişi zorunludur")]
        public string KaydedenId { get; set; }

        [Display(Name = "Kaydeden")]
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
    }
}
