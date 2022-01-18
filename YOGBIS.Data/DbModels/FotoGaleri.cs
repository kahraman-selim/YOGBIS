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
        public int? EtkinlikId { get; set; }
        public int? OkulId { get; set; }
        public int? UniId { get; set; }
        public int? SehirId { get; set; }
        public int? UlkeId { get; set; }
        public int? AdayId { get; set; }
        public int? DuyurularId { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }

    }
}
