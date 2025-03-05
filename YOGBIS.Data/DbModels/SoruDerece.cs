using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class SoruDerece
    {
        public Guid SoruId { get; set; }
        [ForeignKey("SoruId")]
        public virtual SoruBankasi SoruBankasi { get; set; }

        public Guid DereceId { get; set; }
        [ForeignKey("DereceId")]
        public virtual SoruDereceler SoruDereceler { get; set; }
    }
}
