using System;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class AutoHistoryVM
    {
        public int Id { get; set; }
        public string RowId { get; set; }
        public string TableName { get; set; }
        public string Changed { get; set; }
        public int Kind { get; set; }

        [DisplayFormat(DataFormatString = "{0,d}")]
        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
