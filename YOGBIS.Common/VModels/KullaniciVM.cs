using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class KullaniciVM
    {
        public string Id { get; set; }
        [Display(Name ="Kullanıcı Adı")]
        public string UserName { get; set; }
        [Display(Name = "E-Posta Adresi")]
        public string EMail { get; set; }
        [Display(Name = "Telefon Numarası")]
        public string PhoneNumber { get; set; }
        [StringLength(11,ErrorMessage ="TC Kimlik Numaranızı kontrol ediniz")]
        public int TcKimlikNo { get; set; }
        [Required (ErrorMessage ="{0} boş geçilemez")]
        public string Ad { get; set; }
        [Required(ErrorMessage = "{0} boş geçilemez")]
        public string Soyad { get; set; }
        public string AdSoyad { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime KayitTarihi { get; set; } = DateTime.Now;
        public int KulaniciAdDegLimiti { get; set; } = 10;        
        public byte[] KullaniciResim { get; set; }
        public string KullaniciResimYol { get; set; }
        public bool? Aktif { get; set; }

        #region BağlıTablolar
        public List<DerecelerVM> Derecelers { get; set; }
        public List<SoruKategorilerVM> SoruKategorilers { get; set; }
        public List<MulakatlarVM> Mulakatlars { get; set; }
        public List<AdaylarVM> Adaylars { get; set; }
        public List<UlkeGruplariVM> UlkeGruplaris { get; set; }
        public List<UlkelerVM> Ulkelers { get; set; }
        public List<EyaletlerVM> Eyaletlers { get; set; }
        public List<SehirlerVM> Sehirlers { get; set; }
        public List<OkullarVM> Okullars { get; set; }
        public List<NotlarVM> Notlars { get; set; }
        public List<OgretmenlerVM> Ogretmenlers { get; set; }
        public List<OkutmanlarVM> Okutmanlars { get; set; }
        public List<UniversitelerVM> Universitelers { get; set; }
        public List<SubelerVM> Subelers { get; set; }
        public List<SiniflarVM> Siniflars { get; set; }
        public List<OgrencilerVM> Ogrencilers { get; set; }
        public List<GorevKaydiVM> GorevKaydis { get; set; }
        #endregion
    }
}
