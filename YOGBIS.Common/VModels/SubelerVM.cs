using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Common.VModels
{
    public class SubelerVM:BaseVM
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid SubeId { get; set; }
        public string SubeAdi { get; set; }
        public DateTime SubeAcilisTarihi { get; set; }
        public bool SubeDurumu { get; set; }        
        public Guid EgitimciId { get; set; }
        public Guid OkulId { get; set; }
        public string SinifAdi { get; set; }
        public Guid SinifId { get; set; }        
        public SiniflarVM Siniflar { get; set; }        
        public string KaydedenId { get; set; }
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
        public List<OgrencilerVM> Ogrencilers { get; set; }
    }
}
