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
        public string UlkeAdi { get; set; }
        [Required(ErrorMessage = "Ülkenin Bayrağını yükleyiniz")]
        public IFormFile UlkeBayrak { get; set; }
        public string UlkeBayrakURL { get; set; }        
        public string UlkeBayrakAdi { get; set; }
        public string UlkeAciklama { get; set; }
        [Required(ErrorMessage = "Ülke Temsil durumunu seçiniz")]
        public bool Aktif { get; set; }
        public int VatandasSayisi { get; set; }

        [Required(ErrorMessage = "Kıta adı zorunlu bir alandır")]
        public Guid KitaId { get; set; }

        [Required(ErrorMessage = "Kıta adı zorunlu bir alandır")]
        public string KitaAdi { get; set; }
        public KitalarVM Kitalar { get; set; }
        public Guid UlkeGrupId { get; set; }
        public string UlkeGrupAdi { get; set; }
        public UlkeGruplariVM UlkeGruplari { get; set; }
        public string KaydedenId { get; set; }
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
        public List<EyaletlerVM> Eyaletler { get; set; }
        public List<SehirlerVM> Sehirler { get; set; }
        public IFormFileCollection FotoGaleris { get; set; }
        public List<FotoGaleriVM> FotoGaleri { get; set; }
    }
}
