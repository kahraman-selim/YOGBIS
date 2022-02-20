using System;

namespace YOGBIS.Data.DbModels
{
    public class SoruDerece
    {
        public Guid SoruId { get; set; }
        public SoruBankasi SoruBankasi { get; set; }
        public Guid DereceId { get; set; }
        public SoruDereceler SoruDereceler { get; set; }
    }
}
