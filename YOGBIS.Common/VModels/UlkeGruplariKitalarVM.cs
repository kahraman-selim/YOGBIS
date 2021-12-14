namespace YOGBIS.Common.VModels
{
    public class UlkeGruplariKitalarVM
    {
        public int Id { get; set; }
        public int UlkeGrupId { get; set; }
        public UlkeGruplariVM UlkeGruplari { get; set; }
        public int KitaId { get; set; }
        public KitalarVM Kitalar { get; set; }
    }
}
