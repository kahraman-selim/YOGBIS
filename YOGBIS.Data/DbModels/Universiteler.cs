using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Universiteler:Base
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
        public Guid? EyaletId { get; set; }
        public Guid? TemsilciId { get; set; }
        public Guid UlkeId { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
        public List<AdayGorevKaydi> AdayGorevKaydis { get; set; }
        public List<Ogrenciler> Ogrenciler { get; set; }
        public ICollection<FotoGaleri> FotoGaleri { get; set; }
    }
}
