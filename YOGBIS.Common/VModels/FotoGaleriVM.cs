using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class FotoGaleriVM:BaseVM
    {
        [Key]
        public int FotoGaleriId { get; set; }
        public string FotoAdi { get; set; }
        public string FotoURL { get; set; }
        public string KaydedenId { get; set; }
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
    }
}
