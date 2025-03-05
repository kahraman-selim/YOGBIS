using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class DosyaGaleri : Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid DosyaGaleriId { get; set; }
        public string DosyaAdi { get; set; }
        public string DosyaURL { get; set; }

        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public virtual Kullanici Kullanici { get; set; }

        public Guid? AdayId { get; set; }
        [ForeignKey("AdayId")]
        public virtual Adaylar Aday { get; set; }

        public Guid? DuyuruId { get; set; }
        [ForeignKey("DuyuruId")]
        public virtual Duyurular Duyuru { get; set; }

        public Guid? EtkinlikId { get; set; }
        [ForeignKey("EtkinlikId")]
        public virtual Etkinlikler Etkinlik { get; set; }
    }
}
