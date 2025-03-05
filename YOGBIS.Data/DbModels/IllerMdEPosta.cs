using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class IllerMdEPosta : Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public string IlEPostaAdres { get; set; }
        public string IlEpostaAdresAciklama { get; set; }

        public Guid IlId { get; set; }
        [ForeignKey("IlId")]
        public virtual Iller Il { get; set; }

        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public virtual Kullanici Kullanici { get; set; }
    }
}
