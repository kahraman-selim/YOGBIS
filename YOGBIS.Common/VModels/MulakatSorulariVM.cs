using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Common.VModels
{
    public class MulakatSorulariVM:BaseVM
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid MulakatSorulariId { get; set; }
        [Required(ErrorMessage = "Soru Sıra Numarası yazınız...")]
        public int SoruSiraNo { get; set; }
        [Required(ErrorMessage = "Soru Numarası yazınız...")]
        public int SoruNo { get; set; }
        [Required(ErrorMessage = "Soru Derecesini seçiniz...")]
        public Guid DereceId { get; set; }
        public SoruDerecelerVM SoruDereceler { get; set; }
        public string DereceAdi { get; set; }
        [Required(ErrorMessage = "Kategori seçimi yazınız...")]
        public Guid SoruKategorilerId { get; set; }
        public SoruKategorilerVM SoruKategoriler { get; set; }     
        public string SoruKategoriAdi { get; set; }
        public string SoruKategoriTakmaAdi { get; set; }
        public int SoruKategoriSiraNo { get; set; }
        [Required(ErrorMessage = "Soruyu yazınız...")]
        public string Soru { get; set; }
        [Required(ErrorMessage = "Cevabı yazınız...")]
        public string Cevap { get; set; }
        public int SinavKateogoriTurId { get; set; }
        public string SinavKategoriTurAdi { get; set; }
        public Guid MulakatId { get; set; }        
        public MulakatlarVM Mulakatlar { get; set; }
        public string MulakatDonemi { get; set; }
        public string KaydedenId { get; set; }
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
    }
}
