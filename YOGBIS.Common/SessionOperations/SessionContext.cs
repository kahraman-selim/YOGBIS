using System;
using System.Collections.Generic;
using System.Text;

namespace YOGBIS.Common.SessionOperations
{
    public class SessionContext
    {
        public string GirisId { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string EPosta { get; set; }
        public bool? AdminMi { get; set; }
    }
}
