using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Data.DbModels
{
    public class Adaylar:Base
    {
        [Key]
        public int AdayId { get; set; }
        public int AdayTC { get; set; }
        public string AdayAd { get; set; }
        public string AdayAd2 { get; set; }
        public string AdaySoyad { get; set; }
        public string AdaySoyad2 { get; set; }
    }
}
