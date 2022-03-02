using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Common.VModels
{
    public class OkulBinaBolumVM:BaseVM
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid OkulBinaBolumId { get; set; }
        public string BolumAdi { get; set; }
        public int BolumAdedi { get; set; }
        public int BolumAdToplam { get; set; }
        public Guid OkulId { get; set; }
        public OkullarVM Okullar { get; set; }
        public string KaydedenId { get; set; }
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
        public IFormFileCollection FotoGaleris { get; set; }
        public List<FotoGaleriVM> FotoGaleri { get; set; }
    }
}
