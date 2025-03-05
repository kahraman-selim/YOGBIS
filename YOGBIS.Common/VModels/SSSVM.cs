using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Common.VModels
{
    public class SSSVM : BaseVM
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid SSSId { get; set; }

        [Required(ErrorMessage = "Soru zorunludur")]
        [Display(Name = "Soru")]
        public string Soru { get; set; }

        [Display(Name = "Soru Durumu")]
        public bool? Durumu { get; set; }

        [Required(ErrorMessage = "Kaydeden kişi zorunludur")]
        public string KaydedenId { get; set; }

        [Display(Name = "Kaydeden")]
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }

        [Display(Name = "Cevaplar")]
        public virtual ICollection<SSSCevapVM> SSSCevaps { get; set; }
    }
}
