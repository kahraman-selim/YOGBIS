using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Common.VModels
{
    public class AdayNotVM:BaseVM
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public Guid AdayId { get; set; }
        public Adaylar Adaylar { get; set; }
        public string TC { get; set; }
        public Guid? KomisyonId { get; set; }
        public int KomisyonUyeSiraId { get; set; }
        public string KomisyonUyeAdSoyad { get; set; }
        public Guid? NotKategoriId { get; set; }
        public string NotKategoriTakmaAdi { get; set; }
        public int Not { get; set; }
        public Guid? MulakatId { get; set; }
        public string KaydedenId { get; set; }
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
    }
}
