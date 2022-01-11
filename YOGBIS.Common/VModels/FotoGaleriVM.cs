using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class FotoGaleriVM:BaseVM
    {
        [Key]
        public int FotoGaleriId { get; set; }
        public string FotoAdi { get; set; }
        public string FotoURL { get; set; }
        public int AktiviteId { get; set; }
        //public AktivitelerVM Aktiviteler { get; set; }
    }
}
