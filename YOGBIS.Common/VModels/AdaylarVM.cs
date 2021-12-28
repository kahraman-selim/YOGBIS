using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class AdaylarVM:BaseVM
    {
        [Key]
        public int AdayId { get; set; }
        public int AdayTC { get; set; }
        public string KaydedenId { get; set; }
        public KullaniciVM Kullanici { get; set; }
        public string AdayAd { get; set; }
        public string AdayAd2 { get; set; }
        public string AdaySoyad { get; set; }
        public string AdaySoyad2 { get; set; }
        public int MulakatId { get; set; }
        public MulakatlarVM Mulakatlar { get; set; }
    }
}
