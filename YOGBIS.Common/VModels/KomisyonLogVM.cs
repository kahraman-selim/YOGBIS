using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YOGBIS.Data.DbModels;
using System.Collections.Generic;

namespace YOGBIS.Common.VModels
{
    public  class KomisyonLogVM: BaseVM
    {
        public Guid Id { get; set; }
        public Guid KomisyonId { get; set; }
        public int KomisyonUyeSiraNo { get; set; }
        public string KomisyonUyeAdSoyad { get; set; }
        public DateTime GorevBaslamaTarihi { get; set; }
        public DateTime GorevBitisTarihi { get; set; }
        public string KaydedenId { get; set; }
        public string KaydedenAdi { get; set; }
        public virtual KullaniciVM Kullanici { get; set; }
        public virtual KomisyonlarVM Komisyonlar { get; set; }
    }
}
