using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Universiteler:Base
    {
        [Key]
        public int UniId { get; set; }
        public string UniAdi { get; set; }
        public bool YurtIciMi { get; set; }
        public string UniStatu { get; set; }
        public string UniLogo { get; set; }
        public string UniBilgi { get; set; }
        public int? SehirId { get; set; }
        public int? EyaletId { get; set; }
        public int UlkeId { get; set; }
        [ForeignKey("UlkeId")]
        public Ulkeler Ulkeler { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
        public List<AdayGorevKaydi> AdayGorevKaydis { get; set; }
        public ICollection<FotoGaleri> FotoGaleri { get; set; }
    }
}
