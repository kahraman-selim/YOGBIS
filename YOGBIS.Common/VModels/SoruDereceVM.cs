using System;
using System.Collections.Generic;
using System.Text;

namespace YOGBIS.Common.VModels
{
    public class SoruDereceVM
    {
        public int SoruId { get; set; }
        public SoruBankasiVM SoruBankasi { get; set; }
        public int DereceId { get; set; }
        public DerecelerVM Dereceler { get; set; }
    }
}
