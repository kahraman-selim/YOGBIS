using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class KomisyonPersoneller:Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public string KomisyonKullaniciId { get; set; }
        public Guid PersonelId { get; set; }
        public Guid RolId { get; set; }
        [ForeignKey("KomisyonId")]
        public virtual Komisyonlar Komisyonlar { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public virtual Kullanici Kullanici { get; set; }
    }
}
