using System;
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
        public Guid SehirId { get; set; }
        [ForeignKey("SehirId")]
        public Sehirler Sehirler { get; set; }
        public string TemsilciId { get; set; }
        public string TemsilcilikTel { get; set; }
        public string TemsilcilikEPosta { get; set; }
        public string TemsilcilikWebAdres { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
    }
}
