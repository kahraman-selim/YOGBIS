using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.Extentsion;
using System;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class KullaniciYekiYonetimVM
    {
        public string RoleId { get; set; }        
        public string RoleName { get; set; }
        public bool Selected { get; set; }
        [Required(ErrorMessage = "Kaydeden kişi zorunludur")]
        [Display(Name = "Kaydeden")]
        public string KaydedenAdi { get; set; }
    }
}
