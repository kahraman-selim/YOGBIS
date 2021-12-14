using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Data.DbModels
{
    public class Kitalar
    {
        [Key]
        public int KitaId { get; set; }
        public string KitaAdi { get; set; }
        public string KitaAciklama { get; set; }
        public List<UlkeGruplariKitalar> UlkeGruplariKitalars { get; set; }
    }
}
