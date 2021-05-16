using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YOGBIS.Data.DbModels
{
    public class Eyaletler
    {
        [Key]
        public int EyaletId { get; set; }
        public string EyaletAdi { get; set; }
        public string EyaletAciklama { get; set; }
        [ForeignKey("UlkeId")]
        public int UlkeId { get; set; }
        public Ulkeler Ulkeler { get; set; }
    }
}
