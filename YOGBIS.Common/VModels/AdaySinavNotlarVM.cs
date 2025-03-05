using System;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class AdaySinavNotlarVM : BaseVM
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "TC Kimlik No zorunludur")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "TC Kimlik No 11 haneli olmalıdır")]
        [Display(Name = "TC Kimlik No")]
        public string TC { get; set; }

        public Guid? KomisyonId { get; set; }

        [Display(Name = "Komisyon")]
        public KomisyonlarVM Komisyon { get; set; }

        [Display(Name = "Komisyon Üye Sıra No")]
        public int KomisyonUyeSiraId { get; set; }

        public Guid? NotKategoriId1 { get; set; }

        [Display(Name = "Not Kategorisi 1")]
        public SoruKategorilerVM NotKategorisi1 { get; set; }

        [Range(0, 100, ErrorMessage = "Not 0-100 arasında olmalıdır")]
        [Display(Name = "Not 1")]
        public int Not1 { get; set; }

        public Guid? NotKategoriId2 { get; set; }

        [Display(Name = "Not Kategorisi 2")]
        public SoruKategorilerVM NotKategorisi2 { get; set; }

        [Range(0, 100, ErrorMessage = "Not 0-100 arasında olmalıdır")]
        [Display(Name = "Not 2")]
        public int Not2 { get; set; }

        public Guid? NotKategoriId3 { get; set; }

        [Display(Name = "Not Kategorisi 3")]
        public SoruKategorilerVM NotKategorisi3 { get; set; }

        [Range(0, 100, ErrorMessage = "Not 0-100 arasında olmalıdır")]
        [Display(Name = "Not 3")]
        public int Not3 { get; set; }

        public Guid? NotKategoriId4 { get; set; }

        [Display(Name = "Not Kategorisi 4")]
        public SoruKategorilerVM NotKategorisi4 { get; set; }

        [Range(0, 100, ErrorMessage = "Not 0-100 arasında olmalıdır")]
        [Display(Name = "Not 4")]
        public int Not4 { get; set; }

        public Guid? NotKategoriId5 { get; set; }

        [Display(Name = "Not Kategorisi 5")]
        public SoruKategorilerVM NotKategorisi5 { get; set; }

        [Range(0, 100, ErrorMessage = "Not 0-100 arasında olmalıdır")]
        [Display(Name = "Not 5")]
        public int Not5 { get; set; }

        [Required(ErrorMessage = "Aday seçimi zorunludur")]
        public Guid AdayId { get; set; }

        [Display(Name = "Aday")]
        public AdaylarVM Adaylar { get; set; }

        public Guid? MulakatId { get; set; }

        [Display(Name = "Mülakat")]
        public MulakatlarVM Mulakat { get; set; }

        [Required(ErrorMessage = "Kaydeden kişi zorunludur")]
        public string KaydedenId { get; set; }

        [Display(Name = "Kaydeden")]
        public string KaydedenAdi { get; set; }

        public KullaniciVM Kullanici { get; set; }
    }
}
