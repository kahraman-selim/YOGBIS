using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YOGBIS.Data.DbModels
{
    public class Kullanici : IdentityUser
    {
        public string TcKimlikNo { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }

        [DisplayFormat(DataFormatString = "{0,d}")]
        [DataType(DataType.DateTime)]
        public DateTime KayitTarihi { get; set; } = DateTime.Now;
        public int KulaniciAdDegLimiti { get; set; } = 10;
        public byte[] KullaniciResim { get; set; }
        public bool? Aktif { get; set; }
    }
}
