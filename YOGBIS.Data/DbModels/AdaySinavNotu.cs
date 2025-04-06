using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    [Table("AdaySinavNotu")]
    public class AdaySinavNotu
    {
        [Key]
        public Guid Id { get; set; }
        public Guid AdayId { get; set; }
        public Guid KomisyonId { get; set; }
        public int Not1 { get; set; }
        public int Not2 { get; set; }
        public int Not3 { get; set; }
        public int Not4 { get; set; }
        public int Not5 { get; set; }
        public int Toplam1 { get; set; }
        public int Toplam2 { get; set; }
        public int Toplam3 { get; set; }
        public double Ortalama { get; set; }
        public double MYSPuan { get; set; }
        public string MYSSonuc { get; set; }
        public string KaydedenId { get; set; }
        public DateTime KayitTarihi { get; set; }
    }
}
