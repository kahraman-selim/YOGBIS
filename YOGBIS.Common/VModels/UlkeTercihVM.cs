using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Common.VModels
{
    public class UlkeTercihVM : BaseVM
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid UlkeTercihId { get; set; }

        [Required(ErrorMessage = "Ülke adı zorunludur")]
        [Display(Name = "Ülke Adı")]
        public string UlkeTercihAdi { get; set; }

        [Required(ErrorMessage = "Tercih sırası zorunludur")]
        [Display(Name = "Tercih Sırası")]
        public int UlkeTercihSiraNo { get; set; }

        [Required(ErrorMessage = "Yabancı dil seçimi zorunludur")]
        [Display(Name = "Yabancı Dil")]
        public string YabancıDil { get; set; }

        [Required(ErrorMessage = "Derece seçimi zorunludur")]
        public Guid DereceId { get; set; }

        public string DereceAdi { get; set; }
        public int DereceKodu { get; set; }

        [Display(Name = "Soru Derecesi")]
        public SoruDerecelerVM SoruDereceler { get; set; }

        [Required(ErrorMessage = "Mülakat seçimi zorunludur")]
        public Guid MulakatId { get; set; }

        public int MulakatYil { get; set; }

        [Display(Name = "Mülakat")]
        public MulakatlarVM Mulakatlar { get; set; }

        [Required(ErrorMessage = "Kaydeden kişi zorunludur")]
        public string KaydedenId { get; set; }

        [Display(Name = "Kaydeden")]
        public string KaydedenAdi { get; set; }

        public KullaniciVM Kullanici { get; set; }

        [Display(Name = "Tercih Edilen Branşlar")]
        public virtual ICollection<UlkeTercihBranslarVM> UlkeTercihBranslar { get; set; }

        [Display(Name = "Aday Başvuru Bilgileri")]
        public virtual ICollection<AdayBasvuruBilgileriVM> AdayBasvuruBilgileri { get; set; }
    }
}
