﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Common.VModels
{
    public class SSSVM:BaseVM
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid SSSId { get; set; }
        public string Soru { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public KullaniciVM Kullanici { get; set; }
    }
}
