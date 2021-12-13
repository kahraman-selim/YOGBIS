using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Common.VModels
{
    public class MulakatlarVM:BaseVM
    {
        [Key]
        public int MulakatId { get; set; }
        [Required(ErrorMessage ="{0} zorunlu bir alandır")]
        public string OnaySayisi { get; set; }
        [Required(ErrorMessage = "{0} zorunlu bir alandır")]
        public string MulakatAdi { get; set; }
        [Required(ErrorMessage = "{0} zorunlu bir alandır")]
        public int DereceId { get; set; }
        public string Derecesi { get; set; }
        [Required(ErrorMessage = "{0} zorunlu bir alandır")]
        public DateTime BaslamaTarihi { get; set; }
        [Required(ErrorMessage = "{0} zorunlu bir alandır")]
        public DateTime BitisTarihi { get; set; }
        public int AdaySayisi { get; set; } = 0;
        public int SorulanSoruSayisi { get; set; } = 0;
        public bool Durumu { get; set; } = true;
        public string MulakatAciklama { get; set; }
        public string KullaniciId { get; set; }
        public string KullaniciAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
    }
}
