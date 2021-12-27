using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Ogretmenler:Base
    {
        [Key]
        public string OgretmenTC { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
        public string OgretmenAd { get; set; }
        public string OgretmenAd2 { get; set; }
        public string OgretmenSoyad { get; set; }
        public string OgretmenSoyad2 { get; set; }
        
        //diğer alanlar eklenecek
        public int OkulId { get; set; }
        [ForeignKey("OkulId")]
        public Okullar Okullar { get; set; }
    }
}
