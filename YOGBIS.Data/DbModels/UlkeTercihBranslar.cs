using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class UlkeTercihBranslar:Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid TercihBransId { get; set; }
        public string BransAdi { get; set; }
        public Guid BransId { get; set; }
        [ForeignKey("BransId")]
        public Branslar Brans { get; set; }
        public string BransCinsiyet { get; set; }
        public int BransKontSayi { get; set; }
        public bool EsitBrans { get; set; }
        public string YabanciDil { get; set; }
        public Guid UlkeTercihId { get; set; }
        [ForeignKey("UlkeTercihId")]
        public UlkeTercih UlkeTercih { get; set; }       
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
    }
}
