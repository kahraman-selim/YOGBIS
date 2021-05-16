using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YOGBIS.Common.VModels
{
    public class SehirlerVM
    {
        [Key]
        public int SehirId { get; set; }
        public string SehirAdi { get; set; }
        public bool? Baskent { get; set; }
        public string SehirAciklama { get; set; }
        public int EyaletId { get; set; }
        public EyaletlerVM EyaletlerVm { get; set; }
    }
}
