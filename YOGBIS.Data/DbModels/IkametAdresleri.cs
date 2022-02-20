using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class IkametAdresleri:Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public Guid IkametIlId { get; set; }
        public Guid IkametIlceId { get; set; }
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
