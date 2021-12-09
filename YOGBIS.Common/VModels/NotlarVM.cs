using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Common.VModels
{
    public class NotlarVM:BaseVM
    {
        [Key]
        public int NotId { get; set; }
        [Required(ErrorMessage = "Not için başlık yazınz")]
        public string NotAdi { get; set; }
        public string NotDetay { get; set; }
        [Required(ErrorMessage = "Başlangıç tarihini seçiniz")]
        public DateTime BaTarihi { get; set; }
        [Required(ErrorMessage = "Bitiş tarihini seçiniz")]
        public DateTime BiTarihi { get; set; }
        public string NotRenk { get; set; }
        public string KullaniciId { get; set; }
        public string KullaniciAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
    }
}
