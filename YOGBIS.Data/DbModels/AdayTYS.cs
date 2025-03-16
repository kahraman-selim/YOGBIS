using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class AdayTYS : Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public string TC { get; set; }
        public string TYSTarih { get; set; }        
        public string TYSSaat { get; set; }
        public string TYSMulakatYer { get; set; }
        public string TYSDurumu { get; set; }
        public string TYSDurumAck { get; set; }
        public int? TYSKomisyonSiraNo { get; set; }
        public string TYSKomisyonAdi { get; set; }
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
        public string TYSPuan { get; set; }
        public string TYSSonuc { get; set; }
        public string TYSSonucAck { get; set; }
        public int GorevlendirmeSiraNo { get; set; }
        public int? TYSSorulanSoruNo { get; set; }
        public bool? SinavIptal { get; set; }
        public string SinavIptalAck { get; set; }
        public Guid? BransId { get; set; }
        public string BransAdi { get; set; }
        public Guid? DereceId { get; set; }
        public string DereceAdi { get; set; }
        public Guid? UlkeTercihId { get; set; }
        public string UlkeTercihAdi { get; set; }
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
