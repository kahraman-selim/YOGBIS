using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Common.VModels
{
    public class SubelerVM:BaseVM
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid SubeId { get; set; }
        [Required(ErrorMessage = "Şube adı zorunludur")]
        [Display(Name = "Şube Adı")]
        public string SubeAdi { get; set; }
        public DateTime SubeAcilisTarihi { get; set; }
        public bool SubeDurumu { get; set; }        
        public Guid? EgitimciId { get; set; }
        public Guid OkulId { get; set; }
        public string SinifAdi { get; set; }        
        public int OgrenciSayisiKiz { get; set; }
        public int OgrenciSayisiErkek { get; set; }
        public int OgrenciSayisiToplam { get; set; }
        [Required(ErrorMessage = "Sınıf zorunludur")]
        public Guid SinifId { get; set; }        
        [Display(Name = "Sınıf")]
        public SiniflarVM Siniflar { get; set; }        
        [Required(ErrorMessage = "Kaydeden kişi zorunludur")]
        public string KaydedenId { get; set; }
        [Display(Name = "Kaydeden")]
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
        [Display(Name = "Öğrenciler")]
        public List<OgrencilerVM> Ogrenciler { get; set; }
    }
}
