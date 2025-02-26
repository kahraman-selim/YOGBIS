using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Common.VModels
{
    public class UlkeTercihBranslarVM:BaseVM
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid TercihBransId { get; set; }
        [Required(ErrorMessage = "Branş adı zorunlu bir alandır")]
        public string BransAdi { get; set; }
        [Required(ErrorMessage = "Bu alan zorunlu bir alandır")]
        public Guid BransId { get; set; }
        public string BransCinsiyet { get; set; }
        public int BransKontSayi { get; set; }
        public bool EsitBrans { get; set; }
        public string YabanciDil { get; set; }
        public Guid UlkeTercihId { get; set; }
        public string UlkeTercihAdi { get; set; }        
        public string KaydedenId { get; set; }
        public string KaydedenAdi { get; set; }
    }
}
