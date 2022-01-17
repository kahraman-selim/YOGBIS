using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Kitalar
    {
        [Key]
        public int KitaId { get; set; }
        public string KitaAdi { get; set; }
        public string KitaAciklama { get; set; }
        public int UlkeGrupId { get; set; }
        [ForeignKey("UlkeGrupId")]
        public UlkeGruplari UlkeGruplari { get; set; }
        public List<Ulkeler> Ulkelers { get; set; }
    }
}
