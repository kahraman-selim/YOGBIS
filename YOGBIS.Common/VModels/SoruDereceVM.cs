using System;

namespace YOGBIS.Common.VModels
{
    public class SoruDereceVM
    {
        public Guid SoruId { get; set; }
        public SoruBankasiVM SoruBankasi { get; set; }
        public Guid DereceId { get; set; }
        public SoruDerecelerVM SoruDereceler { get; set; }
    }
}
