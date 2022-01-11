using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class SoruDerecelerVM:BaseVM
    {
        [Key]
        public int DereceId { get; set; }
        [Required (ErrorMessage ="Dereceyi yazınz")]
        public string DereceAdi { get; set; }
        public string KaydedenId { get; set; }
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
        public List<SoruDereceVM> SoruDereces { get; set; }
        public List<SoruKategorilerVM> SoruKategorilers { get; set; }
    }
}
