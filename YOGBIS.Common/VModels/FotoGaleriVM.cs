using System;
using System.Collections.Generic;
using System.Text;

namespace YOGBIS.Common.VModels
{
    public class FotoGaleriVM:BaseVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public int AktiviteId { get; set; }
        public AktivitelerVM Aktiviteler { get; set; }
    }
}
