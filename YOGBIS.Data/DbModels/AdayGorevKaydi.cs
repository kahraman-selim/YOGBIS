﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class AdayGorevKaydi:Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid AdayGorevKaydiId { get; set; }
        public Guid DereceId { get; set; }
        public string GorevliTC { get; set; }
        public string GorevliAdi { get; set; }
        public string GorevliSoyadi { get; set; }        
        public string Gorevi { get; set; }
        public Guid BransId { get; set; }      
        public string GorevOnaySayi { get; set; }
        public DateTime GorevOnayTarihi { get; set; }
        public DateTime? GorevlendirilmeTarihi { get; set; }
        public DateTime? GorevBasTarihi { get; set; }
        public DateTime? GorevBitisTarihi { get; set; }
        public int GorevDurumu { get; set; }
        public string GorevAciklama { get; set; }        
        public Guid? OkulId { get; set; }
        public Guid? UniId { get; set; }
        public Guid? TemsilcilikId { get; set; }
        public Guid? SehirId { get; set; }        
        public Guid? EyaletId { get; set; }
        public Guid? MulakatId { get; set; }
        public Guid UlkeId { get; set; }
        [ForeignKey("UlkeId")]
        public Ulkeler Ulkeler { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
        public ICollection<GorevKararPdfGaleri> GorevKararPdfGaleri { get; set; }
        public ICollection<FotoGaleri> FotoGaleri { get; set; }
        public List<Ogrenciler> Ogrenciler { get; set; }
    }
}
