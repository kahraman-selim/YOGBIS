namespace YOGBIS.Data.DbModels
{
    public class UlkeGruplariKitalar
    {
        public int UlkeGrupId { get; set; }
        public UlkeGruplari UlkeGruplari { get; set; }
        public int KitaId { get; set; }
        public Kitalar Kitalar { get; set; }
    }
}
