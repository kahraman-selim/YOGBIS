using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class SiniflarVM:BaseVM
    {
        [Key]
        public int SinifId { get; set; }
        public string SinifAdi { get; set; }
        public DateTime SinifAcilisTarihi { get; set; }
        public int SubeId { get; set; }     
        public SubelerVM Subeler { get; set; }
        public string KaydedenId { get; set; }
        public KullaniciVM Kullanici { get; set; }
        public List<OgrencilerVM> Ogrencilers { get; set; }
    }
}
