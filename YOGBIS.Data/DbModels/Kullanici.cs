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
        public byte[] KullaniciResim { get; set; }
        public string KullaniciResimYol { get; set; }
        public bool? Aktif { get; set; }

        #region BağlıTablolar
        public List<Dereceler> Derecelers { get; set; }
        public List<SoruKategoriler> SoruKategorilers { get; set; }
        public List<Mulakatlar> Mulakatlars { get; set; }
        public List<Adaylar> Adaylars { get; set; }
        public List<UlkeGruplari> UlkeGruplaris { get; set; }
        public List<Ulkeler> Ulkelers { get; set; }
        public List<Eyaletler> Eyaletlers { get; set; }
        public List<Sehirler> Sehirlers { get; set; }
        public List<Okullar> Okullars { get; set; }
        public List<Notlar> Notlars { get; set; }
        public List<Ogretmenler> Ogretmenlers { get; set; }
        public List<Okutmanlar> Okutmanlars { get; set; }
        public List<Universiteler> Universitelers { get; set; }
        public List<Subeler> Subelers { get; set; }
        public List<Siniflar> Siniflars { get; set; }
        public List<Ogrenciler> Ogrencilers { get; set; }
        #endregion
    }
}
