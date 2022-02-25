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
        public List<AdayDDK> AdayDDKs { get; set; }
        public List<AdayGorevKaydi> AdayGorevKaydis { get; set; }
        public List<Adaylar> Adaylars { get; set; }
        public List<AdayNot> AdayNots { get; set; }
        public List<Branslar> Branslar { get; set; }
        public List<Duyurular> Duyurulars { get; set; }
        public List<EPostaAdresleri> EPostaAdresleris { get; set; }
        public List<Etkinlikler> Etkinliklers { get; set; }
        public List<Eyaletler> Eyaletlers { get; set; }
        public ICollection<FotoGaleri> FotoGaleri { get; set; }
        public ICollection<DosyaGaleri> DosyaGaleri { get; set; }
        public ICollection<GorevKararPdfGaleri> GorevKararPdfGaleris { get; set; }
        public List<IkametAdresleri> IkametAdresleris { get; set; }
        public List<Ilceler> Ilcelers { get; set; }
        public List<Iller> Illers { get; set; }
        public List<IllerMdEPosta> IllerMdEPostas { get; set; }        
        public List<Komisyonlar> Komisyonlars { get; set; }
        public List<Mulakatlar> Mulakatlars { get; set; }
        public List<MulakatSorulari> MulakatSorularis { get; set; }
        public List<Notlar> Notlars { get; set; }
        public List<Ogrenciler> Ogrencilers { get; set; }
        public List<OkulBilgi> OkulBilgis { get; set; }
        public List<Okullar> Okullars { get; set; }
        public List<Personeller> Personellers { get; set; }
        public List<Sehirler> Sehirlers { get; set; }
        public List<Siniflar> Siniflars { get; set; }
        public List<SoruBankasi> SoruBankasis { get; set; }
        public List<SoruBankasiLog> SoruBankasiLogs { get; set; }
        public List<SoruDereceler> SoruDerecelers { get; set; }
        public List<SoruKategoriler> SoruKategorilers { get; set; }
        public List<SoruOnay> SoruOnays { get; set; }
        public List<SSS> SSSs { get; set; }
        public List<SSSCevap> SSSCevaps { get; set; }
        public List<Subeler> Subelers { get; set; }
        public List<Telefonlar> Telefonlar { get; set; }
        public List<Temsilcilikler> Temsilciliklers { get; set; }
        public List<UlkeGruplari> UlkeGruplaris { get; set; }
        public List<Ulkeler> Ulkelers { get; set; }
        public List<Universiteler> Universitelers { get; set; }
        #endregion
    }
}
