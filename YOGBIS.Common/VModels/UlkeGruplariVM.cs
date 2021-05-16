using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YOGBIS.Common.VModels
{
    public class UlkeGruplariVM
    {
        [Key]
        public int UlkeGrupId { get; set; }
        [Required]
        public string UlkeGrupAdi { get; protected set; }
        public string UlkeGrupAciklama { get; protected set; }

        //VievModel için oluşturma methodu
        public void SetUlkeGruplari(string ulkeGrupAdi) 
        {
            this.UlkeGrupAdi = ulkeGrupAdi;
        }
    }
}
