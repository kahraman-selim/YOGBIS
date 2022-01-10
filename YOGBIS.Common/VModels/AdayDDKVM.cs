using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class AdayDDKVM:BaseVM
    {
        [Key]
        public int Id { get; set; }
        public int AdayId { get; set; }       
        public AdaylarVM Adaylar { get; set; }
        public int AdayTC { get; set; }
        //diğeralanlar
        public string KaydedenId { get; set; }
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
    }
}
