using System;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Data.DbModels
{
    public class AutoHistory
    {
        public int Id { get; set; }
        public string RowId { get; set; }
        public string TableName { get; set; }
        public string Changed { get; set; }
        public int Kind { get; set; }
        public DateTime Created { get; set; }
    }
}
