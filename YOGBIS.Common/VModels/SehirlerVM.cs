using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class SehirlerVM:BaseVM
    {
        [Key]
        public int SehirId { get; set; }
        public string SehirAdi { get; set; }
        public bool? Baskent { get; set; }
        public string SehirAciklama { get; set; }
        public int EyaletId { get; set; }
        public EyaletlerVM Eyaletler { get; set; }
        public string KullaniciId { get; set; }
        public string KullaniciAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
    }
}
