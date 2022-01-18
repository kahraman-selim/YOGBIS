using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class SSSCevap:Base
    {
        [Key]
        public int SSSCevapId { get; set; }
        public int SSSId { get; set; }
        [ForeignKey("SSSId")]
        public SSS SSS { get; set; }
        public string SSSCevapDetay { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
    }
}
