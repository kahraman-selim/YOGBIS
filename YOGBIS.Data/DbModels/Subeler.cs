using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Subeler : Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid SubeId { get; set; }
        public string SubeAdi { get; set; }
        public DateTime SubeAcilisTarihi { get; set; }
        public bool SubeDurumu { get; set; }
        public string SinifAdi { get; set; }

        public Guid? EgitimciId { get; set; }
        [ForeignKey("EgitimciId")]
        public virtual Personeller Egitimci { get; set; }

        public Guid OkulId { get; set; }
        [ForeignKey("OkulId")]
        public virtual Okullar Okul { get; set; }

        public Guid SinifId { get; set; }
        [ForeignKey("SinifId")]
        public virtual Siniflar Sinif { get; set; }

        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public virtual Kullanici Kullanici { get; set; }

        public virtual ICollection<Ogrenciler> Ogrenciler { get; set; }
        public virtual ICollection<FotoGaleri> FotoGaleri { get; set; }
    }
}
