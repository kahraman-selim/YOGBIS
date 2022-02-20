using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Common.VModels
{
    public class GorevKaydiVM:BaseVM
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid GorevId { get; set; }
        public int GorevliTC { get; set; }
        public string GörevAdi { get; set; }
        public DateTime? GorevBasTarihi { get; set; }
        public DateTime? GorevBitisTarihi { get; set; }
        public DateTime? GorevTarihi { get; set; }
        public string GorevOnaySayi { get; set; }
        public string GorevYeri { get; set; }
        public string KaydedenId { get; set; }
        public KullaniciVM Kullanici { get; set; }
    }
}
