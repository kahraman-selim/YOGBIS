using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Common.VModels
{
    public class AdaylarVM:BaseVM
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid AdayId { get; set; }
        public string TC { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string BabaAd { get; set; }
        public string AnaAd { get; set; }
        public string DogumTarihi { get; set; }
        public string DogumTarihi2 { get; set; }
        public int? Yasi { get; set; }
        public string DogumYeri { get; set; }
        public string Cinsiyet { get; set; }
        //Kayıt yapan kullanıcı
        public string KaydedenId { get; set; }
        public string KaydedenAdi { get; set; }
        public Kullanici Kullanici { get; set; }
        //Bağlı tablolar
        public IFormFileCollection FotoGaleris { get; set; }
        public List<FotoGaleriVM> FotoGaleri { get; set; }
        public IFormFileCollection DosyaGaleris { get; set; }
        public List<DosyaGaleriVM> DosyaGaleri { get; set; }
        public List<AdayNotVM> AdayNots { get; set; }
        public List<AdayDDKVM> AdayDDKs { get; set; }
        public List<AdayGorevKaydiVM> AdayGorevKaydis { get; set; }
        public List<EPostaAdresleriVM> EpostaAdresleris { get; set; }
        public List<TelefonlarVM> Telefonlars { get; set; }
        public List<IkametAdresleriVM> IkametAdresleris { get; set; }
        public List<AdayBasvuruBilgileriVM> AdayBasvuruBilgileris { get; set; }
        public List<AdayIletisimBilgileriVM> AdayIletisimBilgileris { get; set; }
        public List<AdayMYSSVM> AdayMYSSs { get; set; }
        public List<AdayTYSVM> AdayTYSs { get; set; }
    }
}
