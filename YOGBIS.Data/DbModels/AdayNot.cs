using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class AdayNot:Base
    {
        [Key]
        public int Id { get; set; }
        public int AdayId { get; set; }
        [ForeignKey("AdayId")]
        public Adaylar Adaylar { get; set; }
        public int AdayTC { get; set; }
        public int KomisyonId { get; set; }
        public int KomisyonUyeSiraId { get; set; }
        public int NotKategoriId { get; set; }
        public int Not { get; set; }
        public int MulakatId { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
    }
}
