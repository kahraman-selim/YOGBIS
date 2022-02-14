namespace YOGBIS.Common.VModels
{
    public class DosyaGaleriVM:BaseVM
    {
        public int DosyaGaleriId { get; set; }
        public string DosyaAdi { get; set; }
        public string DosyaURL { get; set; }
        public string KaydedenId { get; set; }
        public KullaniciVM Kullanici { get; set; }
    }
}
