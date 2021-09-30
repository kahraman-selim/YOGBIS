using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

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
        public string KullaniciId { get; set; }
        [ForeignKey("KullaniciId")]
        public Kullanici Kullanici { get; set; }
    }
}
