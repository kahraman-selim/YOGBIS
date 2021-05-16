using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YOGBIS.Data.DbModels
{
    public class Ulkeler
    {
        [Key]
        public int UlkeId { get; set; }
        public string UlkeAdi { get; set; }
        public string UlkeBayrak { get; set; }
        public string UlkeAciklama { get; set; }
        [ForeignKey("KitaId")]
        public int KitaId { get; set; }
        public Kitalar Kitalar { get; set; }
    }
}
