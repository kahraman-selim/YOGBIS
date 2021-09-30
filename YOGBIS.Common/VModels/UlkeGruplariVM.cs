using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YOGBIS.Common.VModels
{
    public class UlkeGruplariVM:BaseVM
    {
        [Key]
        public int UlkeGrupId { get; set; }
        [Required]
        public string UlkeGrupAdi { get; set; }
        public string UlkeGrupAciklama { get; set; }
        public string KullaniciId { get; set; }
       
        public KullaniciVM KullaniciVm { get; set; }
        //VievModel için oluşturma methodu
        //public void SetUlkeGruplari(string ulkeGrupAdi) 
        //{
        //    this.UlkeGrupAdi = ulkeGrupAdi;
        //}
    }
}
