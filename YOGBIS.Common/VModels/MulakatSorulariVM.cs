using System;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class MulakatSorulariVM : BaseVM
    {
        public Guid MulakatSorulariId { get; set; }

        [Required(ErrorMessage = "Soru sıra numarası zorunludur")]
        [Display(Name = "Soru Sıra No")]
        [Range(1, int.MaxValue, ErrorMessage = "Soru sıra numarası 0'dan büyük olmalıdır")]
        public int SoruSiraNo { get; set; }

        [Required(ErrorMessage = "Soru numarası zorunludur")]
        [Display(Name = "Soru No")]
        [Range(1, int.MaxValue, ErrorMessage = "Soru numarası 0'dan büyük olmalıdır")]
        public int SoruNo { get; set; }

        [Required(ErrorMessage = "Soru derecesi zorunludur")]
        [Display(Name = "Soru Derecesi")]
        public Guid DereceId { get; set; }

        [Display(Name = "Soru Derecesi")]
        public SoruDerecelerVM SoruDereceler { get; set; }

        [Display(Name = "Derece Adı")]
        public string DereceAdi { get; set; }

        [Required(ErrorMessage = "Soru kategorisi zorunludur")]
        [Display(Name = "Soru Kategorisi")]
        public Guid SoruKategorilerId { get; set; }

        [Display(Name = "Soru Kategorisi")]
        public SoruKategorilerVM SoruKategoriler { get; set; }

        [Display(Name = "Soru Kategori Adı")]
        public string SoruKategoriAdi { get; set; }

        [Display(Name = "Soru Kategori Takma Adı")]
        public string SoruKategoriTakmaAdi { get; set; }

        [Display(Name = "Soru Kategori Sıra No")]
        [Range(0, int.MaxValue, ErrorMessage = "Soru kategori sıra no 0'dan küçük olamaz")]
        public int SoruKategoriSiraNo { get; set; }

        [Required(ErrorMessage = "Soru zorunludur")]
        [Display(Name = "Soru")]
        public string Soru { get; set; }

        [Required(ErrorMessage = "Cevap zorunludur")]
        [Display(Name = "Cevap")]
        public string Cevap { get; set; }

        [Required(ErrorMessage = "Sınav kategori türü zorunludur")]
        [Display(Name = "Sınav Kategori Türü")]
        public int SinavKateogoriTurId { get; set; }

        [Display(Name = "Sınav Kategori Tür Adı")]
        public string SinavKategoriTurAdi { get; set; }

        [Display(Name = "İptal")]
        public bool? Iptal { get; set; }

        [Required(ErrorMessage = "Mülakat ID zorunludur")]
        public Guid MulakatId { get; set; }

        [Display(Name = "Mülakat")]
        public MulakatlarVM Mulakatlar { get; set; }

        [Display(Name = "Mülakat Dönemi")]
        public string MulakatDonemi { get; set; }

        [Required(ErrorMessage = "Kaydeden kişi zorunludur")]
        public string KaydedenId { get; set; }

        [Display(Name = "Kaydeden")]
        public string KaydedenAdi { get; set; }

        public KullaniciVM Kullanici { get; set; }
    }
}
