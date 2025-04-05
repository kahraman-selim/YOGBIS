using System;
using System.Collections.Generic;
using System.Text;

namespace YOGBIS.Common.VModels
{
    public class AdayMulakatListeViewModel
    {
        public IEnumerable<KomisyonBaskanViewModel> KomisyonBaskanları { get; set; }
        public IEnumerable<YOGBIS.Common.VModels.AdayMYSSVM> AdayListesi { get; set; }
    }
}
