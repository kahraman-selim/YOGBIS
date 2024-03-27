using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class MulakatSorulari:Base
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int MulakatSorulariId { get; set; } //accessden kimlik ile birleştir
        public int SoruSiraNo { get; set; }
        //public Guid SoruId { get; set; }
        public int SoruNo { get; set; }
        //public Guid DereceId { get; set; }
        public int KategoriID { get; set; }
        public string KategoriAdi { get; set; }
        public int SoruDereceId { get; set; }
        public string SoruDereceAdi { get; set; }    
        public string Soru { get; set; }
        public string Cevap { get; set; }
        public int SinavKategoriID { get; set; }
        public string SinavKategoriAdi { get; set; }
        public string MulakatId { get; set; }

        //[ForeignKey("MulakatId")]
        //public Mulakatlar Mulakatlar { get; set; }
        //public int? SorulanAdayTC { get; set; }
        
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
    }
}
