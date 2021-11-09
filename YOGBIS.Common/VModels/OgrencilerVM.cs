using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YOGBIS.Common.VModels
{
    public class OgrencilerVM:BaseVM
    {
        [Key]
        public int OgrencilerId { get; set; }
        public int OkulId { get; set; }
        public string OkulAdi { get; set; }
        public OkullarVM Okullar { get; set; }
        public int UlkeId { get; set; }
        public string UlkeAdi { get; set; }
        public UlkelerVM Ulkeler { get; set; }
        public int TCEOg { get; set; }
        public int TCKOg { get; set; }
        public int DEOg { get; set; }
        public int DKOg { get; set; }
        public string Ay { get; set; }
        public string Yil { get; set; }
        public string KullaniciId { get; set; }
        public string KullaniciAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
    }
}
