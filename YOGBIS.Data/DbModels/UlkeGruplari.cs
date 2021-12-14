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
        public string KullaniciId { get; set; }
        [ForeignKey("KullaniciId")]
        public Kullanici Kullanici { get; set; }
        public List<UlkeGruplariKitalar> UlkeGruplariKitalars { get; set; }
    }
}
