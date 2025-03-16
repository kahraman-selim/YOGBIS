using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Common.VModels
{
    public class SoruKategorilerVM : BaseVM
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid SoruKategorilerId { get; set; }

        [Required(ErrorMessage = "Kategori sıra numarasını yazınız")]
        [Range(1, int.MaxValue, ErrorMessage = "Kategori sıra numarası 0'dan büyük olmalıdır")]
        public int SoruKategorilerSiraNo { get; set; }

        [Required(ErrorMessage = "Kategori adını yazınız")]
        [StringLength(100, ErrorMessage = "Kategori adı en fazla 100 karakter olabilir")]
        public string SoruKategorilerAdi { get; set; }

        [Required(ErrorMessage = "Kategori kullanımını seçiniz")]
        public string SoruKategorilerKullanimi { get; set; }

        [Required(ErrorMessage = "Kategori puanını yazınız")]
        [Range(0, 20, ErrorMessage = "Kategori puanı 0-20 arasında olmalıdır")]
        public int SoruKategorilerPuan { get; set; }

        [Required(ErrorMessage = "Derece seçiniz")]
        public Guid? DereceId { get; set; }
        public string DereceAdi { get; set; }
        public SoruDerecelerVM SoruDereceler { get; set; }

        public string SoruKategorilerTakmaAdi { get; set; }
        public string SoruKategorilerTamAdi { get; set; }
        public int SinavKateogoriTurId { get; set; }
        public string SinavKategoriTurAdi { get; set; }
       
        public string KaydedenId { get; set; }
        [Display(Name = "Kaydeden")]
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
        public List<SoruKategoriVM> SoruKategoris { get; set; }
    }
}
