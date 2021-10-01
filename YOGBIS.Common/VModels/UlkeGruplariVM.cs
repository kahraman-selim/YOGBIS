using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using YOGBIS.Data.DbModels;

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
        public KullaniciVM KullaniciVm { get; set; }
        public List<UlkeGruplariKitalarVM> UlkeGruplariKitalarsVm { get; set; }
        //VievModel için oluşturma methodu
        //public void SetUlkeGruplari(string ulkeGrupAdi) 
        //{
        //    this.UlkeGrupAdi = ulkeGrupAdi;
        //}
    }
}
