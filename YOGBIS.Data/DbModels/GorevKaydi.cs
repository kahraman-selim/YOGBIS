using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YOGBIS.Data.DbModels
{
    public class GorevKaydi:Base
    {
        [Key]
        public int GorevId { get; set; }
        public string GörevliTC { get; set; }
        public string GörevAdi { get; set; }
        public DateTime? GorevBasTarihi { get; set; }
        public DateTime? GorevBitisTarihi { get; set; }
        public DateTime? GorevTarihi { get; set; }
        public string GorevOnaySayi { get; set; }
        public string GorevYeri { get; set; }
    }
}
