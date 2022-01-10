namespace YOGBIS.Data.DbModels
{
    public class SoruDerece
    {
        public int SoruId { get; set; }
        public SoruBankasi SoruBankasi { get; set; }
        public int DereceId { get; set; }
        public SoruDereceler Dereceler { get; set; }
    }
}
