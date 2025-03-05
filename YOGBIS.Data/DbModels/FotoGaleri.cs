using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class FotoGaleri : Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid FotoGaleriId { get; set; }
        public string FotoAdi { get; set; }
        public string FotoURL { get; set; }

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

        public Guid? OkulBinaBolumId { get; set; }
        [ForeignKey("OkulBinaBolumId")]
        public virtual OkulBinaBolum OkulBinaBolum { get; set; }

        public Guid? SubeId { get; set; }
        [ForeignKey("SubeId")]
        public virtual Subeler Sube { get; set; }

        public Guid? UlkeId { get; set; }
        [ForeignKey("UlkeId")]
        public virtual Ulkeler Ulke { get; set; }
    }
}
