using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Common.VModels
{
    public class IllerVM : BaseVM
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid IllerId { get; set; }

        [Required(ErrorMessage = "İl adı zorunludur")]
        [Display(Name = "İl Adı")]
        public string IlAdi { get; set; }

        [Display(Name = "İl Açıklaması")]
        public string IlAciklama { get; set; }

        [Display(Name = "İl Durumu")]
        public bool? Durumu { get; set; }

        [Required(ErrorMessage = "Kaydeden kişi zorunludur")]
        public string KaydedenId { get; set; }

        [Display(Name = "Kaydeden")]
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }

        [Display(Name = "İlçeler")]
        public virtual ICollection<IlcelerVM> Ilcelers { get; set; }
    }
}
