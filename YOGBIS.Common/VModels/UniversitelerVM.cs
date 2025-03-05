using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Common.VModels
{
    public class UniversitelerVM:BaseVM
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid UniId { get; set; }
        public string UniAdi { get; set; }
        public string UniLogo { get; set; }
        public string UniBilgi { get; set; }
        public Guid SehirId { get; set; }
        public SehirlerVM Sehirler { get; set; }
        public string KaydedenId { get; set; }
        [Display(Name = "Kaydeden")]
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
        public List<AdayGorevKaydiVM> AdayGorevKaydi { get; set; }
    }
}
