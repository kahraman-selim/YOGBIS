using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Data.DbModels
{
    public class Kullanici : IdentityUser
    {
        public string TcKimlikNo { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public DateTime KayitTarihi { get; set; }
        public int KulaniciAdDegLimiti { get; set; }        
        [MaxLength]
        public byte[] KullaniciResim { get; set; }
        public string KullaniciResimYol { get; set; }
        public bool? Aktif { get; set; }

        #region BağlıTablolar
        public List<AdayDDK> AdayDDK { get; set; }
        public List<AdayGorevKaydi> AdayGorevKaydi { get; set; }
        public List<Adaylar> Adaylar { get; set; }
        public List<AdayNot> AdayNot { get; set; }
        public List<Branslar> Branslar { get; set; }
        public ICollection<DosyaGaleri> DosyaGaleri { get; set; }
        public List<Duyurular> Duyurular { get; set; }
        public List<EPostaAdresleri> EPostaAdresleri { get; set; }
        public List<Etkinlikler> Etkinlikler { get; set; }
        public List<Eyaletler> Eyaletler { get; set; }
        public ICollection<FotoGaleri> FotoGaleri { get; set; }        
        public ICollection<GorevKararPdfGaleri> GorevKararPdfGaleri { get; set; }
        public List<IkametAdresleri> IkametAdresleri { get; set; }
        public List<Ilceler> Ilceler { get; set; }
        public List<Iller> Iller { get; set; }
        public List<IllerMdEPosta> IllerMdEPosta { get; set; }        
        public List<Komisyonlar> Komisyonlar { get; set; }
        public List<Mulakatlar> Mulakatlar { get; set; }
        public List<MulakatSorulari> MulakatSorulari { get; set; }
        public List<Notlar> Notlar { get; set; }
        public List<Ogrenciler> Ogrenciler { get; set; }
        public List<OkulBilgi> OkulBilgi { get; set; }
        public List<Okullar> Okullar { get; set; }
        public List<Personeller> Personeller { get; set; }
        public List<Sehirler> Sehirler { get; set; }
        public List<Subeler> Siniflar { get; set; }
        public List<SoruBankasi> SoruBankasi { get; set; }
        public List<SoruBankasiLog> SoruBankasiLog { get; set; }
        public List<SoruDereceler> SoruDereceler { get; set; }
        public List<SoruKategoriler> SoruKategoriler { get; set; }
        public List<SoruOnay> SoruOnay { get; set; }
        public List<SSS> SSS { get; set; }
        public List<SSSCevap> SSSCevap { get; set; }
        public List<Siniflar> Subeler { get; set; }
        public List<Telefonlar> Telefonlar { get; set; }
        public List<Temsilcilikler> Temsilcilikler { get; set; }
        public List<UlkeGruplari> UlkeGruplari { get; set; }
        public List<Ulkeler> Ulkeler { get; set; }
        public List<Universiteler> Universiteler { get; set; }
        #endregion
    }
}
