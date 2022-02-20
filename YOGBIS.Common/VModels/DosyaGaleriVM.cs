using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Common.VModels
{
    public class DosyaGaleriVM:BaseVM
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid DosyaGaleriId { get; set; }
        public string DosyaAdi { get; set; }
        public string DosyaURL { get; set; }
        public string KaydedenId { get; set; }
        public KullaniciVM Kullanici { get; set; }
    }
}
