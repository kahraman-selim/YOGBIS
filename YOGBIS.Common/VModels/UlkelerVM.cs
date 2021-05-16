using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YOGBIS.Common.VModels
{
    public class UlkelerVM
    {
        [Key]
        public int UlkeId { get; set; }
        public string UlkeAdi { get; set; }
        public string UlkeBayrak { get; set; }
        public string UlkeAciklama { get; set; }
        public int KitaId { get; set; }
        public KitalarVM KitalarVm { get; set; }
    }
}
