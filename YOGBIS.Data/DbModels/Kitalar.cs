using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YOGBIS.Data.DbModels
{
    public class Kitalar
    {
        [Key]
        public int KitaId { get; set; }
        public string KitaAdi { get; set; }
        public string KitaAciklama { get; set; }
        [ForeignKey("UlkeGrupId")]
        public int UlkeGrupId { get; set; }
        public UlkeGruplari UlkeGruplari { get; set; }
    }
}
