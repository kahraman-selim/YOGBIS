using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class UlkeGruplari : Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid UlkeGrupId { get; set; }
        public string UlkeGrupAdi { get; set; }
        public string UlkeGrupAciklama { get; set; }

        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public virtual Kullanici Kullanici { get; set; }

        public virtual ICollection<Kitalar> Kitalar { get; set; }
    }
}
