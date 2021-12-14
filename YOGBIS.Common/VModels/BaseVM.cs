using System;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class BaseVM
    {
        [DisplayFormat(DataFormatString = "{0,d}")]
        [DataType(DataType.DateTime)]
        public DateTime KayitTarihi { get; set; } = DateTime.Now;
    }
}
