using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using YOGBIS.Data.DbModels;
using System.Collections.Generic;

namespace YOGBIS.Common.VModels
{
    public class KomisyonlarVM:BaseVM
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid KomisyonId { get; set; }
        public string KomisyonKullaniciId { get; set; }
        public int KomisyonSiraNo { get; set; }
        public string KomisyonAdi { get; set; }
        public bool KomisyonUyeDurum { get; set; }
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
        public string KaydedenId { get; set; }
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
        public List<AdaylarVM> Adaylar { get; set; }
    }
}
