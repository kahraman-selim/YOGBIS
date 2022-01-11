﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Okullar:Base
    {
        [Key]
        public int OkulId { get; set; }
        public int OkulKodu { get; set; }
        public string OkulAdi { get; set; }
        public string OkulLogo { get; set; }        
        public bool? OkulLab { get; set; }
        public bool? OkulKutuphane { get; set; }
        public string OkulBilgi { get; set; }
        public DateTime? OkulAcilisTarihi { get; set; }
        public bool? OkulDurumu { get; set; }
        public int SehirId { get; set; }
        [ForeignKey("SehirId")]
        public Sehirler Sehirler { get; set; }
        public int EyaletId { get; set; }
        public Eyaletler Eyaletler { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
        public List<Subeler> Subelers { get; set; }
        public List<AdayGorevKaydi> AdayGorevKaydis { get; set; }
        public List<Etkinlikler> Aktivitelers { get; set; }
        public ICollection<FotoGaleri> FotoGaleri { get; set; }
    }
}
