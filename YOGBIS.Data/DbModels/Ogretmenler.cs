using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Ogretmenler:Base
    {
        [Key]
        public int OgretmenId { get; set; }
        public int OgretmenTC { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
        public string OgretmenAd { get; set; }
        public string OgretmenAd2 { get; set; }
        public string OgretmenSoyad { get; set; }
        public string OgretmenSoyad2 { get; set; }
        //diğer alanlar eklenecek
        public int? OkulId { get; set; }
        [ForeignKey("OkulId")]
        public Okullar Okullar { get; set; }
        public int SehirId { get; set; }
        [ForeignKey("SehirId")]
        public Sehirler Sehirler { get; set; }
        public List<GorevKaydi> GorevKaydis { get; set; }
    }
}
