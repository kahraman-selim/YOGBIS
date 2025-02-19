using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Common.VModels
{
    public class UlkeTercihVM:BaseVM
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid UlkeTercihId { get; set; }
        [Required(ErrorMessage = "Ülke adını yazınız...")]
        public string UlkeTercihAdi { get; set; }
        [Required(ErrorMessage = "Ülke tercih sırasını yazınız...")]
        public int UlkeTercihSiraNo { get; set; }
        [Required(ErrorMessage = "Yabancı dil seçimi yapınız...")]
        public string YabancıDil { get; set; }
        [Required(ErrorMessage = "Derece seçimi yapınız...")]
        public Guid DereceId { get; set; }
        public string DereceAdi { get; set; }
        public SoruDerecelerVM SoruDereceler { get; set; }
        [Required(ErrorMessage = "Mülakat seçimi yapınız...")]
        public Guid MulakatId { get; set; }
        public int MulakatYil { get; set; }
        public MulakatlarVM Mulakatlar { get; set; }
        public string KaydedenId { get; set; }
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
        public List<BranslarVM> Branslars { get; set; }
        public List<AdayBasvuruBilgileriVM> AdayBasvuruBilgileris { get; set; }
    }
}
