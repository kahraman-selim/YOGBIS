using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class IllerMdEPosta:Base
    {
        [Key]
        public int Id { get; set; }
        public int IlId { get; set; }
        [ForeignKey("IlId")]
        public Iller Iller { get; set; }
        public string IlEPostaAdres { get; set; }
        public string IlEpostaAdresAciklama { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
    }
}
