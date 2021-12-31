using System;
using System.Collections.Generic;
using System.Text;

namespace YOGBIS.Data.DbModels
{
    public class FotoGaleri:Base
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public int AktiviteId { get; set; }
        public Aktiviteler Aktiviteler { get; set; }
    }
}
