using System;
using System.Collections.Generic;
using System.Text;

namespace YOGBIS.Common.VModels
{
    public class GecmisVM:AutoHistoryVM
    {
        public string KullaniciId { get; set; }       
        public KullaniciVM KullaniciVm { get; set; }
    }
}
