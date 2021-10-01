using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YOGBIS.Data.DbModels
{
    public class Eyaletler:Base
    {
        [Key]
        public int EyaletId { get; set; }
        public string EyaletAdi { get; set; }
        public string EyaletAciklama { get; set; }        
        public int UlkeId { get; set; }
        [ForeignKey("UlkeId")]
        public Ulkeler Ulkeler { get; set; }
        public string KullaniciId { get; set; }
        [ForeignKey("KullaniciId")]
        public Kullanici Kullanici { get; set; }
    }
}
