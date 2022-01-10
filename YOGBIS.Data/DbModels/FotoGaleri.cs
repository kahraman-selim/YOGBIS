using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class FotoGaleri:Base
    {
        [Key]
        public int FotoGaleriId { get; set; }
        public string FotoAdi { get; set; }
        public string FotoURL { get; set; }        
        public int EtkinlikId { get; set; }
        [ForeignKey("EtkinlikId")]
        public Etkinlikler Etkinlikler { get; set; }
        public int OkulId { get; set; }
        [ForeignKey("OkulId")]
        public Okullar Okullar { get; set; }
        public int UniId { get; set; }
        [ForeignKey("UniId")]
        public Universiteler Universiteler { get; set; }
        public int UlkeId { get; set; }
        [ForeignKey("UlkeId")]
        public Ulkeler Ulkeler { get; set; }
        public int SehirId { get; set; }
        [ForeignKey("SehirId")]
        public Sehirler Sehirler { get; set; }
        public int OgretmenId { get; set; }
        [ForeignKey("OgretmenId")]
        public Ogretmenler Ogretmenler { get; set; }
        public int OkutmanId { get; set; }
        [ForeignKey("OkutmanId")]
        public Okutmanlar Okutmanlar { get; set; }

    }
}
