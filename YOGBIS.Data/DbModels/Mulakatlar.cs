﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Mulakatlar:Base
    {
        [Key]
        public int MulakatId { get; set; }
        public string OnaySayisi { get; set; }
        public string MulakatAdi { get; set; }
        public int DereceId { get; set; }
        [ForeignKey("DereceId")]
        public Dereceler Dereceler { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime BaslamaTarihi { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime BitisTarihi { get; set; }
        public int AdaySayisi { get; set; }
        public int SorulanSoruSayisi { get; set; }
        public bool? Durumu { get; set; }
        public string MulakatAciklama { get; set; }
        public string KullaniciId { get; set; }
        [ForeignKey("KullaniciId")]
        public Kullanici Kullanici { get; set; }
    }
}
