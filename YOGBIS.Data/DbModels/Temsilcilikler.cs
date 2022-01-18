using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Temsilcilikler:Base
    {
        [Key]
        public int TemsilcilikId { get; set; }
        public string TemsilcilikAdi { get; set; }
        public int SehirId { get; set; }
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
