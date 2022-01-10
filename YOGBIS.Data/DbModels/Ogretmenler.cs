using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Ogretmenler:Base
    {
        [Key]
        public int OgretmenId { get; set; }
        public int OgretmenTC { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
        public string OgretmenAd { get; set; }
        public string OgretmenAd2 { get; set; }
        public string OgretmenSoyad { get; set; }
        public string OgretmenSoyad2 { get; set; }
        public string OgretmenBabaAd { get; set; }
        public string OgretmenAnaAd { get; set; }
        public DateTime DogumTarihi { get; set; }
        public string DogumYeriIl { get; set; }
        public string DogumYeriIlce { get; set; }
        public string NufusIl { get; set; }
        public string NufusIlce { get; set; }
        public bool Cinsiyet { get; set; }
        public string Askerlik { get; set; }
        public string CepTelNo { get; set; }
        public string EPosta { get; set; }
        public string AdresIl { get; set; }
        public string AdresIlce { get; set; }
        public string KurumKod { get; set; }
        public string KurumAdi { get; set; }
        public string Ogrenim { get; set; }
        public string MezunOkulKodu { get; set; }
        public string MezunOkulAdi { get; set; }
        public string Brans { get; set; }
        public string Unvan { get; set; }
        public string HizmetYil { get; set; }
        public string HizmetAy { get; set; }
        public string HizmetGun { get; set; }
        public string Derece { get; set; }
        public string Kademe { get; set; }
        public string Enaz5Yil { get; set; }
        public bool DahaOnceYYGorev { get; set; }
        public bool? YYSonrasi2Yil { get; set; }
        public DateTime? YIciGorevBasTar { get; set; }
        public string DisiplinCeza { get; set; }
        public string YabanciDilAdi { get; set; }
        public string YabanciDilTuru { get; set; }
        public DateTime YabanciDilTarihi { get; set; }
        public string YabanciDilSeviye { get; set; }
        public string UlkeTercihi { get; set; }
        public string IlTercihi { get; set; }
        public string BasvuranIP { get; set; }
        public string BasvuruTarihi { get; set; }
        public string OnayDurumu { get; set; }
        public DateTime? OnayTarihi { get; set; }
        public string Onaylayan { get; set; }
        public string OnayAciklama { get; set; }
        public DateTime SinavTarihi { get; set; }
        public string SinavTedbiri { get; set; }
        public string Aciklama { get; set; }
        public string MYSPuan { get; set; }
        public string MYSSonuc { get; set; }
        public string IlMemGorus { get; set; }
        public int? OkulId { get; set; }
        [ForeignKey("OkulId")]
        public Okullar Okullar { get; set; }
        public int SehirId { get; set; }
        [ForeignKey("SehirId")]
        public Sehirler Sehirler { get; set; }
        public int EyaletId { get; set; }
        [ForeignKey("EyaletId")]
        public Eyaletler Eyaletler { get; set; }
        public List<GorevKaydi> GorevKaydis { get; set; }
        public ICollection<FotoGaleri> FotoGaleri { get; set; }
    }
}
