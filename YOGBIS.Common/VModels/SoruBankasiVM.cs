using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using YOGBIS.Common.ConstantsModels;

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

        [Required(ErrorMessage = "Onay durumunu seçiniz...")]
        public EnumsSoruOnay OnayDurumu { get; set; }
        public string OnayAciklama { get; set; }
        public string KaydedenId { get; set; }
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kaydeden { get; set; }
        public string OnaylayanId { get; set; }
        public string OnaylayanAdi { get; set; }
        public int DereceId { get; set; }
        public int SoruKategorilerId { get; set; }
        public KullaniciVM Onaylayan { get; set; }
        public List<SoruKategoriVM> SoruKategoris { get; set; }
        public List<SoruDereceVM> SoruDereces { get; set; }
    }
}
