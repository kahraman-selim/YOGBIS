using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class GorevKararPdfGaleri:Base
    {
        [Key]
        public int Id { get; set; }
        public string GorevKararPdfUrl { get; set; }
        public int AdayGorevKaydiId { get; set; }
        [ForeignKey("AdayGorevKaydiId")]
        public AdayGorevKaydi AdayGorevKaydi { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
    }
}
