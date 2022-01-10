using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class AdayDDK:Base
    {
        [Key]
        public int Id { get; set; }
        public int AdayId { get; set; }
        [ForeignKey("AdayId")]
        public Adaylar Adaylar { get; set; }
        public int AdayTC { get; set; }
        //diğeralanlar
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
    }
}
