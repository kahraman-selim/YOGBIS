using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Ilceler:Base
    {
        [Key]
        public int IlceId { get; set; }
        public string IlceAdi { get; set; }
        public int IlId { get; set; }
        [ForeignKey("IlId")]
        public Iller Iller { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
    }
}
