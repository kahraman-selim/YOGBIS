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
        [Required(ErrorMessage = "Duyuru başlığı zorunludur")]
        [Display(Name = "Duyuru Başlığı")]
        public string DuyuruBaslık { get; set; }
        public string DuyuruAltBaslık { get; set; }
        [Required(ErrorMessage = "Duyuru detayı zorunludur")]
        [Display(Name = "Duyuru Detayı")]
        public string DuyuruDetay { get; set; }
        public string DuyuruLink { get; set; }
        public IFormFile DuyuruKapakResim { get; set; }        
        public string DuyuruKapakResimUrl { get; set; }
        public string DuyuruKapakAdi { get; set; }
        [Required(ErrorMessage = "Kaydeden kişi zorunludur")]
        public string KaydedenId { get; set; }
        [Display(Name = "Kaydeden")]
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
        public IFormFileCollection FotoGaleris { get; set; }
        public List<FotoGaleriVM> FotoGaleri { get; set; }
        public IFormFileCollection DosyaGaleris { get; set; }
        public List<DosyaGaleriVM> DosyaGaleri { get; set; }
        [Required(ErrorMessage = "Başlangıç tarihi zorunludur")]
        [Display(Name = "Başlangıç Tarihi")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BasTarihi { get; set; }

        [Required(ErrorMessage = "Bitiş tarihi zorunludur")]
        [Display(Name = "Bitiş Tarihi")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BitTarihi { get; set; }

        [Display(Name = "Duyuru Durumu")]
        public bool? Durumu { get; set; }
    }
}
