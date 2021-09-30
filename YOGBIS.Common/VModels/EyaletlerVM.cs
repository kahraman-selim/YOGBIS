using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YOGBIS.Common.VModels
{
    public class EyaletlerVM:BaseVM
    {
        [Key]
        public int EyaletId { get; set; }
        public string EyaletAdi { get; set; }
        public string EyaletAciklama { get; set; }
        public int UlkeId { get; set; }
        public UlkelerVM UlkelerVm { get; set; }
        public string KullaniciId { get; set; }        
        public KullaniciVM KullaniciVm { get; set; }
    }
}
