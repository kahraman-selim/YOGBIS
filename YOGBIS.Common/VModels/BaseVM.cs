using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YOGBIS.Common.VModels
{
    public class BaseVM
    {
        [DisplayFormat(DataFormatString = "{0,d}")]
        [DataType(DataType.DateTime)]
        public DateTime KayitTarihi { get; set; } = DateTime.Now;
    }
}
