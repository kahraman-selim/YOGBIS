using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class UlkeTercih : Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid UlkeTercihId { get; set; }
        public string UlkeTercihAdi { get; set; }
        public int UlkeTercihSiraNo { get; set; }
        public string YabancıDil { get; set; }

        public Guid DereceId { get; set; }
        [ForeignKey("DereceId")]
        public virtual SoruDereceler SoruDereceler { get; set; }

        public Guid MulakatId { get; set; }
        [ForeignKey("MulakatId")]
        public virtual Mulakatlar Mulakatlar { get; set; }

        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public virtual Kullanici Kullanici { get; set; }

        public virtual ICollection<UlkeTercihBranslar> UlkeTercihBranslar { get; set; }
        public virtual ICollection<AdayBasvuruBilgileri> AdayBasvuruBilgileri { get; set; }
    }
}
