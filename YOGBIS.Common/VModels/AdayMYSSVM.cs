using System;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class AdayMYSSVM : BaseVM
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "TC Kimlik No zorunludur")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "TC Kimlik No 11 haneli olmalıdır")]
        [Display(Name = "TC Kimlik No")]
        public string TC { get; set; }

        [Display(Name = "MYSS Tarihi")]
        public string MYSSTarih { get; set; }

        [Display(Name = "MYSS Saati")]
        public string MYSSSaat { get; set; }

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

        [Display(Name = "Komisyon")]
        public Guid? KomisyonId { get; set; }

        [Display(Name = "Komisyon SN")]
        public int? KomisyonSN { get; set; }

        [Display(Name = "Komisyon Gün SN")]
        public int? KomisyonGunSN { get; set; }

        [Display(Name = "Çağrı Durumu")]
        public bool CagriDurum { get; set; }

        [Display(Name = "Kabul Durumu")]
        public bool KabulDurum { get; set; }

        [Display(Name = "Sınav Durumu")]
        public bool SinavDurum { get; set; }

        [Display(Name = "Sınava Gelmedi")]
        public bool SinavaGelmedi { get; set; }

        [Display(Name = "Sınava Gelmedi Açıklaması")]
        public string SinavaGelmediAck { get; set; }

        [Display(Name = "MYS Puanı")]
        public string MYSPuan { get; set; }

        [Display(Name = "MYS Sonucu")]
        public string MYSSonuc { get; set; }

        [Display(Name = "MYS Sonuç Açıklaması")]
        public string MYSSonucAck { get; set; }

        [Display(Name = "MYSS Sorulan Soru No")]
        public int MYSSSorulanSoruNo { get; set; }

        [Display(Name = "Sınav İptal")]
        public bool SinavIptal { get; set; }

        [Display(Name = "Sınav İptal Açıklaması")]
        public string SinavIptalAck { get; set; }

        public Guid? AdayId { get; set; }

        public Guid? MulakatId { get; set; }

        [Required(ErrorMessage = "Kaydeden kişi zorunludur")]
        public string KaydedenId { get; set; }

        public int? MulakatYil { get; set; }

        [Display(Name = "Adaylar")]
        public AdaylarVM Adaylar { get; set; }

        [Display(Name = "Komisyonlar")]
        public KomisyonlarVM Komisyonlar { get; set; }

        [Display(Name = "Mülakatlar")]
        public MulakatlarVM Mulakatlar { get; set; }

        [Display(Name = "Kaydeden")]
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
    }
}
