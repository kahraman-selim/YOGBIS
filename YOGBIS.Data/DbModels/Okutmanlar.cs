using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Okutmanlar:Base
    {
        [Key]
        public int OkutmanId { get; set; }
        public int OkutmanTC { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
        public string OkutmanAd { get; set; }
        public string OkutmanAd2 { get; set; }
        public string OkutmanSoyad { get; set; }
        public string OkutmanSoyad2 { get; set; }

        //diğer alanlar eklenecek
        public int UniId { get; set; }
        [ForeignKey("UniId")]
        public Universiteler Universiteler { get; set; }
        public List<GorevKaydi> GorevKaydis { get; set; }
    }
}
