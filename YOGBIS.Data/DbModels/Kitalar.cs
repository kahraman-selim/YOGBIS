using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Kitalar
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid KitaId { get; set; }
        public string KitaAdi { get; set; }
        public string KitaAciklama { get; set; }
        public List<Ulkeler> Ulkelers { get; set; }
    }
}
