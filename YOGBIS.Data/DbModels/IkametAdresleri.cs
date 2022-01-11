using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class IkametAdresleri:Base
    {
        [Key]
        public int Id { get; set; }
        public int IkametIlId { get; set; }
        [ForeignKey("IkametIlId")]
        public Iller Iller { get; set; }
        public int IkametIlceId { get; set; }
        [ForeignKey("IkametIlceId")]
        public Ilceler Ilceler { get; set; }
        public string PostaKodu { get; set; }
        public string AdresTuru { get; set; }
        public string IkametAdresi { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
    }
}
