using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Kitalar : Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid KitaId { get; set; }
        public string KitaAdi { get; set; }
        public string KitaAciklama { get; set; }

        public Guid? UlkeGrupId { get; set; }
        [ForeignKey("UlkeGrupId")]
        public virtual UlkeGruplari UlkeGrup { get; set; }

        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public virtual Kullanici Kullanici { get; set; }

        public virtual ICollection<Ulkeler> Ulkeler { get; set; }
    }
}
