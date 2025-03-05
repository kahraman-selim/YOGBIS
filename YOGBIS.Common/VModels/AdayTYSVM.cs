using System;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class AdayTYSVM : BaseVM
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "TC Kimlik No zorunludur")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "TC Kimlik No 11 haneli olmalıdır")]
        [Display(Name = "TC Kimlik No")]
        public string TC { get; set; }

        [Required(ErrorMessage = "TYS tarihi zorunludur")]
        [Display(Name = "TYS Tarihi")]
        public string TYSTarih { get; set; }

        [Required(ErrorMessage = "TYS saati zorunludur")]
        [Display(Name = "TYS Saati")]
        public string TYSSaat { get; set; }

        [Required(ErrorMessage = "TYS mülakat yeri zorunludur")]
        [Display(Name = "TYS Mülakat Yeri")]
        public string TYSMulakatYer { get; set; }

        [Display(Name = "TYS Durumu")]
        public string TYSDurumu { get; set; }

        [Display(Name = "TYS Durum Açıklaması")]
        public string TYSDurumAck { get; set; }

        [Display(Name = "TYS Komisyon Sıra No")]
        public int TYSKomisyonSiraNo { get; set; }

        [Display(Name = "TYS Komisyon Adı")]
        public string TYSKomisyonAdi { get; set; }

        public Guid? KomisyonId { get; set; }

        [Display(Name = "Komisyon")]
        public KomisyonlarVM Komisyonlar { get; set; }

        [Display(Name = "Komisyon Sıra No")]
        public int KomisyonSiraNo { get; set; }

        [Display(Name = "Komisyon SN")]
        public int KomisyonSN { get; set; }

        [Display(Name = "Komisyon Gün SN")]
        public int KomisyonGunSN { get; set; }

        [Display(Name = "Çağrı Durumu")]
        public bool? CagriDurum { get; set; }

        [Display(Name = "Kabul Durumu")]
        public bool? KabulDurum { get; set; }

        [Display(Name = "Sınav Durumu")]
        public bool? SinavDurum { get; set; }

        [Display(Name = "TYS Puanı")]
        public string TYSPuan { get; set; }

        [Display(Name = "TYS Sonucu")]
        public string TYSSonuc { get; set; }

        [Display(Name = "TYS Sonuç Açıklaması")]
        public string TYSSonucAck { get; set; }

        [Display(Name = "TYS Sorulan Soru No")]
        public int TYSSorulanSoruNo { get; set; }

        public Guid? MulakatId { get; set; }

        [Display(Name = "Mülakat")]
        public MulakatlarVM Mulakatlar { get; set; }

        [Display(Name = "Mülakat Onay No")]
        public string MulakatOnayNo { get; set; }

        [Required(ErrorMessage = "Kaydeden kişi zorunludur")]
        public string KaydedenId { get; set; }

        [Display(Name = "Kaydeden")]
        public KullaniciVM Kullanici { get; set; }
    }
}
