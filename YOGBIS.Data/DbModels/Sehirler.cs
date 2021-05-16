using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YOGBIS.Data.DbModels
{
    public class Sehirler
    {
        [Key]
        public int SehirId { get; set; }
        public string SehirAdi { get; set; }
        public bool? Baskent { get; set; }
        public string SehirAciklama { get; set; }
        [ForeignKey("EyaletId")]
        public int EyaletId { get; set; }
        public Eyaletler Eyaletler { get; set; }
    }
}
