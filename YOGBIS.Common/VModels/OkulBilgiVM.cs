using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Common.VModels
{
    public class OkulBilgiVM:BaseVM
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid OkulBilgiId { get; set; }

        [Required(ErrorMessage = "Telefon zorunlu bir alandır")]
        public string OkulTelefon { get; set; }
        
        [Required(ErrorMessage = "Okul adresi zorunlu bir alandır")]
        public string OkulAdres { get; set; }

        //**************************************

        [Required(ErrorMessage = "Müdür adı ve soyadı zorunlu bir alandır")]
        public string MudurAdiSoyadi { get; set; }

        [Required(ErrorMessage = "Müdür telefonu zorunlu bir alandır")]
        public string MudurTelefon { get; set; }

        [EmailAddress(ErrorMessage ="Girilen bilgi E-Posta adresi değil")]
        [Required(ErrorMessage = "Müdür E-Posta zorunlu bir alandır")]
        public string MudurEPosta { get; set; }

        [Required(ErrorMessage = "Dönüş Yılı zorunlu bir alandır")]
        public string MudurDonusYil { get; set; }

        //******************************************************        
        
        public string MdYrdAdiSoyadi { get; set; }        
        public string MdYrdTelefon { get; set; }

        [EmailAddress(ErrorMessage = "Girilen bilgi E-Posta adresi değil")]        
        public string MdYrdEPosta { get; set; }       
        public string MdYrdDonusYil { get; set; }

        //******************************************************

        [Required(ErrorMessage = "Okul adı zorunlu bir alandır")]
        public Guid OkulId { get; set; }

        [Required(ErrorMessage = "Okul adı zorunlu bir alandır")]
        public string OkulAdi { get; set; }
        public OkullarVM Okullar { get; set; }

        [Required(ErrorMessage = "Bulunduğunuz ülkeyi seçiniz")]
        public Guid UlkeId { get; set; }

        [Required(ErrorMessage = "Bulunduğunuz ülkeyi seçiniz")]
        public string UlkeAdi { get; set; }
        public UlkelerVM Ulkeler { get; set; }
        public string KullaniciId { get; set; }
        public string KullaniciAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
    }
}
