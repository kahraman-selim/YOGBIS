using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class EtkinliklerVM:BaseVM
    {
        [Key]
        public int EtkinlikId { get; set; }
        public string EtkinlikAdi { get; set; }
        public string OkulAdi { get; set; }
        public int OkulId { get; set; }
        public OkullarVM Okullar { get; set; }
        public int TemsilcilikId { get; set; }
        public TemsilciliklerVM Temsilcilikler { get; set; }
        public DateTime BasTarihi { get; set; }
        public DateTime BitTarihi { get; set; }
        public string EtkinlikBilgi { get; set; }
        public int KatilimciSayisi { get; set; }
        public string DuzenleyenAdiSoyadi { get; set; }
        public IFormFile EtkinlikKapakResim { get; set; }
        public string EtkinlikKapakResimUrl { get; set; }
        public IFormFile EtkinlikDosya { get; set; }
        public string KaydedenId { get; set; }
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
        public IFormFileCollection DosyaGaleris { get; set; }
        public List<DosyaGaleriVM> DosyaGaleri { get; set; }
        public IFormFileCollection FotoGaleris { get; set; }
        public List<FotoGaleriVM> FotoGaleri { get; set; }
    }
}
