using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Sehirler:Base
    {
        [Key]
        public int SehirId { get; set; }
        public string SehirAdi { get; set; }
        public bool? Baskent { get; set; }
        public string SehirAciklama { get; set; }        
        public int EyaletId { get; set; }
        [ForeignKey("EyaletId")]
        public Eyaletler Eyaletler { get; set; }
        public string KullaniciId { get; set; }
        [ForeignKey("KullaniciId")]
        public Kullanici Kullanici { get; set; }
    }
}
