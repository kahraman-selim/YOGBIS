using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Temsilcilikler:Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid TemsilcilikId { get; set; }
        public string TemsilcilikAdi { get; set; }
        public string TemsilciId { get; set; }
        public string TemsilcilikTel { get; set; }
        public string TemsilcilikEPosta { get; set; }
        public string TemsilcilikWebAdres { get; set; }
        public List<EPostaAdresleri> EpostaAdresleri { get; set; }
        public List<Telefonlar> Telefonlar { get; set; }
        public Guid SehirId { get; set; }
        [ForeignKey("SehirId")]
        public Sehirler Sehirler { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
    }
}
