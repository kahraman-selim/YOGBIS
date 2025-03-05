using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class SoruDereceler : Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid DereceId { get; set; }
        public string DereceAdi { get; set; }
        public int DereceKodu { get; set; }

        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public virtual Kullanici Kullanici { get; set; }

        public virtual ICollection<SoruDerece> SoruDerece { get; set; }
        public virtual ICollection<SoruKategoriler> SoruKategoriler { get; set; }
    }
}
