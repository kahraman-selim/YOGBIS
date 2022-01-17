using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class UlkeGruplari:Base
    {
        [Key]
        public int UlkeGrupId { get; set; }
        public string UlkeGrupAdi { get; set; }
        public string UlkeGrupAciklama { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }        
        public int UlkeId { get; set; }
        public int KitaId { get; set; }
        public List<Kitalar> Kitalars { get; set; }        
    }
}
