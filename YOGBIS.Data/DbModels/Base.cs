using System;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Data.DbModels
{
    public class Base
    {
        [DisplayFormat(DataFormatString = "{0,d}")]
        [DataType(DataType.DateTime)]
        public DateTime KayitTarihi { get; set; } = DateTime.Now;
    }
}
