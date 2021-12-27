using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YOGBIS.Data.DbModels
{
    public class Universiteler:Base
    {
        [Key]
        public int UniId { get; set; }
        public string UniAdi { get; set; }
        public string UniLogo { get; set; }
        public string UniBilgi { get; set; }
        public int SehirId { get; set; }
        [ForeignKey("SehirId")]
        public Sehirler Sehirler { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
        public List<Okutmanlar> Okutmanlars { get; set; }
    }
}
