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
        public string SehirAdi { get; set; }
        public bool? Baskent { get; set; }
        public string SehirAciklama { get; set; }
        public int SehirVatandas { get; set; }
        public string TemsilciId { get; set; }
        public Guid UlkeId { get; set; }
        public Guid EyaletId { get; set; }
        public string EyaletAdi { get; set; }
        public EyaletlerVM Eyaletler { get; set; }
        public string KaydedenId { get; set; }
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
        public List<OkullarVM> Okullars { get; set; }
        public List<UniversitelerVM> Universitelers { get; set; }
        public List<OgretmenlerVM> Ogretmenlers { get; set; }
        public IFormFileCollection FotoGaleris { get; set; }
        public List<FotoGaleriVM> FotoGaleri { get; set; }
    }
}
