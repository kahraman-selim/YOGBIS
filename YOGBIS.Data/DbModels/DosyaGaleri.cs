using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class DosyaGaleri:Base
    {
        public int DosyaGaleriId { get; set; }
        public string DosyaAdi { get; set; }
        public string DosyaURL { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
    }
}
