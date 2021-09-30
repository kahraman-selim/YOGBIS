using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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
        public string TcKimlikNo { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public DateTime DogumTarihi { get; set; }
        public int KulaniciAdDegLimiti { get; set; } = 10;
        public byte[] KullaniciResim { get; set; }
        public bool? Aktif { get; set; }
    }
}
