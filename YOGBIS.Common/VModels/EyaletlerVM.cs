using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Common.VModels
{
    public class EyaletlerVM:BaseVM
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid EyaletId { get; set; }
        [Required(ErrorMessage ="Eyalet adı zorunludur")]
        [Display(Name = "Eyalet Adı")]
        public string EyaletAdi { get; set; }
        public int EyaletVatandas { get; set; }
        public string EyaletAciklama { get; set; }
        public string TemsilciId { get; set; }
        public string TemsilciAdiSoyyadi { get; set; }
        public Guid UlkeId { get; set; }
        public string UlkeAdi { get; set; }
        public UlkelerVM Ulkeler { get; set; }
        [Required(ErrorMessage = "Kaydeden kişi zorunludur")]
        public string KaydedenId { get; set; }
        [Display(Name = "Kaydeden")]
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
        public List<SehirlerVM> Sehirler { get; set; }
        public List<OkullarVM> Okullar { get; set; }
    }
}
