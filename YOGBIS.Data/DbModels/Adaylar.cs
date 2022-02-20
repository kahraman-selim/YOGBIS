using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Data.DbModels
{
    public class Adaylar:Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid AdayId { get; set; }
        public string AdayTC { get; set; }
        public string AdayAd { get; set; }
        public string AdaySoyad { get; set; }
        public string AdayBabaAd { get; set; }
        public string AdayAnaAd { get; set; }
        public string DogumTarihi { get; set; }
        public string DogumYeri { get; set; }
        public string Cinsiyet { get; set; }
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
        public string HizmetYil { get; set; }
        public string HizmetAy { get; set; }
        public string HizmetGun { get; set; }
        public string Derece { get; set; }
        public string Kademe { get; set; }
        public string Enaz5Yil { get; set; }
        public string DahaOnceYYGorev { get; set; }
        public string YYSonrasi2Yil { get; set; }
        public string YIciGorevBasTar { get; set; }
        public string DisiplinCeza { get; set; }
        public string YabanciDilAdi { get; set; }
        public string YabanciDilTuru { get; set; }
        public string YabanciDilTarihi { get; set; }
        public string YabanciDilSeviye { get; set; }
        public string YabanciDilBelgesi { get; set; }
        public string UlkeTercihi { get; set; }
        public string IlTercihi { get; set; }
        public string BasvuranIP { get; set; }
        public string BasvuruTarihi { get; set; }
        public string OnayDurumu { get; set; }
        public string OnayTarihi { get; set; }
        public string Onaylayan { get; set; }
        public string OnayAciklama { get; set; }
        public string MYSSinavTarihi { get; set; }
        public string MYSSinavTedbiri { get; set; }
        public string MYSSTAciklama { get; set; }
        public Guid DereceId { get; set; }
        //Aday başvuru tablosu
        public string MYSPuan { get; set; }
        public string MYSSonuc { get; set; }
        //yazılısınav sonrası
        public string MulakatDurum { get; set; }
        public string MulakatDurumAciklama { get; set; }
        public string IlMemGorus { get; set; }
        public string TYSTarih { get; set; }
        public string TYSSaat { get; set; }
        public string TYSPuan { get; set; }
        public string AritmetikOrtalama { get; set; }
        public string SonucDurumu { get; set; }
        public int? GorevDurumu { get; set; }        
        public Guid MulakatId { get; set; }
        [ForeignKey("MulakatId")]
        public Mulakatlar Mulakatlar { get; set; }
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
        public string AdayFotoURL { get; set; }
        public ICollection<FotoGaleri> FotoGaleri { get; set; }
        public List<AdayDDK> AdayDDKs { get; set; }
        public List<AdayNot> AdayNots { get; set; }
    }
}
