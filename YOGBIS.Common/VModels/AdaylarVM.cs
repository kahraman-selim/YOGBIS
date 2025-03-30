using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Common.VModels
{
    public class AdaylarVM : BaseVM
    {
        public Guid AdayId { get; set; }

        [Required(ErrorMessage = "TC Kimlik No zorunludur")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "TC Kimlik No 11 haneli olmalıdır")]
        [Display(Name = "TC Kimlik No")]
        public string TC { get; set; }

        [Required(ErrorMessage = "Ad zorunludur")]
        [StringLength(50, ErrorMessage = "Ad en fazla 50 karakter olabilir")]
        [Display(Name = "Ad")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Soyad zorunludur")]
        [StringLength(50, ErrorMessage = "Soyad en fazla 50 karakter olabilir")]
        [Display(Name = "Soyad")]
        public string Soyad { get; set; }

        [Required(ErrorMessage = "Baba adı zorunludur")]
        [StringLength(50, ErrorMessage = "Baba adı en fazla 50 karakter olabilir")]
        [Display(Name = "Baba Adı")]
        public string BabaAd { get; set; }

        [Required(ErrorMessage = "Ana adı zorunludur")]
        [StringLength(50, ErrorMessage = "Ana adı en fazla 50 karakter olabilir")]
        [Display(Name = "Ana Adı")]
        public string AnaAd { get; set; }

        [Required(ErrorMessage = "Doğum tarihi zorunludur")]
        [Display(Name = "Doğum Tarihi")]
        public string DogumTarihi { get; set; }

        [Display(Name = "Doğum Tarihi (Alternatif)")]
        public string DogumTarihi2 { get; set; }

        [Display(Name = "Yaş")]
        public int? Yasi { get; set; }

        [Required(ErrorMessage = "Doğum yeri zorunludur")]
        [Display(Name = "Doğum Yeri")]
        public string DogumYeri { get; set; }

        [Required(ErrorMessage = "Cinsiyet zorunludur")]
        [Display(Name = "Cinsiyet")]
        public string Cinsiyet { get; set; }

        public int MulakatYil { get; set; }

        public Guid? MulakatId { get; set; }
        public virtual MulakatlarVM Mulakatlar { get; set; }

        [Required(ErrorMessage = "Kaydeden kişi zorunludur")]
        public string KaydedenId { get; set; }

        [Display(Name = "Kaydeden")]
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }

        [Display(Name = "Fotoğraf Dosyaları")]
        public IFormFileCollection FotoGaleris { get; set; }

        [Display(Name = "Fotoğraf Galerisi")]
        public virtual List<FotoGaleriVM> FotoGaleri { get; set; }

        [Display(Name = "Dosyalar")]
        public IFormFileCollection DosyaGaleris { get; set; }

        [Display(Name = "Dosya Galerisi")]
        public virtual ICollection<DosyaGaleriVM> DosyaGaleri { get; set; }

        [Display(Name = "DDK Bilgileri")]
        public virtual ICollection<AdayDDKVM> AdayDDK { get; set; }

        [Display(Name = "Görev Kayıtları")]
        public virtual ICollection<AdayGorevKaydiVM> AdayGorevKaydi { get; set; }

        [Display(Name = "Sınav Notlar")]
        public virtual ICollection<AdaySinavNotlarVM> AdaySinavNotlar { get; set; }

        [Display(Name = "E-posta Adresleri")]
        public virtual ICollection<EPostaAdresleriVM> EpostaAdresleri { get; set; }

        [Display(Name = "Telefon Numaraları")]
        public virtual ICollection<TelefonlarVM> Telefonlar { get; set; }

        [Display(Name = "İkamet Adresleri")]
        public virtual ICollection<IkametAdresleriVM> IkametAdresleri { get; set; }

        [Display(Name = "Başvuru Bilgileri")]
        public virtual ICollection<AdayBasvuruBilgileriVM> AdayBasvuruBilgileri { get; set; }

        [Display(Name = "İletişim Bilgileri")]
        public virtual ICollection<AdayIletisimBilgileriVM> AdayIletisimBilgileri { get; set; }

        [Display(Name = "MYSS Bilgileri")]
        public virtual ICollection<AdayMYSSVM> AdayMYSS { get; set; }

        [Display(Name = "TYS Bilgileri")]
        public virtual ICollection<AdayTYSVM> AdayTYS { get; set; }
    }
}
