using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Komisyonlar : Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid KomisyonId { get; set; }
        public string KomisyonKullaniciId { get; set; }
        public int KomisyonSiraNo { get; set; }
        public string KomisyonAdi { get; set; }
        public string KomisyonUyeDurum { get; set; }
        public bool KomisyonGorevDurum { get; set; }
        public int KomisyonUyeSiraNo { get; set; }
        public string KomisyonUyeGorevi { get; set; }
        public string KomisyonUyeAdiSoyadi { get; set; }
        public string KomisyonUyeGorevYeri { get; set; }        
        public string KomisyoUyeStatu { get; set; }
        public string KomisyonUlkeGrubu { get; set; }
        public string KomisyoUyeTel { get; set; }
        public string KomisyonUyeEPosta { get; set; }
        public DateTime KomisyonGorevBaslamaTarihi { get; set; }
        public DateTime KomisyonGorevBitisTarihi { get; set; }
        public Guid? IlgiliPersonelId { get; set; }

        public Guid? MulakatId { get; set; }
        [ForeignKey("MulakatId")]
        public virtual Mulakatlar Mulakat { get; set; }

        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public virtual Kullanici Kullanici { get; set; }
    }
}
