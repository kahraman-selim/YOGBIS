using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Common.VModels
{
    public class SoruDerecelerVM:BaseVM
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid DereceId { get; set; }
        [Required (ErrorMessage ="Dereceyi yazınz")]
        public string DereceAdi { get; set; }
        public int DereceKodu { get; set; }
        public string KaydedenId { get; set; }
        [Display(Name = "Kaydeden")]
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
        public List<SoruDereceVM> SoruDereces { get; set; }
        public List<SoruKategorilerVM> SoruKategoriler { get; set; }
    }
}
