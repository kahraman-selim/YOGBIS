using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class EtkinliklerVM : BaseVM
    {
        public Guid EtkinlikId { get; set; }

        [Required(ErrorMessage = "Etkinlik adı zorunludur")]
        [StringLength(200, ErrorMessage = "Etkinlik adı en fazla 200 karakter olabilir")]
        [Display(Name = "Etkinlik Adı")]
        public string EtkinlikAdi { get; set; }

        [Required(ErrorMessage = "Başlama tarihi zorunludur")]
        [Display(Name = "Başlama Tarihi")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BasTarihi { get; set; }

        [Required(ErrorMessage = "Bitiş tarihi zorunludur")]
        [Display(Name = "Bitiş Tarihi")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BitTarihi { get; set; }

        [Required(ErrorMessage = "Etkinlik bilgisi zorunludur")]
        [Display(Name = "Etkinlik Bilgisi")]
        public string EtkinlikBilgi { get; set; }

        [Required(ErrorMessage = "Hedef kitle zorunludur")]
        [Display(Name = "Hedef Kitle")]
        public string HedefKitle { get; set; }

        [Required(ErrorMessage = "Katılımcı sayısı zorunludur")]
        [Display(Name = "Katılımcı Sayısı")]
        [Range(1, int.MaxValue, ErrorMessage = "Katılımcı sayısı 0'dan büyük olmalıdır")]
        public int KatilimciSayisi { get; set; }

        [Required(ErrorMessage = "Sonuç bilgisi zorunludur")]
        [Display(Name = "Sonuç")]
        public string Sonuc { get; set; }

        [Required(ErrorMessage = "Düzenleyen adı soyadı zorunludur")]
        [Display(Name = "Düzenleyen Adı Soyadı")]
        public string DuzenleyenAdiSoyadi { get; set; }

        [Display(Name = "Etkinlik Kapak Resmi")]
        public IFormFile EtkinlikKapakResim { get; set; }

        [Display(Name = "Etkinlik Kapak Resim URL")]
        public string EtkinlikKapakResimUrl { get; set; }

        [Display(Name = "Etkinlik Kapak Adı")]
        public string EtkinlikKapakAdi { get; set; }

        [Display(Name = "Okul Adı")]
        public string OkulAdi { get; set; }

        public Guid? OkulId { get; set; }

        [Display(Name = "Temsilcilik Adı")]
        public string TemsilcilikAdi { get; set; }

        public Guid? TemsilcilikId { get; set; }

        [Display(Name = "Ülke Adı")]
        public string UlkeAdi { get; set; }

        public Guid? UlkeId { get; set; }

        [Required(ErrorMessage = "Kaydeden kişi zorunludur")]
        public string KaydedenId { get; set; }

        [Display(Name = "Kaydeden")]
        public string KaydedenAdi { get; set; }

        public KullaniciVM Kullanici { get; set; }

        [Display(Name = "Dosyalar")]
        public IFormFileCollection DosyaGaleris { get; set; }

        [Display(Name = "Dosya Galerisi")]
        public virtual List<DosyaGaleriVM> DosyaGaleri { get; set; }

        [Display(Name = "Fotoğraflar")]
        public IFormFileCollection FotoGaleris { get; set; }

        [Display(Name = "Fotoğraf Galerisi")]
        public virtual List<FotoGaleriVM> FotoGaleri { get; set; }
    }
}
