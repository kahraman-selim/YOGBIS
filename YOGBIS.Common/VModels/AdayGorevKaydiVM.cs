using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class AdayGorevKaydiVM : BaseVM
    {
        public Guid AdayGorevKaydiId { get; set; }

        [Required(ErrorMessage = "TC Kimlik No zorunludur")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "TC Kimlik No 11 haneli olmalıdır")]
        [Display(Name = "TC Kimlik No")]
        public string GorevliTC { get; set; }

        [Required(ErrorMessage = "Derece seçimi zorunludur")]
        public Guid DereceId { get; set; }

        [Display(Name = "Derece")]
        public SoruDerecelerVM SoruDereceler { get; set; }

        [Required(ErrorMessage = "Görev bilgisi zorunludur")]
        [Display(Name = "Görevi")]
        public string Gorevi { get; set; }

        [Required(ErrorMessage = "Branş seçimi zorunludur")]
        public Guid BransId { get; set; }

        [Display(Name = "Branş")]
        public BranslarVM Branslar { get; set; }

        [Display(Name = "Görev Onay Sayısı")]
        public string GorevOnaySayi { get; set; }

        [Display(Name = "Görev Onay Tarihi")]
        public DateTime GorevlOnayTarihi { get; set; }

        [Display(Name = "Görevlendirme Tarihi")]
        public DateTime? GorevlendirilmeTarihi { get; set; }

        [Display(Name = "Görev Başlangıç Tarihi")]
        public DateTime? GorevBasTarihi { get; set; }

        [Display(Name = "Görev Bitiş Tarihi")]
        public DateTime? GorevBitisTarihi { get; set; }

        [Display(Name = "Görev Durumu")]
        public int GorevDurumu { get; set; }

        [Display(Name = "Görev Açıklaması")]
        public string GorevAciklama { get; set; }

        public Guid? OkulId { get; set; }

        [Display(Name = "Okul")]
        public OkullarVM Okul { get; set; }

        public Guid? UniId { get; set; }

        [Display(Name = "Üniversite")]
        public UniversitelerVM Universite { get; set; }

        public Guid? TemsilcilikId { get; set; }

        [Display(Name = "Temsilcilik")]
        public TemsilciliklerVM Temsilcilik { get; set; }

        public Guid? SehirId { get; set; }

        [Display(Name = "Şehir")]
        public SehirlerVM Sehir { get; set; }

        public Guid? EyaletId { get; set; }

        [Display(Name = "Eyalet")]
        public EyaletlerVM Eyalet { get; set; }

        [Required(ErrorMessage = "Ülke seçimi zorunludur")]
        public Guid UlkeId { get; set; }

        [Display(Name = "Ülke")]
        public UlkelerVM Ulkeler { get; set; }

        [Required(ErrorMessage = "Kaydeden kişi zorunludur")]
        public string KaydedenId { get; set; }

        [Display(Name = "Kaydeden")]
        public string KaydedenAdi { get; set; }

        public KullaniciVM Kullanici { get; set; }

        [Display(Name = "E-posta Adresleri")]
        public virtual ICollection<EPostaAdresleriVM> EpostaAdresleri { get; set; }

        [Display(Name = "Telefon Numaraları")]
        public virtual ICollection<TelefonlarVM> Telefonlar { get; set; }

        [Display(Name = "Karar PDF URL")]
        public string KararPdfUrl { get; set; }

        [Display(Name = "Görev Karar PDF Dosyaları")]
        public IFormFileCollection GorevKararPdfGaleris { get; set; }

        [Display(Name = "Görev Karar PDF Galerisi")]
        public virtual ICollection<GorevKararPdfGaleriVM> GorevKararPdfGaleri { get; set; }

        [Display(Name = "Fotoğraf Dosyaları")]
        public IFormFileCollection FotoGaleris { get; set; }

        [Display(Name = "Fotoğraf Galerisi")]
        public List<FotoGaleriVM> FotoGaleri { get; set; }
    }
}
