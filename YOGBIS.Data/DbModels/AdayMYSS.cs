using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class AdayMYSS : Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public string TC { get; set; }       

        public string MYSSTarih { get; set; }
        
        public string MYSSSaat { get; set; }
        public string MYSSMulakatYer { get; set; }
        public string MYSSDurum { get; set; }
        public string MYSSDurumAck { get; set; }
        public int? MYSSKomisyonSiraNo { get; set; }
        public string MYSSKomisyonAdi { get; set; }
        public Guid? KomisyonId { get; set; }
        
        [ForeignKey("KomisyonId")]
        public Komisyonlar Komisyonlar { get; set; }
        
        public int? KomisyonSN { get; set; }
        public int? KomisyonGunSN { get; set; }
        public bool? CagriDurum { get; set; }
        public bool? KabulDurum { get; set; }
        public bool? SinavDurum { get; set; }
        public bool? SinavaGelmedi { get; set; }
        public string SinavaGelmediAck { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal? MYSPuan { get; set; }
        
        public string MYSSonuc { get; set; }
        public string MYSSonucAck { get; set; }
        public int? MYSSSorulanSoruNo { get; set; }
        public bool? SinavIptal { get; set; }
        public string SinavIptalAck { get; set; }
        public Guid AdayId { get; set; }
        
        [ForeignKey("AdayId")]
        public virtual Adaylar Adaylar { get; set; }
        
        public Guid MulakatId { get; set; }
        
        [ForeignKey("MulakatId")]
        public Mulakatlar Mulakatlar { get; set; }
        
        public string KaydedenId { get; set; }        
        [ForeignKey("KaydedenId")]        
        public Kullanici Kullanici { get; set; }
    }
}
