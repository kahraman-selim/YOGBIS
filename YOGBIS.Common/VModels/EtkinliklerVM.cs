using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Common.VModels
{
    public class EtkinliklerVM:BaseVM
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid EtkinlikId { get; set; }

        [Required(ErrorMessage = "Etkinlik için bir ad yazmalısınız !")]
        public string EtkinlikAdi { get; set; }

        [Required(ErrorMessage = "Başlama tarihini belirtmelisiniz !")]
        public DateTime BasTarihi { get; set; }

        [Required(ErrorMessage = "Bitiş tarihini belirtmelisiniz !")]
        public DateTime BitTarihi { get; set; }

        [Required(ErrorMessage = "Etkinlik için bilgi yazmalısınız !")]
        public string EtkinlikBilgi { get; set; }

        [Required(ErrorMessage = "Hedef kitleyi belirtin !")]
        public string HedefKitle { get; set; }

        [Required(ErrorMessage = "Katılımcı sayısı zorunludur !")]
        public int KatilimciSayisi { get; set; }

        [Required(ErrorMessage = "Sonuç hakkında bilgi yazmalısınız !")]
        public string Sonuc { get; set; }

        [Required(ErrorMessage = "Etkinlik sorumlusunu belirtiniz !")]
        public string DuzenleyenAdiSoyadi { get; set; }
        public IFormFile EtkinlikKapakResim { get; set; }
        public string EtkinlikKapakResimUrl { get; set; }
        public string EtkinlikKapakAdi { get; set; }
        public string OkulAdi { get; set; }
        public Guid? OkulId { get; set; }
        public string TemsilcilikAdi { get; set; }
        public Guid? TemsilcilikId { get; set; }
        public string UlkeAdi { get; set; }
        public Guid? UlkeId { get; set; }
        public string KaydedenId { get; set; }
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
        public IFormFileCollection DosyaGaleris { get; set; }
        public List<DosyaGaleriVM> DosyaGaleri { get; set; }
        public IFormFileCollection FotoGaleris { get; set; }
        public List<FotoGaleriVM> FotoGaleri { get; set; }
    }
}
