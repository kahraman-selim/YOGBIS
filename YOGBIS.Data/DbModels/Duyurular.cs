﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Duyurular:Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid DuyurularId { get; set; }
        public string DuyuruBaslık { get; set; }
        public string DuyuruAltBaslık { get; set; }
        public string DuyuruDetay { get; set; }
        public string DuyuruLink { get; set; }
        public string DuyuruKapakResimUrl { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
        public ICollection<FotoGaleri> FotoGaleri { get; set; }
        public ICollection<DosyaGaleri> DosyaGaleri { get; set; }
    }
}
