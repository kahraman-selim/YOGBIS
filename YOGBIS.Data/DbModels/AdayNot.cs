using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class AdayNot:Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public Guid AdayId { get; set; }
        [ForeignKey("AdayId")]
        public Adaylar Adaylar { get; set; }
        public string TC { get; set; }
        public Guid? KomisyonId { get; set; }
        public int KomisyonUyeSiraId { get; set; }
        public Guid? NotKategoriId { get; set; }
        public int Not { get; set; }
        public Guid? MulakatId { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
    }
}
