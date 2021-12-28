using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class UlkelerVM:BaseVM
    {
        [Key]
        public int UlkeId { get; set; }
        [Required (ErrorMessage ="Ülke adı zorunlu bir alandır")]
        public string UlkeAdi { get; set; }

        //[Required(ErrorMessage = "Ülkenin Bayrağını yükleyiniz")]
        //public IFormFile UlkeBayrak { get; set; }
        //public string UlkeBayrakText { get; set; }
        public string UlkeAciklama { get; set; }

        [Required(ErrorMessage = "Kıta adı zorunlu bir alandır")]
        public int KitaId { get; set; }

        [Required(ErrorMessage = "Kıta adı zorunlu bir alandır")]
        public string KitaAdi { get; set; }
        public KitalarVM Kitalar { get; set; }
        public string KaydedenId { get; set; }
        public string KullaniciAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
    }
}
