using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Common.VModels
{
    public class SoruBankasiVM:BaseVM
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid SoruBankasiId { get; set; }

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
        public Guid SoruKategorilerId { get; set; }
        public List<SoruKategoriVM> SoruKategori { get; set; }
        [Required(ErrorMessage = "Soru derecesini seçiniz...")]
        public Guid SoruDereceId { get; set; }
        public List<SoruDereceVM> SoruDerece { get; set; }
        [Required(ErrorMessage = "Onaylayacak kişi/kişileri seçiniz...")]
        public string[] OnaylayanId { get; set; }
        public int OnayDurumu { get; set; }
        public string OnayDurumuAciklama { get; set; }
        public SoruOnayVM SoruOnay { get; set; }
        public List<SoruBankasiLogVM> SoruBankasiLogVM { get; set; }
    }
}
