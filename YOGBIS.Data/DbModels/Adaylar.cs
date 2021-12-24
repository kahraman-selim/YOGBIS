using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Adaylar:Base
    {
        public string AdayTC { get; set; }
        [ForeignKey("TcKimlikNo")]
        public Kullanici Kullanici { get; set; }
        public string KaydedenId { get; set; }
        public string AdayAd { get; set; }
        public string AdayAd2 { get; set; }
        public string AdaySoyad { get; set; }
        public string AdaySoyad2 { get; set; }
        public int MulakatId { get; set; }
        [ForeignKey("MulakatId")]
        public Mulakatlar Mulakatlar { get; set; }        

    }
}
