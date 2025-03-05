using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Common.VModels
{
    public class UlkeGruplariVM : BaseVM
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid UlkeGrupId { get; set; }

        [Required(ErrorMessage = "Grup adı zorunludur")]
        [Display(Name = "Grup Adı")]
        public string UlkeGrupAdi { get; set; }

        [Display(Name = "Grup Açıklaması")]
        public string UlkeGrupAciklama { get; set; }

        [Display(Name = "Grup Durumu")]
        public bool? Durumu { get; set; }

        [Required(ErrorMessage = "Kaydeden kişi zorunludur")]
        public string KaydedenId { get; set; }

        [Display(Name = "Kaydeden")]
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }

        [Display(Name = "Ülkeler")]
        public virtual ICollection<UlkelerVM> Ulkelers { get; set; }
    }
}
