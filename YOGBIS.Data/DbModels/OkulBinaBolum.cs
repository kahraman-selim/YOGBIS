using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class OkulBinaBolum:Base
    {
        public int OkulBinaBolumId { get; set; }
        public string BolumAdi { get; set; }
        public int OkulId { get; set; }
        [ForeignKey("OkulId")]
        public Okullar Okullar { get; set; }
        public ICollection<FotoGaleri> FotoGaleri { get; set; }
    }
}
