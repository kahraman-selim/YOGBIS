using System;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class OgrencilerVM:BaseVM
    {
        [Key]
        public int OgrencilerId { get; set; }
        [Required(ErrorMessage = "Sınıfı seçiniz")]
        public int SinifId { get; set; }
        public string SinifAdi { get; set; }
        public SiniflarVM Siniflar { get; set; }
        public string Uyruk { get; set; }
        public bool Cinsiyet { get; set; }
        public DateTime OkulKayitTarihi { get; set; }
        public string KaydedenId { get; set; }
        public string KullaniciAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
    }
}
