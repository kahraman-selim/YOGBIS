using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime KayitTarihi { get; set; } = DateTime.UtcNow;
        public int KulaniciAdDegLimiti { get; set; } = 10;
        [MaxLength]
        public byte[] KullaniciResim { get; set; }
        public string KullaniciResimYol { get; set; }
        public IFormFile KullaniciResimIFromFile { get; set; }
        public bool? Aktif { get; set; }

        #region BağlıTablolar
        public List<AdayDDKVM> AdayDDK { get; set; }
        public List<AdayGorevKaydiVM> AdayGorevKaydi { get; set; }
        public List<AdaylarVM> Adaylars { get; set; }
        public List<AdayNotVM> AdayNot { get; set; }
        public List<BranslarVM> Branslar { get; set; }
        public IFormFileCollection DosyaGaleris { get; set; }
        public List<DosyaGaleriVM> DosyaGaleri { get; set; }
        public List<DuyurularVM> Duyurular { get; set; }
        public List<EPostaAdresleriVM> EPostaAdresleri { get; set; }
        public List<EtkinliklerVM> Etkinlikler { get; set; }
        public List<EyaletlerVM> Eyaletler { get; set; }
        public IFormFileCollection FotoGaleris { get; set; }
        public List<FotoGaleriVM> FotoGaleri { get; set; }
        public IFormFileCollection GorevKararPdfGaleris { get; set; }
        public List<GorevKararPdfGaleriVM> GorevKararPdfGaleri { get; set; }
        public List<IkametAdresleriVM> IkametAdresleri { get; set; }
        public List<IlcelerVM> Ilceler { get; set; }
        public List<IllerMdEPostaVM> IllerMdEPosta { get; set; }
        public List<IllerVM> Iller { get; set; }
        public List<KomisyonlarVM> Komisyonlar { get; set; }
        public List<MulakatlarVM> Mulakatlar { get; set; }
        public List<MulakatSorulariVM> MulakatSorulari { get; set; }
        public List<NotlarVM> Notlar { get; set; }
        public List<OgrencilerVM> Ogrenciler { get; set; }
        public List<OkulBilgiVM> OkulBilgi { get; set; }
        public List<OkulBinaBolumVM> OkulBinaBolum { get; set; }
        public List<OkullarVM> Okullar { get; set; }
        public List<PersonellerVM> Personeller { get; set; }
        public List<SehirlerVM> Sehirler { get; set; }
        public List<SubelerVM> Siniflar { get; set; }
        public List<SoruBankasiLogVM> SoruBankasiLog { get; set; }
        public List<SoruBankasiVM> SoruBankasi { get; set; }
        public List<SoruDerecelerVM> SoruDereceler { get; set; }
        public List<SoruKategorilerVM> SoruKategoriler { get; set; }
        public List<SoruOnayVM> SoruOnay { get; set; }
        public List<SSSCevapVM> SSSCevap { get; set; }
        public List<SSSVM> SSS { get; set; }
        public List<SiniflarVM> Subeler { get; set; }
        public List<TelefonlarVM> Telefonlar { get; set; }
        public List<TemsilciliklerVM> Temsilcilikler { get; set; }
        public List<UlkeGruplariVM> UlkeGruplari { get; set; }
        public List<UlkelerVM> Ulkeler { get; set; }
        public List<UniversitelerVM> Universiteler { get; set; }      
        
               
        #endregion
    }
}
