using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Common.VModels
{
    public class DuyurularVM:BaseVM
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid DuyurularId { get; set; }
        public string DuyuruBaslık { get; set; }
        public string DuyuruAltBaslık { get; set; }
        public string DuyuruDetay { get; set; }
        public string DuyuruLink { get; set; }
        public IFormFile DuyuruKapakResim { get; set; }        
        public string DuyuruKapakResimUrl { get; set; }
        public string DuyuruKapakAdi { get; set; }
        public string KaydedenId { get; set; }
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
        public IFormFileCollection FotoGaleris { get; set; }
        public ICollection<FotoGaleriVM> FotoGaleri { get; set; }
        public IFormFileCollection DosyaGaleris { get; set; }
        public ICollection<DosyaGaleriVM> DosyaGaleri { get; set; }
    }
}
