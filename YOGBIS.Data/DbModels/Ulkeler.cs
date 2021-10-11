using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YOGBIS.Data.DbModels
{
    public class Ulkeler:Base
    {
        [Key]
        public int UlkeId { get; set; }
        public string UlkeAdi { get; set; }
        public string UlkeBayrak { get; set; }
        public string UlkeAciklama { get; set; }        
        public int KitaId { get; set; }
        [ForeignKey("KitaId")]
        public Kitalar Kitalar { get; set; }
        public string KullaniciId { get; set; }
        [ForeignKey("KullaniciId")]
        public Kullanici Kullanici { get; set; }
    }
}
