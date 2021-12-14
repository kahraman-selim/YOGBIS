using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class UlkeGruplariVM:BaseVM
    {
        [Key]
        public int UlkeGrupId { get; set; }
        [Required (ErrorMessage ="Grup adı zorunlu bir alandır")]
        public string UlkeGrupAdi { get; set; }
        public string UlkeGrupAciklama { get; set; }
        public string KullaniciId { get; set; }
        public string KullaniciAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
        public List<UlkeGruplariKitalarVM> UlkeGruplariKitalars { get; set; }
        //VievModel için oluşturma methodu
        //public void SetUlkeGruplari(string ulkeGrupAdi) 
        //{
        //    this.UlkeGrupAdi = ulkeGrupAdi;
        //}
    }
}
