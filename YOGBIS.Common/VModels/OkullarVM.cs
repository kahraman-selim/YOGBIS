using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YOGBIS.Common.VModels
{
    public class OkullarVM:BaseVM
    {
        [Key]
        public int OkulId { get; set; }
        public int OkulKodu { get; set; }
        public string OkulAdi { get; set; }
    }
}
