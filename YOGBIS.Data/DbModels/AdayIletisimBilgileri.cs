using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class AdayIletisimBilgileri:Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public string TC { get; set; }
        public string CepTelNo { get; set; }
        public string EPosta { get; set; }
        public string NufusIl { get; set; }
        public string NufusIlce { get; set; }
        public string IkametAdres { get; set; }
        public string IkametIl { get; set; }
        public string IkametIlce { get; set; }
        public Guid? MulakatId { get; set; }
        public Guid AdayId { get; set; }
        [ForeignKey("AdayId")]
        public Adaylar Adaylar { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
    }
}
