using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YOGBIS.Common.VModels
{
    public class MulakatlarVM
    {
        [Key]
        public int MulakatId { get; set; }
        public string OnaySayisi { get; set; }
        public string MulakatAdi { get; set; }
        public string Derecesi { get; set; }
        public DateTime BaslamaTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public int AdaySayisi { get; set; }
        public int SorulanSoruSayisi { get; set; }
        public string Durumu { get; set; }
        public string MulakatAciklama { get; set; }
    }
}
