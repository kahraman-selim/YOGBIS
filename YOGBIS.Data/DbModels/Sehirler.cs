using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Sehirler : Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid SehirId { get; set; }
        public string SehirAdi { get; set; }
        public bool? Baskent { get; set; }
        public string SehirAciklama { get; set; }
        public int SehirVatandas { get; set; }

        public Guid TemsilciId { get; set; }
        [ForeignKey("TemsilciId")]
        public virtual Temsilcilikler Temsilcilik { get; set; }

        public Guid? EyaletId { get; set; }
        [ForeignKey("EyaletId")]
        public virtual Eyaletler Eyalet { get; set; }

        public Guid UlkeId { get; set; }
        [ForeignKey("UlkeId")]
        public virtual Ulkeler Ulke { get; set; }

        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public virtual Kullanici Kullanici { get; set; }

        public virtual ICollection<Okullar> Okullar { get; set; }
        public virtual ICollection<Universiteler> Universiteler { get; set; }
        public virtual ICollection<AdayGorevKaydi> Gorevliler { get; set; }
        public virtual ICollection<Ogrenciler> Ogrenciler { get; set; }
        public virtual ICollection<FotoGaleri> FotoGaleri { get; set; }
    }
}
