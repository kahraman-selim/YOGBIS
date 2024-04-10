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
        [Required(ErrorMessage = "Öğretim türü zorunlu bir alandır")]
        public string OgretimTuru { get; set; }
        public string OkulLogoURL { get; set; }
        public IFormFile OkulLogo { get; set; }
        [Required(ErrorMessage = "Okul hakkında bilgi yazmalısınız")]
        public string OkulBilgi { get; set; }
        [Required(ErrorMessage = "Okul açılış tarihi zorunlu bir alandır")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
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
        public int SubeSayisi { get; set; }
        public int SinifSayisi { get; set; }
        public int OgrenciSayisi { get; set; }
        public Guid? SehirId { get; set; }
        public string SehirAdi { get; set; }
        public Guid? EyaletId { get; set; }
        public string EyaletAdi { get; set; }
        public Guid? TemsilcilikId { get; set; }
        [Required(ErrorMessage = "Ülkeyi seçiniz")]
        public Guid UlkeId { get; set; }
        public string UlkeAdi { get; set; }
        public string KaydedenId { get; set; }
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
        public List<OkulBinaBolumVM> OkulBinaBolum { get; set; }
        public List<SiniflarVM> Siniflar { get; set; }
        public List<SubelerVM> Subeler { get; set; }
        public int TCEOgr { get; set; }
        public int TCKOgr { get; set; }
        public int TCTopOgr { get; set; }
        public int DigEOgr { get; set; }
        public int DigKOgr { get; set; }
        public int DigTopOgr { get; set; }
        public int GenEOgr { get; set; }
        public int GenKOgr { get; set; }
        public int GenTopOgr { get; set; }
        public List<OgrencilerVM> Ogrenciler { get; set; }
        public List<AdayGorevKaydiVM> AdayGorevKaydi { get; set; }
        public List<EtkinliklerVM> Etkinlikler { get; set; }
        public List<EPostaAdresleriVM> EpostaAdresleri { get; set; }
        public List<TelefonlarVM> Telefonlar { get; set; }
        public IFormFileCollection FotoGaleris { get; set; }
        public List<FotoGaleriVM> FotoGaleri { get; set; }
    }
}
