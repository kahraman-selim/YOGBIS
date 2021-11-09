using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YOGBIS.Common.VModels
{
    public class OgrencilerVM:BaseVM
    {
        [Key]
        public int OgrencilerId { get; set; }
        [Required(ErrorMessage = "Okulunuzu seçiniz")]
        public int OkulId { get; set; }
        public string OkulAdi { get; set; }
        public OkullarVM Okullar { get; set; }
        [Required(ErrorMessage = "Bulunduğunuz ülkeyi seçiniz")]
        public int UlkeId { get; set; }
        public string UlkeAdi { get; set; }
        public UlkelerVM Ulkeler { get; set; }
        [Required(ErrorMessage = "En az 0 değeri gereklidir")]
        public int TCEOg { get; set; }
        [Required(ErrorMessage = "En az 0 değeri gereklidir")]
        public int TCKOg { get; set; }
        [Required(ErrorMessage = "En az 0 değeri gereklidir")]
        public int DEOg { get; set; }
        [Required(ErrorMessage = "En az 0 değeri gereklidir")]
        public int DKOg { get; set; }
        [Required(ErrorMessage = "Ay seçimi gereklidir")]
        public string Ay { get; set; }
        [Required(ErrorMessage = "Yıl seçimi gereklidir")]
        public string Yil { get; set; }
        public string KullaniciId { get; set; }
        public string KullaniciAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
    }
}
