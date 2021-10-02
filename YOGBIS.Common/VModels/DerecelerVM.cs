using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Common.VModels
{
    public class DerecelerVM:BaseVM
    {
        [Key]
        public int Id { get; set; }
        [Required (ErrorMessage ="Dereceyi yazınz")]
        public string DereceAdi { get; set; }
        public string KullaniciId { get; set; }
        public Kullanici Kullanici { get; set; }
    }
}
