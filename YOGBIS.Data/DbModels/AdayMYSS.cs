using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class AdayMYSS:Base
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
        public int MYSSKomisyonSiraNo { get; set; }
        public string MYSSKomisyonAdi { get; set; }
        public Guid? KomisyonId { get; set; }
        [ForeignKey("KomisyonId")]
        public Komisyonlar Komisyonlar { get; set; }
        public int KomisyonSN { get; set; }
        public int KomisyonGunSN { get; set; }
        public bool? CagriDurum { get; set; }
        public bool? KabulDurum { get; set; }
        public bool? SinavDurum { get; set; }
        public string MYSSPuan { get; set; }
        public string MYSSSonuc { get; set; }
        public string MYSSSonucAck { get; set; }
        public int MYSSSorulanSoruNo { get; set; }
        public Guid? MulakatId { get; set; }
        public string MulakatOnayNo { get; set; }
        [ForeignKey("MulakatId")]
        public Mulakatlar Mulakatlar { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
    }
}
