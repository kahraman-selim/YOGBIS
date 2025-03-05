using System;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class AdayMYSSVM : BaseVM
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "TC Kimlik No zorunludur")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "TC Kimlik No 11 haneli olmalıdır")]
        [Display(Name = "TC Kimlik No")]
        public string TC { get; set; }

        [Required(ErrorMessage = "MYSS tarihi zorunludur")]
        [Display(Name = "MYSS Tarihi")]
        public string MYSSTarih { get; set; }

        [Required(ErrorMessage = "MYSS saati zorunludur")]
        [Display(Name = "MYSS Saati")]
        public string MYSSSaat { get; set; }

        [Required(ErrorMessage = "MYSS mülakat yeri zorunludur")]
        [Display(Name = "MYSS Mülakat Yeri")]
        public string MYSSMulakatYer { get; set; }

        [Display(Name = "MYSS Durumu")]
        public string MYSSDurum { get; set; }

        [Display(Name = "MYSS Durum Açıklaması")]
        public string MYSSDurumAck { get; set; }

        [Display(Name = "MYSS Komisyon Sıra No")]
        public int MYSSKomisyonSiraNo { get; set; }

        [Display(Name = "MYSS Komisyon Adı")]
        public string MYSSKomisyonAdi { get; set; }

        public Guid? KomisyonId { get; set; }

        [Display(Name = "Komisyon")]
        public KomisyonlarVM Komisyonlar { get; set; }

        [Display(Name = "Komisyon Sıra No")]
        public int KomisyonSiraNo { get; set; }

        [Display(Name = "Komisyon Adı")]
        public string KomisyonAdi { get; set; }

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

        [Display(Name = "MYSS Puanı")]
        public string MYSSPuan { get; set; }

        [Display(Name = "MYSS Sonucu")]
        public string MYSSSonuc { get; set; }

        [Display(Name = "MYSS Sonuç Açıklaması")]
        public string MYSSSonucAck { get; set; }

        [Display(Name = "MYSS Sorulan Soru No")]
        public int MYSSSorulanSoruNo { get; set; }

        public Guid? MulakatId { get; set; }

        [Display(Name = "Mülakat")]
        public MulakatlarVM Mulakatlar { get; set; }

        [Display(Name = "Mülakat Onay No")]
        public string MulakatOnayNo { get; set; }

        [Required(ErrorMessage = "Kaydeden kişi zorunludur")]
        public string KaydedenId { get; set; }

        [Display(Name = "Kaydeden")]
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
    }
}
