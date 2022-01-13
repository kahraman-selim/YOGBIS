using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class EyaletlerVM:BaseVM
    {
        [Key]
        public int EyaletId { get; set; }
        [Required(ErrorMessage ="Eyalet adını yazınız!")]
        public string EyaletAdi { get; set; }
        public int EyaletVatandas { get; set; }
        public string EyaletAciklama { get; set; }       
        public int UlkeId { get; set; }
        public string UlkeAdi { get; set; }
        public UlkelerVM Ulkeler { get; set; }
        public string KaydedenId { get; set; }
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
        public List<SehirlerVM> Sehirlers { get; set; }
    }
}
