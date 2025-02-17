using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Common.VModels
{
    public class AdayIletisimBilgileriVM:BaseVM
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
        public Guid? AdayId { get; set; }
        public AdaylarVM Adaylar { get; set; }
        public string KaydedenId { get; set; }
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
    }
}
