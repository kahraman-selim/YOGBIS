using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
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
        public IFormFile EtkinlikKapakResim { get; set; }
        public string EtkinlikKapakResimUrl { get; set; }
        public IFormFile EtkinlikDosya { get; set; }
        public string EtkinlikDosyaUrl { get; set; }               
        public string KaydedenId { get; set; }
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
        public IFormFileCollection GaleriDosyalar { get; set; }
        public List<FotoGaleriVM> FotoGaleri { get; set; }
    }
}
