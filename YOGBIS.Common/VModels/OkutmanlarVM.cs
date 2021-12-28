using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class OkutmanlarVM:BaseVM
    {
        [Key]
        public int OkutmanId { get; set; }
        public int OkutmanTC { get; set; }
        public string KaydedenId { get; set; }
        public KullaniciVM Kullanici { get; set; }
        public string OkutmanAd { get; set; }
        public string OkutmanAd2 { get; set; }
        public string OkutmanSoyad { get; set; }
        public string OkutmanSoyad2 { get; set; }

        //diğer alanlar eklenecek
        public int UniId { get; set; }
        public UniversitelerVM Universiteler { get; set; }
        public List<GorevKaydiVM> GorevKaydis { get; set; }
    }
}
