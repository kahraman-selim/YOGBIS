using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Common.VModels
{
    public class MulakatSorulariVM:BaseVM
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid MulakatSorulariId { get; set; }
                
        //[Required(ErrorMessage = "Soru Sıra Numarası yazınız...")]
        public int SoruSiraNo { get; set; }

        //[Required(ErrorMessage = "Soru Numarası yazınız...")]
        //public Guid SoruId { get; set; }

        public int SoruNo { get; set; }

        //[Required(ErrorMessage = "Kategori seçimi yazınız...")]
        //public Guid SoruKategoriId { get; set; }
        public int KategoriID { get; set; }
        public string KategoriAdi { get; set; }

        //[Required(ErrorMessage = "Soru Derecesini seçiniz...")]
        //public Guid DereceId { get; set; }
        public int SoruDereceId { get; set; }
        public string SoruDereceAdi { get; set; }
       
        //[Required(ErrorMessage = "Soruyu yazınız...")]
        public string Soru { get; set; }

        //[Required(ErrorMessage = "Cevabı yazınız...")]
        public string Cevap { get; set; }
        public int SinavKategoriID { get; set; }
        public string SinavKategoriAdi { get; set; }
        public string MulakatId { get; set; }
        //public string MulakatAdi { get; set; }
        //public MulakatlarVM Mulakatlar { get; set; }
        public string KaydedenId { get; set; }
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
    }
}
