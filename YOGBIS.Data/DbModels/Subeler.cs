using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Subeler:Base
    {
        [Key]
        public int SubeId { get; set; }
        public string OkulAdi { get; set; }
        public DateTime SubeAcilisTarihi { get; set; }
        public int OkulId { get; set; }
        [ForeignKey("OkulId")]
        public Okullar Okullar { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
        public List<Siniflar> Siniflars { get; set; }
    }
}
