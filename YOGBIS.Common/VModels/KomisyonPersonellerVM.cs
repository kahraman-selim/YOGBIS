using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YOGBIS.Data.DbModels;
using System.Collections.Generic;

namespace YOGBIS.Common.VModels
{
    public class KomisyonPersonellerVM:BaseVM
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public string KomisyonKullaniciId { get; set; }
        public Guid PersonelId { get; set; }
        public Guid RolId { get; set; }
        
        //public Guid? YardimciPersonelId { get; set; }
        //public Guid? IlgiliPersonelId { get; set; }
      
        public virtual KomisyonlarVM Komisyonlar { get; set; }
        public string KaydedenId { get; set; }
        public string KaydedenAdi { get; set; }
        public virtual KullaniciVM Kullanici { get; set; }
    }
}
