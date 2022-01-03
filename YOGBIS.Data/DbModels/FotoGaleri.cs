using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Data.DbModels
{
    public class FotoGaleri:Base
    {
        [Key]
        public int FotoGaleriId { get; set; }
        public string FotoAdi { get; set; }
        public string FotoURL { get; set; }        
        public int AktiviteId { get; set; }
        public Aktiviteler Aktiviteler { get; set; }
    }
}
