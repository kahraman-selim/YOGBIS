using System;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class AdayTYSVM : BaseVM
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "TC Kimlik No zorunludur")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "TC Kimlik No 11 haneli olmalıdır")]
        [Display(Name = "TC Kimlik No")]
        public string TC { get; set; }

        [Display(Name = "TYS Tarihi")]
        public string TYSTarih { get; set; }

        [Display(Name = "TYS Saati")]
        public string TYSSaat { get; set; }

        [Display(Name = "TYS Mülakat Yeri")]
        public string TYSMulakatYer { get; set; }

        [Display(Name = "TYS Durumu")]
        public string TYSDurumu { get; set; }

        [Display(Name = "TYS Durum Açıklaması")]
        public string TYSDurumAck { get; set; }

        [Display(Name = "TYS Komisyon Sıra No")]
        public int? TYSKomisyonSiraNo { get; set; }

        [Display(Name = "TYS Komisyon Adı")]
        public string TYSKomisyonAdi { get; set; }

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

        [Display(Name = "TYS Puanı")]
        public string TYSPuan { get; set; }

        [Display(Name = "TYS Sonucu")]
        public string TYSSonuc { get; set; }

        [Display(Name = "TYS Sonuç Açıklaması")]
        public string TYSSonucAck { get; set; }

        public int GorevlendirmeSiraNo { get; set; }

        [Display(Name = "TYS Sorulan Soru No")]
        public int? TYSSorulanSoruNo { get; set; }

        [Display(Name = "Sınav İptal")]
        public bool SinavIptal { get; set; }

        [Display(Name = "Sınav İptal Açıklaması")]
        public string SinavIptalAck { get; set; }

        public Guid? BransId { get; set; }
        public string BransAdi { get; set; }

        public Guid? DereceId { get; set; }
        public string DereceAdi { get; set; }

        public Guid? UlkeTercihId { get; set; }
        public string UlkeTercihAdi { get; set; }

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
