using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Common.VModels
{
    public class UlkelerVM:BaseVM
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid UlkeId { get; set; }
        [Required(ErrorMessage = "Ülke kodu zorunlu bir alandır")]
        public string UlkeKodu { get; set; }
        [Required (ErrorMessage ="Ülke adı zorunlu bir alandır")]
        [Display(Name = "Ülke Adı")]
        public string UlkeAdi { get; set; }
        [Required(ErrorMessage = "Ülkenin Bayrağını yükleyiniz")]
        public IFormFile UlkeBayrak { get; set; }
        public string UlkeBayrakURL { get; set; }        
        public string UlkeBayrakAdi { get; set; }
        [Display(Name = "Ülke Açıklaması")]
        public string UlkeAciklama { get; set; }
        [Required(ErrorMessage = "Ülke Temsil durumunu seçiniz")]
        public bool Aktif { get; set; }
        public int VatandasSayisi { get; set; }

        [Required(ErrorMessage = "Kıta adı zorunlu bir alandır")]
        public Guid KitaId { get; set; }

        [Required(ErrorMessage = "Kıta adı zorunlu bir alandır")]
        public string KitaAdi { get; set; }
        [Display(Name = "Kıta")]
        public KitalarVM Kitalar { get; set; }
        public Guid UlkeGrupId { get; set; }
        public string UlkeGrupAdi { get; set; }
        public UlkeGruplariVM UlkeGruplari { get; set; }
        [Required(ErrorMessage = "Kaydeden kişi zorunludur")]
        public string KaydedenId { get; set; }
        [Display(Name = "Kaydeden")]
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
        public List<EyaletlerVM> Eyaletler { get; set; }
        [Display(Name = "Şehirler")]
        public virtual ICollection<SehirlerVM> Sehirlers { get; set; }
        [Display(Name = "Okullar")]
        public virtual ICollection<OkullarVM> Okullars { get; set; }
        public int OkulSayisi { get; set; }
        public int OgrenciOkulSayisi { get; set; }
        public List<OkullarVM> Okullar { get; set; }
        public IFormFileCollection FotoGaleris { get; set; }
        public List<FotoGaleriVM> FotoGaleri { get; set; }
    }
}
