using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Personeller : Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public string PersonelGrupAdi { get; set; }
        public string PersonelAdiSoyadi { get; set; }
        public string PersonelGorevi { get; set; }
        public string PersonelUnvan { get; set; }
        public string PersonelTelefon { get; set; }
        public string PersonelEPosta { get; set; }
        public string PersonelGorevAlani { get; set; }
        public string PersonelGorevYeri { get; set; }

        public Guid? TemsilcilikId { get; set; }
        [ForeignKey("TemsilcilikId")]
        public virtual Temsilcilikler Temsilcilik { get; set; }

        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public virtual Kullanici Kullanici { get; set; }
    }
}
