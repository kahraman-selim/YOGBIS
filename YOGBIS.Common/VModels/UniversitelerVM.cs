using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class UniversitelerVM:BaseVM
    {
        [Key]
        public int UniId { get; set; }
        public string UniAdi { get; set; }
        public string UniLogo { get; set; }
        public string UniBilgi { get; set; }
        public int SehirId { get; set; }
        public SehirlerVM Sehirler { get; set; }
        public string KaydedenId { get; set; }
        public KullaniciVM Kullanici { get; set; }
        public List<OkutmanlarVM> Okutmanlars { get; set; }
    }
}
