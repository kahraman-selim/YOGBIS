using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Common.VModels
{
    public class SoruBankasiVM:BaseVM
    {
        [Key]
        public int SoruBankasiId { get; set; }

        [Required(ErrorMessage = "Kategoriyi seçiniz...")]
        public int SoruKategorilerId { get; set; }

        [Required(ErrorMessage = "Kategoriyi seçiniz...")]
        public string SoruKategorilerAdi { get; set; }
        public SoruKategorilerVM SoruKategoriler { get; set; }

        [Required(ErrorMessage = "Soruyu yazınız...")]
        public string Soru { get; set; }

        [Required(ErrorMessage = "Cevabı yazınız...")]
        public string Cevap { get; set; }

        [Required(ErrorMessage = "Derecesini seçiniz...")]
        public int DereceId { get; set; }

        [Required(ErrorMessage = "Derecesini seçiniz...")]
        public string DereceAdi { get; set; }
        public DerecelerVM Dereceler { get; set; }
        public int SorulmaSayisi { get; set; } = 0;

        [Required(ErrorMessage = "Soru durumunu seçiniz...")]
        public bool SoruDurumu { get; set; } = true;
        public EnumsSoruOnay OnayDurumu { get; set; }
        public string OnayDurumuAciklama { get; set; }
        public string OnayAciklama { get; set; }
        public string KaydedenId { get; set; }
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kaydeden { get; set; }
        public string OnaylayanId { get; set; }
        public string OnaylayanAdi { get; set; }
        public KullaniciVM Onaylayan { get; set; }
        public List<SoruKategori> SoruKategoris { get; set; }
    }
}
