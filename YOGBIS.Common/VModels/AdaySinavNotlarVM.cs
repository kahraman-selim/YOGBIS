using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Common.VModels
{
    public class AdaySinavNotlarVM:BaseVM
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public string TC { get; set; }
        public Guid? KomisyonId { get; set; }
        public int KomisyonUyeSiraId { get; set; }
        public Guid? NotKategoriId1 { get; set; }
        public int Not1 { get; set; }
        public Guid? NotKategoriId2 { get; set; }
        public int Not2 { get; set; }
        public Guid? NotKategoriId3 { get; set; }
        public int Not3 { get; set; }
        public Guid? NotKategoriId4 { get; set; }
        public int Not4 { get; set; }
        public Guid? NotKategoriId5 { get; set; }
        public int Not5 { get; set; }
        public Guid AdayId { get; set; }
        public AdaylarVM Adaylar { get; set; }
        public Guid? MulakatId { get; set; }
        public string KaydedenId { get; set; }
        public KullaniciVM Kullanici { get; set; }
    }
}
