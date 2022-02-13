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
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }

    }
}
