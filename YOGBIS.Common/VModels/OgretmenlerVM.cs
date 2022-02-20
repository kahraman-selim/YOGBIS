using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Common.VModels
{
    public class OgretmenlerVM:BaseVM
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid OgretmenId { get; set; }
        public int OgretmenTC { get; set; }
        public string KaydedenId { get; set; }
        public KullaniciVM Kullanici { get; set; }
        public string OgretmenAd { get; set; }
        public string OgretmenAd2 { get; set; }
        public string OgretmenSoyad { get; set; }
        public string OgretmenSoyad2 { get; set; }
        //diğer alanlar eklenecek
        public Guid? OkulId { get; set; }
        public OkullarVM Okullar { get; set; }
        public Guid SehirId { get; set; }
        public SehirlerVM Sehirler { get; set; }
        public List<GorevKaydiVM> GorevKaydis { get; set; }
    }
}
