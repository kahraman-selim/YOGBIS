using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class AdayBasvuruBilgileri:Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public string TC { get; set; }
        public string Askerlik { get; set; }
        public string KurumKod { get; set; }
        public string KurumAdi { get; set; }
        public string Ogrenim { get; set; }
        public string MezunOkulKodu { get; set; }
        public string HizmetYil { get; set; }
        public string HizmetAy { get; set; }
        public string HizmetGun { get; set; }
        public string Derece { get; set; }
        public string Kademe { get; set; }
        public string Enaz5Yil { get; set; }
        public string DahaOnceYYGorev { get; set; }
        public string YIciGorevBasTar { get; set; }
        public string YabanciDilBelge { get; set; }
        public string YabanciDilAdi { get; set; }
        public string YabanciDilTuru { get; set; }
        public string YabanciDilTarihi { get; set; }
        public string YabanciDilPuan { get; set; }
        public string YabanciDilSeviye { get; set; }
        public string IlTercihi1 { get; set; }
        public string IlTercihi2 { get; set; }
        public string IlTercihi3 { get; set; }
        public string IlTercihi4 { get; set; }
        public string IlTercihi5 { get; set; }
        public string BasvuruTarihi { get; set; }
        public string SonDegisiklikTarihi { get; set; }
        public string OnayDurumu { get; set; }
        public string OnayAciklama { get; set; }
        public string MYYSTarihi { get; set; }
        public string MYYSinavTedbiri { get; set; }
        public string MYYSTedbirAck { get; set; }
        public string MYYSPuan { get; set; }
        public string MYYSonuc { get; set; }
        public string MYSSDurum { get; set; }
        public string MYSSDurumAck { get; set; }
        public string IlMemGorus { get; set; }
        public string Referans { get; set; }
        public string ReferansAck { get; set; }
        public string GorevIptalAck { get; set; }
        public string GorevIptalBrans { get; set; }
        public string GorevIptalYil { get; set; }
        public string GorevIptalBAOK { get; set; }
        public string IlkGorevKaydi { get; set; }
        public string YabanciDilALM { get; set; }
        public string YabanciDilIGN { get; set; }
        public string YabanciDilFRS { get; set; }
        public string YabanciDilDiger { get; set; }
        public string GorevdenUzaklastirma { get; set; }
        public string EDurum { get; set; }
        public string MDurum { get; set; }
        public string PDurum { get; set; }
        public string Sendika { get; set; }
        public string SendikaAck { get; set; }
        [MaxLength]
        public byte[] AdliSicilBelge { get; set; }
        [MaxLength]
        public byte[] BelgeEk1 { get; set; }
        public Guid? DereceId { get; set; }
        public string DereceAdi { get; set; }
        public string Unvan { get; set; }
        public Guid? MulakatId { get; set; }
        public Guid? KomisyonId { get; set; }
        public Guid? UlkeTercihId { get; set; }
        public Guid? BransId { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
    }
}
