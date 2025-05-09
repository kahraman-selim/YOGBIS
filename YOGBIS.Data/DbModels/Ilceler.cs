using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Ilceler : Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid IlceId { get; set; }
        public string IlceAdi { get; set; }

        public Guid IlId { get; set; }
        [ForeignKey("IlId")]
        public virtual Iller Il { get; set; }

        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public virtual Kullanici Kullanici { get; set; }

        public virtual ICollection<IkametAdresleri> IkametAdresleri { get; set; }
    }
}
