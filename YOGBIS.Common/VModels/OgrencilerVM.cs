﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Common.VModels
{
    public class OgrencilerVM:BaseVM
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid OgrencilerId { get; set; }
        [Required(ErrorMessage = "Sınıfı seçiniz")]
        public Guid SinifId { get; set; }
        public string SinifAdi { get; set; }
        public SiniflarVM Siniflar { get; set; }
        public string Uyruk { get; set; }
        public bool Cinsiyet { get; set; }
        public DateTime OkulKayitTarihi { get; set; }
        public string KaydedenId { get; set; }
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
    }
}
