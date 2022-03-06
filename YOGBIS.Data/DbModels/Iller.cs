﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Iller:Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid IlId { get; set; }
        public string PlakaKodu { get; set; }
        public string IlAdi { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
        public List<IllerMdEPosta> IllerMdEposta { get; set; }
        public List<Ilceler> Ilceler { get; set; }
    }
}
