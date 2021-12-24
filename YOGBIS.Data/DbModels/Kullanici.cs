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
        public List<Dereceler> Derecelers { get; set; }
        public List<SoruKategoriler> SoruKategorilers { get; set; }
        public List<Mulakatlar> Mulakatlars { get; set; }
        public List<Adaylar> Adaylars { get; set; }
    }
}
