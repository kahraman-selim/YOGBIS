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
        public string TC { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string BabaAd { get; set; }
        public string AnaAd { get; set; }
        public string DogumTarihi { get; set; }
        public string DogumTarihi2 { get; set; }
        public string DogumYeri { get; set; }
        public string Cinsiyet { get; set; }
        public string Askerlik { get; set; }
        public string CepTelNo { get; set; }
        public string CepTelNo2 { get; set; }
        public string EPosta { get; set; }
        public string EPosta2 { get; set; }
        public string NufusIl { get; set; }
        public string NufusIlce { get; set; }
        public string IkametAdres { get; set; }
        public string IkametIl { get; set; }
        public string IkametIlce { get; set; }        
        public string KurumKod { get; set; }
        public string KurumAdi { get; set; }
        public string KurumAdi2 { get; set; }
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
        public string YIciGorevBasTar { get; set; }
        public string YabanciDilBelge { get; set; }
        public string YabanciDilAdi { get; set; }
        public string YabanciDilTuru { get; set; }
        public string YabanciDilTarihi { get; set; }
        public string YabanciDilPuan { get; set; }
        public string YabanciDilSeviye { get; set; }
        public string UlkeTercihi { get; set; }
        public string IlTercihi { get; set; }
        public string BasvuruTarihi { get; set; }
        public string SonDegisiklikTarihi { get; set; }
        public string OnayDurumu { get; set; }
        public string OnayAciklama { get; set; }
        public string MYYSTarihi { get; set; }
        public string MYYSinavTedbiri { get; set; }
        public string MYYSTedbirAciklama { get; set; }        
        public Guid DereceId { get; set; }
        public string Unvan { get; set; }
        public Guid MulakatId { get; set; }
        [ForeignKey("MulakatId")]
        public Mulakatlar Mulakatlar { get; set; }
        public Guid KomisyonId { get; set; }
        [ForeignKey("KomisyonId")]
        public Komisyonlar Komisyonlar { get; set; }
        //Aday başvuru tablosu
        public string MYYSPuan { get; set; }
        public string MYYSonuc { get; set; }
        //yazılısınav sonrası
        public string MYSSDurum { get; set; }
        public string MYSSDurumAciklama { get; set; }
        //MYSS Komisyon Bilgileri
        // eklenecek
        //....
        public string MYSSPuan { get; set; }
        public string MYSSSonuc { get; set; }
        public string IlMemGorus { get; set; }
        public string Referans { get; set; }
        public string ReferansAciklama { get; set; }
        public string TYSDurumu { get; set; }
        public string TYSDurumAciklama { get; set; }
        //TYS Komisyon Bilgileri
        // eklenecek
        // ....
        public string TYSPuan { get; set; }
        public string TYSSonuc { get; set; }
        //--------------------------------
        public string GorevIptalAciklama { get; set; }
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
        //Byte türünde adli sicil kaydı - Ek-1 - 
        //Bağlı tablolar
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
        public ICollection<FotoGaleri> FotoGaleri { get; set; }
        public ICollection<DosyaGaleri> DosyaGaleri { get; set; }
        //public List<AdayDDK> AdayDDK { get; set; }
        public List<AdayNot> AdayNot { get; set; }
        //public List<AdayGorevKaydi> AdayGorevKaydi { get; set; }
        //public List<EPostaAdresleri> EpostaAdresleri { get; set; }
        //public List<Telefonlar> Telefonlar { get; set; }
        //public List<IkametAdresleri> IkametAdresleri { get; set; }
    }
}
