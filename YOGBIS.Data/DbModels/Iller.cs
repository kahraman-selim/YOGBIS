using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Iller:Base
    {
        [Key]
        public int IlId { get; set; }
        public string PlakaKodu { get; set; }
        public string IlAdi { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
        public List<IllerMdEPosta> IllerMdEpostas { get; set; }
    }
}
