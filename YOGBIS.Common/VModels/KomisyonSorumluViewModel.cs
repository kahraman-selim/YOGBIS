using System;
using System.Collections.Generic;

namespace YOGBIS.Common.VModels
{
    public class KomisyonSorumluViewModel
    {
        public Guid PersonelId { get; set; }
        public string PersonelAdSoyad { get; set; }
        public List<string> KomisyonListesi { get; set; }
    }
}
