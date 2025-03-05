using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class SSSCevap : Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid SSSCevapId { get; set; }
        public string SSSCevapDetay { get; set; }

        public Guid SSSId { get; set; }
        [ForeignKey("SSSId")]
        public virtual SSS Soru { get; set; }

        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public virtual Kullanici Kullanici { get; set; }
    }
}
