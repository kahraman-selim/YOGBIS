using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Ogrenciler:Base
    {
        [Key]
        public int OgrencilerId { get; set; }
        public int SinifId { get; set; }
        [ForeignKey("SinifId")]
        public Siniflar Siniflar { get; set; }
        public string Uyruk { get; set; }
        public bool Cinsiyet { get; set; }
        public DateTime OkulKayitTarihi { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
    }
}
