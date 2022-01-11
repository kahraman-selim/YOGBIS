using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Personeller:Base
    {
        public int Id { get; set; }
        public string PersonelAdiSoyadi { get; set; }
        public string PersonelUnvan { get; set; }
        public string PersonelTelefon { get; set; }
        public string PersonelEPosta { get; set; }
        public string PersonelGorevAlani { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
    }
}
