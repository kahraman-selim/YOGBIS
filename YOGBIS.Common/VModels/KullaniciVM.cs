using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YOGBIS.Common.VModels
{
    public class KullaniciVM
    {
        public string Id { get; set; }
        [Display(Name ="Kullanıcı Adı")]
        public string UserName { get; set; }
        [Display(Name = "E-Posta Adresi")]
        public string EMail { get; set; }
        [Display(Name = "Telefon Numarası")]
        public string PhoneNumber { get; set; }
        [StringLength(11,ErrorMessage ="TC Kimlik Numaranızı kontrol ediniz")]
        public string TcKimlikNo { get; set; }
        [Required (ErrorMessage ="{0} boş geçilemez")]
        public string Ad { get; set; }
        [Required(ErrorMessage = "{0} boş geçilemez")]
        public string Soyad { get; set; }

        public string AdSoyad { get; set; }

        [DisplayFormat(DataFormatString = "{0,d}")]
        [DataType(DataType.DateTime)]
        public DateTime KayitTarihi { get; set; } = DateTime.Now;
        public int KulaniciAdDegLimiti { get; set; } = 10;        
        public byte[] KullaniciResim { get; set; }
        public bool? Aktif { get; set; }
    }
}
