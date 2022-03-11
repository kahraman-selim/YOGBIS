using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Siniflar:Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid SinifId { get; set; }
        public string SinifAdi { get; set; }
        public DateTime SinifAcilisTarihi { get; set; }        
        public string SubeAdi { get; set; }
        public Guid SubeId { get; set; }
        [ForeignKey("SubeId")]
        public Subeler Subeler { get; set; }
        public Guid OkulId { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
        public List<Ogrenciler> Ogrenciler { get; set; }
        public ICollection<FotoGaleri> FotoGaleri { get; set; }
    }
}
