﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Common.VModels
{
    public class SiniflarVM:BaseVM
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid SinifId { get; set; }
        public string SinifAdi { get; set; }
        public string SinifGrubu { get; set; }
        public DateTime SinifAcilisTarihi { get; set; }
        public Guid OkulId { get; set; }
        public OkullarVM Okullar { get; set; }
        public string KaydedenAdi { get; set; }
        public string KaydedenId { get; set; }        
        public KullaniciVM Kullanici { get; set; }
        public List<SubelerVM> Subeler { get; set; }
        public List<OgrencilerVM> Ogrenciler { get; set; }
    }
}
