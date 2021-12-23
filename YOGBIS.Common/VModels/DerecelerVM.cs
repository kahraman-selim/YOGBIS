using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class DerecelerVM:BaseVM
    {
        [Key]
        public int DereceId { get; set; }
        [Required (ErrorMessage ="Dereceyi yazınz")]
        public string DereceAdi { get; set; }
        public string KullaniciId { get; set; }
        public string KullaniciAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
        public List<SoruDereceVM> SoruDereces { get; set; }
        public List<SoruKategorilerVM> SoruKategorilers { get; set; }
    }
}
