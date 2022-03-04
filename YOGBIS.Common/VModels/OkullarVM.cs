using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Common.VModels
{
    public class OkullarVM:BaseVM
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid OkulId { get; set; }

        [Required(ErrorMessage = "Okul kodu zorunlu bir alandır")]
        public int OkulKodu { get; set; }

        [Required(ErrorMessage = "Okul adı zorunlu bir alandır")]
        public string OkulAdi { get; set; }

        [Required(ErrorMessage = "Okul türü zorunlu bir alandır")]
        public string OkulTuru { get; set; }
        public string OkulLogoURL { get; set; }
        public IFormFile OkulLogo { get; set; }
        [Required(ErrorMessage = "Okul hakkında bilgi yazmalısınız")]
        public string OkulBilgi { get; set; }
        [Required(ErrorMessage = "Okul açılış tarihi zorunlu bir alandır")]
        public DateTime OkulAcilisTarihi { get; set; }
        public bool OkulDurumu { get; set; }
        public string OkulMudurId { get; set; }
        public string OkulMudurAdiSoyadi { get; set; }
        public string OkulHizmetGecisDonem { get; set; }
        public string OkulKapaliAlan { get; set; }
        public string OkulAcikAlan { get; set; }
        public bool? OkulMulkiDurum { get; set; }
        public string OkulMulkiDurumAciklama { get; set; }
        public string OkulInternetAdresi { get; set; }
        public string OkulEPostaAdresi { get; set; }
        public string OkulTelefon { get; set; }

        [Required(ErrorMessage = "Ülkeyi seçiniz")]
        public Guid? OkulUlkeId { get; set; }
        public string OkulUlkeAdi { get; set; }
        [Required(ErrorMessage = "Şehir adı zorunlu bir alandır")]
        public Guid? SehirId { get; set; }
        public string SehirAdi { get; set; }
        public SehirlerVM Sehirler { get; set; }
        public Guid? EyaletId { get; set; }
        public string EyaletAdi { get; set; }
        public EyaletlerVM Eyaletler { get; set; }
        public string KaydedenId { get; set; }
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
        public List<OkulBinaBolumVM> OkulBinaBolum { get; set; }
        public List<SubelerVM> Subeler { get; set; }
        public List<AdayGorevKaydiVM> AdayGorevKaydi { get; set; }
        public List<EtkinliklerVM> Etkinlikler { get; set; }
        public List<EPostaAdresleriVM> EpostaAdresleri { get; set; }
        public List<TelefonlarVM> Telefonlar { get; set; }
        public IFormFileCollection FotoGaleris { get; set; }
        public List<FotoGaleriVM> FotoGaleri { get; set; }
    }
}
