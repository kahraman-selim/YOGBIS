using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class KomisyonLog:Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public Guid KomisyonId { get; set; }
        public int KomisyonUyeSiraNo { get; set; }
        public string KomisyonUyeAdSoyad { get; set; }
        public DateTime GorevBaslamaTarihi { get; set; }
        public DateTime GorevBitisTarihi { get; set; }
        [ForeignKey("KomisyonId")]
        public virtual Komisyonlar Komisyonlar { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public virtual Kullanici Kullanici { get; set; }
    }
}
