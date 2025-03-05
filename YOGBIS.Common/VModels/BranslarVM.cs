using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class BranslarVM : BaseVM
    {
        public Guid BransId { get; set; }

        [Required(ErrorMessage = "Branş adı zorunludur")]
        [StringLength(100, ErrorMessage = "Branş adı en fazla 100 karakter olabilir")]
        [Display(Name = "Branş Adı")]
        public string BransAdi { get; set; }

        [Display(Name = "Branş Açıklaması")]
        public string BransAciklama { get; set; }

        [Display(Name = "Branş Durumu")]
        public bool? Durumu { get; set; }

        [Required(ErrorMessage = "Kaydeden kişi zorunludur")]
        public string KaydedenId { get; set; }

        [Display(Name = "Kaydeden")]
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }

        [Display(Name = "Ülke Tercih Branşları")]
        public virtual ICollection<UlkeTercihBranslarVM> UlkeTercihBranslars { get; set; }
    }
}
