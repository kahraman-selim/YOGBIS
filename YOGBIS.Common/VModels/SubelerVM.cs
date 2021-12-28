using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class SubelerVM:BaseVM
    {
        [Key]
        public int SubeId { get; set; }
        public string OkulAdi { get; set; }
        public DateTime SubeAcilisTarihi { get; set; }
        public int OkulId { get; set; }
        public OkullarVM Okullar { get; set; }
        public string KaydedenId { get; set; }
        public KullaniciVM Kullanici { get; set; }
        public List<SiniflarVM> Siniflars { get; set; }
    }
}
