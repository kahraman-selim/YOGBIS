using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Common.VModels
{
    public class SiniflarVM:BaseVM
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid SinifId { get; set; }
        [Required(ErrorMessage = "Sınıf adı zorunludur")]
        [Display(Name = "Sınıf Adı")]
        public string SinifAdi { get; set; }
        public string SinifGrubu { get; set; }
        public DateTime SinifAcilisTarihi { get; set; }
        public Guid OkulId { get; set; }
        public OkullarVM Okullar { get; set; }
        [Required(ErrorMessage = "Kaydeden kişi zorunludur")]
        public string KaydedenId { get; set; }
        [Display(Name = "Kaydeden")]
        public string KaydedenAdi { get; set; }        
        public KullaniciVM Kullanici { get; set; }
        public List<SubelerVM> Subeler { get; set; }
        public List<OgrencilerVM> Ogrenciler { get; set; }
    }
}
