using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Branslar : Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid BransId { get; set; }
        public string BransAdi { get; set; }

        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public virtual Kullanici Kullanici { get; set; }

        public virtual ICollection<AdayBasvuruBilgileri> AdayBasvuruBilgileri { get; set; }
        public virtual ICollection<UlkeTercihBranslar> UlkeTercihBranslar { get; set; }
    }
}
