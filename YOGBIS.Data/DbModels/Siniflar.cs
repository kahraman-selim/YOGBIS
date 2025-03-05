using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Siniflar : Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid SinifId { get; set; }
        public string SinifAdi { get; set; }
        public string SinifGrubu { get; set; }
        public DateTime SinifAcilisTarihi { get; set; }

        public Guid OkulId { get; set; }
        [ForeignKey("OkulId")]
        public virtual Okullar Okul { get; set; }

        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public virtual Kullanici Kullanici { get; set; }

        public virtual ICollection<Subeler> Subeler { get; set; }
        public virtual ICollection<Ogrenciler> Ogrenciler { get; set; }
    }
}
