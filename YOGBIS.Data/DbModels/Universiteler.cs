using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Universiteler : Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid UniId { get; set; }
        public string UniAdi { get; set; }
        public bool YurtIciMi { get; set; }
        public string UniStatu { get; set; }
        public string UniLogo { get; set; }
        public string UniBilgi { get; set; }

        public Guid? SehirId { get; set; }
        [ForeignKey("SehirId")]
        public virtual Sehirler Sehir { get; set; }

        public Guid? EyaletId { get; set; }
        [ForeignKey("EyaletId")]
        public virtual Eyaletler Eyalet { get; set; }

        public Guid? TemsilciId { get; set; }
        [ForeignKey("TemsilciId")]
        public virtual Temsilcilikler Temsilcilik { get; set; }

        public Guid UlkeId { get; set; }
        [ForeignKey("UlkeId")]
        public virtual Ulkeler Ulke { get; set; }

        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public virtual Kullanici Kullanici { get; set; }

        public virtual ICollection<AdayGorevKaydi> AdayGorevKaydi { get; set; }
        public virtual ICollection<Ogrenciler> Ogrenciler { get; set; }
        public virtual ICollection<FotoGaleri> FotoGaleri { get; set; }
    }
}
