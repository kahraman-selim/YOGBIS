using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Common.VModels
{
    public class SehirlerVM:BaseVM
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid SehirId { get; set; }
        [Required(ErrorMessage = "Şehir/Bölge adı zorunlu bir alandır!")]
        [Display(Name = "Şehir Adı")]
        public string SehirAdi { get; set; }
        public bool? Baskent { get; set; }
        [Display(Name = "Şehir Açıklaması")]
        public string SehirAciklama { get; set; }
        public int SehirVatandas { get; set; }
        public string TemsilciId { get; set; }
        public Guid UlkeId { get; set; }
        [Required(ErrorMessage = "Eyalet zorunludur")]
        public Guid? EyaletId { get; set; }
        public string EyaletAdi { get; set; }
        [Display(Name = "Eyalet")]
        public EyaletlerVM Eyaletler { get; set; }
        [Required(ErrorMessage = "Kaydeden kişi zorunludur")]
        public string KaydedenId { get; set; }
        [Display(Name = "Kaydeden")]
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
        [Display(Name = "Okullar")]
        public virtual ICollection<OkullarVM> Okullars { get; set; }
        public List<UniversitelerVM> Universitelers { get; set; }
        public IFormFileCollection FotoGaleris { get; set; }
        public List<FotoGaleriVM> FotoGaleri { get; set; }
    }
}
