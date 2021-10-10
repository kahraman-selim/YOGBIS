using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YOGBIS.Data.DbModels
{
    public class OkulBilgi:Base
    {
        [Key]
        public int OkulBilgiId { get; set; }
        public string OkulTelefon { get; set; }
        public string YoneticiGorev { get; set; }
        public string YoneticiAdiSoyadi { get; set; }
        public string YoneticiTelefon { get; set; }
        public int OkulId { get; set; }

        [ForeignKey("OkulId")]
        public Okullar Okullar { get; set; }
        public int UlkeId { get; set; }

        [ForeignKey("UlkeId")]
        public Ulkeler Ulkeler { get; set; }
        public string KullaniciId { get; set; }
        [ForeignKey("KullaniciId")]
        public Kullanici Kullanici { get; set; }
    }
}
