using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YOGBIS.Data.DbModels
{
    public class UlkeGruplari
    {
        [Key]
        public int UlkeGrupId { get; set; }
        public string UlkeGrupAdi { get; set; }
        public string UlkeGrupAciklama { get; set; }
    }
}
