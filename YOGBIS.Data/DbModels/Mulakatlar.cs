using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Mulakatlar:Base
    {
        [Key]
        public int MulakatId { get; set; }
        public string OnaySayisi { get; set; }
        public DateTime OnayTarihi { get; set; }
        public string KararSayisi { get; set; }
        public DateTime KararTarihi { get; set; }
        public string MulakatAdi { get; set; }
        public int DereceId { get; set; }
        [ForeignKey("DereceId")]
        public SoruDereceler Dereceler { get; set; }
        public DateTime BaslamaTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public int? AdaySayisi { get; set; }
        public int? SorulanSoruSayisi { get; set; }
        public bool Durumu { get; set; }
        public string MulakatAciklama { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
        public List<MulakatSorulari> MulakatSorularis { get; set; }
        public List<Adaylar> Adaylars { get; set; }
        public List<Komisyonlar> Komisyonlars { get; set; }
    }
}
