using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class OkulBinaBolum : Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid OkulBinaBolumId { get; set; }
        public string BolumAdi { get; set; }
        public int BolumAdedi { get; set; }

        public Guid OkulId { get; set; }
        [ForeignKey("OkulId")]
        public virtual Okullar Okul { get; set; }

        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public virtual Kullanici Kullanici { get; set; }

        public virtual ICollection<FotoGaleri> FotoGaleri { get; set; }
    }
}
