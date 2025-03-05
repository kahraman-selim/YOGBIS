using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Common.VModels
{
    public class OgrencilerVM : BaseVM
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid OgrencilerId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime KayitTarihi { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Öğrenci türünü seçiniz !")]
        public string OgrenciTuru { get; set; }

        [Required(ErrorMessage = "Öğrenci uyruğunu seçiniz !")]
        public string Uyruk { get; set; }

        [Required(ErrorMessage = "Cinsiyetini seçiniz !")]
        public bool Cinsiyet { get; set; }
        public DateTime? BaslamaKayitTarihi { get; set; }
        public string KayitNedeni { get; set; }        
        public int KayitSayisi { get; set; }
        public int ToplamKayit { get; set; }
        public DateTime? AyrilmaTarihi { get; set; }
        public string AyrilmaNedeni { get; set; }
        public int AyrilanSayisi { get; set; }
        public Guid? EgitimciId { get; set; }
        public string SinifAdi { get; set; }
        public Guid? SinifId { get; set; }
        public string SubeAdi { get; set; }
        public Guid? SubeId { get; set; }
        public Guid? OkulId { get; set; }
        public Guid? UniversiteId { get; set; }
        public Guid? SehirId { get; set; }
        public Guid? EyaletId { get; set; }
        public Guid? TemsilcilikId { get; set; }
        public Guid UlkeId { get; set; }
        public string UlkeAdi { get; set; }
        public UlkelerVM Ulkeler { get; set; }
        public string StatusMessage { get; set; } = "";
        [Required(ErrorMessage = "Kaydeden kişi zorunludur")]
        public string KaydedenId { get; set; }
        [Display(Name = "Kaydeden")]
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
    }
}
