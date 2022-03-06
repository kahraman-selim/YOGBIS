using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class OkulBilgi:Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid OkulBilgiId { get; set; }
        public string OkulTelefon { get; set; }
        public string OkulAdres { get; set; }
        //****************************************
        public string MudurAdiSoyadi { get; set; }
        public string MudurTelefon { get; set; }
        public string MudurEPosta { get; set; }
        public string MudurDonusYil { get; set; }
        //*****************************************
        public string MdYrdAdiSoyadi { get; set; }
        public string MdYrdTelefon { get; set; }
        public string MdYrdEPosta { get; set; }
        public string MdYrdDonusYil { get; set; }

        //****************************************
        public Guid OkulId { get; set; }

        [ForeignKey("OkulId")]
        public Okullar Okullar { get; set; }
        public Guid UlkeId { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
    }
}
