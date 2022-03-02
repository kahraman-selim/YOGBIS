using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Common.VModels
{
    public class AdayGorevKaydiVM:BaseVM
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid AdayGorevKaydiId { get; set; }
        public string GorevliTC { get; set; }
        public Guid DereceId { get; set; }        
        public SoruDerecelerVM SoruDereceler { get; set; }
        public string Gorevi { get; set; }
        public Guid BransId { get; set; }
        public BranslarVM Branslar { get; set; }
        public string GorevOnaySayi { get; set; }
        public DateTime GorevlOnayTarihi { get; set; }        
        public DateTime? GorevlendirilmeTarihi { get; set; }
        public DateTime? GorevBasTarihi { get; set; }
        public DateTime? GorevBitisTarihi { get; set; }
        public int GorevDurumu { get; set; }
        public string GorevAciklama { get; set; }
        public Guid? OkulId { get; set; }
        public Guid? UniId { get; set; }
        public Guid? TemsilcilikId { get; set; }
        public Guid? SehirId { get; set; }
        public Guid? EyaletId { get; set; }
        public Guid UlkeId { get; set; }
        public string UlkeAdi { get; set; }
        public UlkelerVM Ulkeler { get; set; }
        public string KaydedenId { get; set; }
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
        public List<EPostaAdresleriVM> EpostaAdresleri { get; set; }
        public List<TelefonlarVM> Telefonlar { get; set; }
        public string KararPdfUrl { get; set; }
        public IFormFileCollection GorevKararPdfGaleris { get; set; }
        public ICollection<GorevKararPdfGaleriVM> GorevKararPdfGaleri { get; set; }
        public IFormFileCollection FotoGaleris { get; set; }
        public ICollection<FotoGaleriVM> FotoGaleri { get; set; }
    }
}
