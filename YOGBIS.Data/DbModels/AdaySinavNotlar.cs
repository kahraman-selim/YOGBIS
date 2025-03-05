using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class AdaySinavNotlar:Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public string TC { get; set; }
        public Guid? KomisyonId { get; set; }
        public int KomisyonUyeSiraId { get; set; }
        public Guid? NotKategoriId1 { get; set; }
        public int Not1 { get; set; }
        public Guid? NotKategoriId2 { get; set; }
        public int Not2 { get; set; }
        public Guid? NotKategoriId3 { get; set; }
        public int Not3 { get; set; }
        public Guid? NotKategoriId4 { get; set; }
        public int Not4 { get; set; }
        public Guid? NotKategoriId5 { get; set; }
        public int Not5 { get; set; }
        public Guid AdayId { get; set; }
        [ForeignKey("AdayId")]
        public Adaylar Adaylar { get; set; }
        public Guid MulakatId { get; set; }
        [ForeignKey("MulakatId")]
        public Mulakatlar Mulakatlar { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
    }
}
