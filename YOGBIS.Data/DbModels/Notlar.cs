using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Notlar:Base
    {
        [Key]
        public int NotId { get; set; }
        public string NotAdi { get; set; }
        public string NotDetay { get; set; }
        public DateTime BaTarihi { get; set; }
        public DateTime BiTarihi { get; set; }
        public string NotRenk { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
    }
}
