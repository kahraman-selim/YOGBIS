using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class SoruKategori
    {
        public Guid SoruId { get; set; }
        [ForeignKey("SoruId")]
        public virtual SoruBankasi SoruBankasi { get; set; }

        public Guid KategoriId { get; set; }
        [ForeignKey("KategoriId")]
        public virtual SoruKategoriler SoruKategoriler { get; set; }
    }
}
