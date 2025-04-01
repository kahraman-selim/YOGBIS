using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class KullaniciVM
    {
        public string Id { get; set; }
        public string RolId { get; set; }
        [Required(ErrorMessage = "Kullanıcı adı zorunludur")]
        [StringLength(50, ErrorMessage = "Kullanıcı adı en fazla 50 karakter olabilir")]
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "E-posta adresi zorunludur")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
        [Display(Name = "E-Posta Adresi")]
        public string EMail { get; set; }

        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz")]
        [Display(Name = "Telefon Numarası")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "TC Kimlik No zorunludur")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "TC Kimlik No 11 haneli olmalıdır")]
        [Display(Name = "TC Kimlik No")]
        public string TcKimlikNo { get; set; }

        [Required(ErrorMessage = "Ad zorunludur")]
        [StringLength(50, ErrorMessage = "Ad en fazla 50 karakter olabilir")]
        [Display(Name = "Ad")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Soyad zorunludur")]
        [StringLength(50, ErrorMessage = "Soyad en fazla 50 karakter olabilir")]
        [Display(Name = "Soyad")]
        public string Soyad { get; set; }

        [Display(Name = "Ad Soyad")]
        public string AdSoyad { get; set; }
        public string DisplayText => $"{AdSoyad} - (Rol ID: {RolId})";

        [Required(ErrorMessage = "Kayıt tarihi zorunludur")]
        [Display(Name = "Kayıt Tarihi")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime KayitTarihi { get; set; } = DateTime.UtcNow;

        [Display(Name = "Kullanıcı Ad Değiştirme Limiti")]
        public int KulaniciAdDegLimiti { get; set; } = 10;

        [Display(Name = "Kullanıcı Resmi")]
        public byte[] KullaniciResim { get; set; }

        [Display(Name = "Kullanıcı Resim Yolu")]
        public string KullaniciResimYol { get; set; }

        [Display(Name = "Kullanıcı Resmi")]
        public IFormFile KullaniciResimIFromFile { get; set; }

        [Display(Name = "Aktif")]
        public bool? Aktif { get; set; }

        [Display(Name = "Oturum Durumu")]
        public bool? OturumDurumu { get; set; }

        [Display(Name = "Kaydeden")]
        public string KaydedenAdi { get; set; }

        //Bağlı Tablolar
        #region Navigation Properties
        [Display(Name = "Aday Başvuru Bilgileri")]
        public virtual ICollection<AdayBasvuruBilgileriVM> AdayBasvuru { get; set; }

        [Display(Name = "Aday DDK Bilgileri")]
        public virtual ICollection<AdayDDKVM> AdayDDK { get; set; }

        [Display(Name = "Görev Kayıtları")]
        public virtual ICollection<AdayGorevKaydiVM> AdayGorevKaydi { get; set; }

        [Display(Name = "Aday İletişim Bilgileri")]
        public virtual ICollection<AdayIletisimBilgileriVM> AdayIletisimBilgileri { get; set; }

        [Display(Name = "Adaylar")]
        public virtual ICollection<AdaylarVM> Adaylars { get; set; }

        [Display(Name = "Aday MYS Bilgileri")]
        public virtual ICollection<AdayMYSSVM> AdayMYS { get; set; }

        [Display(Name = "Aday TYS Bilgileri")]
        public virtual ICollection<AdayTYSVM> AdayTYS { get; set; }

        [Display(Name = "Aday Sinav Not Bilgileri")]
        public virtual ICollection<AdaySinavNotlarVM> AdaySinavNotlar { get; set; }

        [Display(Name = "Branşlar")]
        public virtual ICollection<BranslarVM> Branslar { get; set; }

        [Display(Name = "Dosyalar")]
        public IFormFileCollection DosyaGaleris { get; set; }

        [Display(Name = "Dosya Galerisi")]
        public virtual ICollection<DosyaGaleriVM> DosyaGaleri { get; set; }

        [Display(Name = "Duyurular")]
        public virtual ICollection<DuyurularVM> Duyurular { get; set; }

        [Display(Name = "E-posta Adresleri")]
        public virtual ICollection<EPostaAdresleriVM> EPostaAdresleri { get; set; }

        [Display(Name = "Etkinlikler")]
        public virtual ICollection<EtkinliklerVM> Etkinlikler { get; set; }

        [Display(Name = "Eyaletler")]
        public virtual ICollection<EyaletlerVM> Eyaletler { get; set; }

        [Display(Name = "Fotoğraflar")]
        public IFormFileCollection FotoGaleris { get; set; }

        [Display(Name = "Fotoğraf Galerisi")]
        public virtual List<FotoGaleriVM> FotoGaleri { get; set; }

        [Display(Name = "Görev Karar PDF Dosyaları")]
        public IFormFileCollection GorevKararPdfGaleris { get; set; }

        [Display(Name = "Görev Karar PDF Galerisi")]
        public virtual ICollection<GorevKararPdfGaleriVM> GorevKararPdfGaleri { get; set; }

        [Display(Name = "İkamet Adresleri")]
        public virtual ICollection<IkametAdresleriVM> IkametAdresleri { get; set; }

        [Display(Name = "İlçeler")]
        public virtual ICollection<IlcelerVM> Ilceler { get; set; }

        [Display(Name = "İl MEM E-postaları")]
        public virtual ICollection<IllerMdEPostaVM> IllerMdEPosta { get; set; }

        [Display(Name = "İller")]
        public virtual ICollection<IllerVM> Iller { get; set; }

        [Display(Name = "Komisyonlar")]
        public virtual ICollection<KomisyonlarVM> Komisyonlar { get; set; }

        [Display(Name = "Mülakatlar")]
        public virtual ICollection<MulakatlarVM> Mulakatlar { get; set; }

        [Display(Name = "Mülakat Soruları")]
        public virtual ICollection<MulakatSorulariVM> MulakatSorulari { get; set; }

        [Display(Name = "Notlar")]
        public virtual ICollection<NotlarVM> Notlar { get; set; }

        [Display(Name = "Öğrenciler")]
        public virtual ICollection<OgrencilerVM> Ogrenciler { get; set; }

        [Display(Name = "Okul Bilgileri")]
        public virtual ICollection<OkulBilgiVM> OkulBilgi { get; set; }

        [Display(Name = "Okul Bina Bölümleri")]
        public virtual ICollection<OkulBinaBolumVM> OkulBinaBolum { get; set; }

        [Display(Name = "Okullar")]
        public virtual ICollection<OkullarVM> Okullar { get; set; }

        [Display(Name = "Personeller")]
        public virtual ICollection<PersonellerVM> Personeller { get; set; }

        [Display(Name = "Şehirler")]
        public virtual ICollection<SehirlerVM> Sehirler { get; set; }

        [Display(Name = "Şubeler")]
        public virtual ICollection<SubelerVM> Siniflar { get; set; }

        [Display(Name = "Soru Bankası Log")]
        public virtual ICollection<SoruBankasiLogVM> SoruBankasiLog { get; set; }

        [Display(Name = "Soru Bankası")]
        public virtual ICollection<SoruBankasiVM> SoruBankasi { get; set; }

        [Display(Name = "Soru Dereceleri")]
        public virtual ICollection<SoruDerecelerVM> SoruDereceler { get; set; }

        [Display(Name = "Soru Kategorileri")]
        public virtual ICollection<SoruKategorilerVM> SoruKategoriler { get; set; }

        [Display(Name = "Soru Onayları")]
        public virtual ICollection<SoruOnayVM> SoruOnay { get; set; }

        [Display(Name = "SSS Cevapları")]
        public virtual ICollection<SSSCevapVM> SSSCevap { get; set; }

        [Display(Name = "SSS")]
        public virtual ICollection<SSSVM> SSS { get; set; }

        [Display(Name = "Sınıflar")]
        public virtual ICollection<SiniflarVM> Subeler { get; set; }

        [Display(Name = "Telefonlar")]
        public virtual ICollection<TelefonlarVM> Telefonlar { get; set; }

        [Display(Name = "Temsilcilikler")]
        public virtual ICollection<TemsilciliklerVM> Temsilcilikler { get; set; }

        [Display(Name = "Ülke Grupları")]
        public virtual ICollection<UlkeGruplariVM> UlkeGruplari { get; set; }

        [Display(Name = "Ülkeler")]
        public virtual ICollection<UlkelerVM> Ulkeler { get; set; }

        [Display(Name = "Üniversiteler")]
        public virtual ICollection<UniversitelerVM> Universiteler { get; set; }
        #endregion
    }
}
