using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class SoruBankasiVM:BaseVM
    {
        [Key]
        public int SoruBankasiId { get; set; }

        [Required(ErrorMessage = "Soruyu yazınız...")]
        public string Soru { get; set; }

        [Required(ErrorMessage = "Cevabı yazınız...")]
        public string Cevap { get; set; }
        public int SorulmaSayisi { get; set; } = 0;

        [Required(ErrorMessage = "Soru durumunu seçiniz...")]
        public bool SoruDurumu { get; set; } = true;
        public string KaydedenId { get; set; }
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kaydeden { get; set; }
        [Required(ErrorMessage = "Soru kategorisini seçiniz...")]
        public int SoruKategorilerId { get; set; }
        public List<SoruKategoriVM> SoruKategori { get; set; }
        [Required(ErrorMessage = "Soru derecesini seçiniz...")]
        public int SoruDereceId { get; set; }
        public List<SoruDereceVM> SoruDerece { get; set; }
        [Required(ErrorMessage = "Onaylayacak kişi/kişileri seçiniz...")]
        public string OnaylayanId { get; set; }
        public int OnayDurumu { get; set; }
        public string OnayDurumuAciklama { get; set; }
        public List<SoruOnayVM> SoruOnay { get; set; }
        public List<SoruBankasiLogVM> SoruBankasiLogVM { get; set; }
    }
}
