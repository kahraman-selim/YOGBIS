using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class SoruKategoriler : Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid SoruKategorilerId { get; set; }
        public int SoruKategorilerSiraNo { get; set; }
        public string SoruKategorilerAdi { get; set; }
        public string SoruKategorilerKullanimi { get; set; }
        public int SoruKategorilerPuan { get; set; }
        public string SoruKategorilerTakmaAdi { get; set; }
        public string SoruKategorilerTamAdi { get; set; }
        public int SinavKateogoriTurId { get; set; }
        public string SinavKategoriTurAdi { get; set; }

        public Guid DereceId { get; set; }
        [ForeignKey("DereceId")]
        public virtual SoruDereceler SoruDereceler { get; set; }

        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public virtual Kullanici Kullanici { get; set; }

        public virtual ICollection<SoruKategori> SoruKategori { get; set; }
    }
}
