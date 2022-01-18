using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Duyurular:Base
    {
        [Key]
        public int DuyurularId { get; set; }
        public string DuyuruBaslık { get; set; }
        public string DuyuruDetay { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
        public ICollection<FotoGaleri> FotoGaleri { get; set; }
    }
}
