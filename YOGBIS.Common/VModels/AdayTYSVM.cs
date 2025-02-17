using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Common.VModels
{
    public class AdayTYSVM:BaseVM
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
        public int TYSKomisyonSiraNo { get; set; }
        public string TYSKomisyonAdi { get; set; }
        public Guid? KomisyonId { get; set; }
        public int KomisyonSiraNo { get; set; }
        public KomisyonlarVM Komisyonlar { get; set; }
        public int KomisyonSN { get; set; }
        public int KomisyonGunSN { get; set; }
        public bool? CagriDurum { get; set; }
        public bool? KabulDurum { get; set; }
        public bool? SinavDurum { get; set; }
        public string TYSPuan { get; set; }
        public string TYSSonuc { get; set; }
        public string TYSSonucAck { get; set; }
        public int TYSSorulanSoruNo { get; set; }
        public Guid? MulakatId { get; set; }
        public string MulakatOnayNo { get; set; }
        public MulakatlarVM Mulakatlar { get; set; }
        public string KaydedenId { get; set; }
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
    }
}
