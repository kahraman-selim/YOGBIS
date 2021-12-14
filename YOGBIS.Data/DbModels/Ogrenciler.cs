using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Ogrenciler:Base
    {
        [Key]
        public int OgrencilerId { get; set; }
        public int OkulId { get; set; }
        [ForeignKey("OkulId")]
        public Okullar Okullar { get; set; }
        public int UlkeId { get; set; }

        [ForeignKey("UlkeId")]
        public Ulkeler Ulkeler { get; set; }
        public int TCEOg { get; set; }
        public int TCKOg { get; set; }
        public int DEOg { get; set; }
        public int DKOg { get; set; }
        public string Ay { get; set; }
        public string Yil { get; set; }
        public string KullaniciId { get; set; }
        [ForeignKey("KullaniciId")]
        public Kullanici Kullanici { get; set; }
    }
}
