using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Common.VModels
{
    public class AktivitelerVM : BaseVM
    {
        [Key]
        public int AktiviteId { get; set; }
        [Required(ErrorMessage = "Etkinlik için bir ad gereklidir")]
        public string AktiviteAdi { get; set; }
        
        [Required(ErrorMessage ="Okulunuzu seçiniz")]
        public int OkulId { get; set; }

        [Required(ErrorMessage = "Okulunuzu seçiniz")]
        public string OkulAdi { get; set; }
        public OkullarVM Okullar { get; set; }
        [Required(ErrorMessage ="Başlama Tarihini belirtiniz")]
        public DateTime BasTarihi { get; set; }

        [Required(ErrorMessage = "Bitiş Tarihini belirtiniz")]
        public DateTime BitTarihi { get; set; }

        [Required(ErrorMessage = "Etkinlik hakkında bilgi yazınız")]
        public string AktiviteBilgi { get; set; }

        [Required(ErrorMessage = "Katılımcı sayısını belirtiniz")]
        public int KatilimciSayisi { get; set; }

        [Required(ErrorMessage = "Etkinliğin sorumlusunu belirtiniz")]
        public string DuzenleyenAdiSoyadi { get; set; }
        public IFormFile Resim1 { get; set; }
        public string Resim1Yol { get; set; }
        public IFormFile Resim2 { get; set; }
        public string Resim2Yol { get; set; }
        public IFormFile Resim3 { get; set; }
        public string Resim3Yol { get; set; }
        public string KaydedenId { get; set; }
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
    }
}
