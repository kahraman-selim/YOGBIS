using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Branslar:Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid BransId { get; set; }
        public string BransAdi { get; set; }
        public string BransCinsiyet { get; set; }
        public int BransKontSayi { get; set; }
        public bool EsitBrans { get; set; }        
        public Guid UlkeTercihId { get; set; }
        [ForeignKey("UlkeTercihId")]
        public UlkeTercih UlkeTercih { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
    }
}
