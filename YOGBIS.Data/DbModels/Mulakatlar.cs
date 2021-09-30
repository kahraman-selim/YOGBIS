using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YOGBIS.Data.DbModels
{
    public class Mulakatlar:Base
    {
        [Key]
        public int MulakatId { get; set; }
        public string OnaySayisi { get; set; }
        public string MulakatAdi { get; set; }
        public string Derecesi { get; set; }
        public DateTime BaslamaTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public int AdaySayisi { get; set; }
        public int SorulanSoruSayisi { get; set; }
        public bool? Durumu { get; set; }
        public string MulakatAciklama { get; set; }
        public string KullaniciId { get; set; }
        [ForeignKey("KullaniciId")]
        public Kullanici Kullanici { get; set; }
    }
}
