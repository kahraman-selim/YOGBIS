using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public bool? OturumDurumu { get; set; }

        #region BağlıTablolar
        public virtual ICollection<AdayBasvuruBilgileri> AdayBasvuruBilgileri { get; set; }
        public virtual ICollection<AdayDDK> AdayDDK { get; set; }
        public virtual ICollection<AdayGorevKaydi> AdayGorevKaydi { get; set; }
        public virtual ICollection<AdayIletisimBilgileri> AdayIletisimBilgileri { get; set; }
        public virtual ICollection<Adaylar> Adaylar { get; set; }
        public virtual ICollection<AdayMYSS> AdayMYSS { get; set; }      
        public virtual ICollection<AdayTYS> AdayTYS { get; set; }
        public virtual ICollection<AdaySinavNotlar> AdayNot { get; set; }
        public virtual ICollection<Branslar> Branslar { get; set; }
        public virtual ICollection<DosyaGaleri> DosyaGaleri { get; set; }
        public virtual ICollection<Duyurular> Duyurular { get; set; }
        public virtual ICollection<EPostaAdresleri> EPostaAdresleri { get; set; }
        public virtual ICollection<Etkinlikler> Etkinlikler { get; set; }
        public virtual ICollection<Eyaletler> Eyaletler { get; set; }
        public virtual ICollection<FotoGaleri> FotoGaleri { get; set; }
        public virtual ICollection<GorevKararPdfGaleri> GorevKararPdfGaleri { get; set; }
        public virtual ICollection<IkametAdresleri> IkametAdresleri { get; set; }
        public virtual ICollection<Ilceler> Ilceler { get; set; }
        public virtual ICollection<Iller> Iller { get; set; }
        public virtual ICollection<IllerMdEPosta> IllerMdEPosta { get; set; }
        public virtual ICollection<Kitalar> Kitalar { get; set; }
        public virtual ICollection<Komisyonlar> Komisyonlar { get; set; }
        public virtual ICollection<Mulakatlar> Mulakatlar { get; set; }
        public virtual ICollection<MulakatSorulari> MulakatSorulari { get; set; }
        public virtual ICollection<Notlar> Notlar { get; set; }
        public virtual ICollection<Ogrenciler> Ogrenciler { get; set; }
        public virtual ICollection<OkulBilgi> OkulBilgi { get; set; }
        public virtual ICollection<OkulBinaBolum> OkulBinaBolum { get; set; }
        public virtual ICollection<Okullar> Okullar { get; set; }
        public virtual ICollection<Personeller> Personeller { get; set; }
        public virtual ICollection<Sehirler> Sehirler { get; set; }
        public virtual ICollection<Siniflar> Siniflar { get; set; }
        public virtual ICollection<SoruBankasi> SoruBankasi { get; set; }
        public virtual ICollection<SoruBankasiLog> SoruBankasiLog { get; set; }
        public virtual ICollection<SoruDereceler> SoruDereceler { get; set; }
        public virtual ICollection<SoruKategoriler> SoruKategoriler { get; set; }
        public virtual ICollection<SoruOnay> SoruOnay { get; set; }
        public virtual ICollection<SSS> SSS { get; set; }
        public virtual ICollection<SSSCevap> SSSCevap { get; set; }
        public virtual ICollection<Subeler> Subeler { get; set; }
        public virtual ICollection<Telefonlar> Telefonlar { get; set; }
        public virtual ICollection<Temsilcilikler> Temsilcilikler { get; set; }
        public virtual ICollection<UlkeGruplari> UlkeGruplari { get; set; }
        public virtual ICollection<Ulkeler> Ulkeler { get; set; }
        public virtual ICollection<UlkeTercih> UlkeTercih { get; set; }
        public virtual ICollection<UlkeTercihBranslar> UlkeTercihBranslar { get; set; }
        public virtual ICollection<Universiteler> Universiteler { get; set; }
        #endregion
    }
}
