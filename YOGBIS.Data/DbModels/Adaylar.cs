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
        public int? Yasi { get; set; }
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
        public string MYYSTedbirAck { get; set; }
        public Guid? DereceId { get; set; }
        public string DereceAdi { get; set; }
        public string Unvan { get; set; }        
        public Guid? MulakatId { get; set; }
        public string MulakatOnayNo { get; set; }
        [ForeignKey("MulakatId")]
        public Mulakatlar Mulakatlar { get; set; }
        //Aday başvuru tablosu
        public string MYYSPuan { get; set; }
        public string MYYSonuc { get; set; }
        public string MYSSDurum { get; set; }
        public string MYSSDurumAck { get; set; }
        public string MYSSTarih { get; set; }
        public string MYSSSaat { get; set; }
        public string MYSSMulakatYer { get; set; }
        public int? MYSSKomisyonSiraNo { get; set; }
        public string MYSSKomisyonAdi { get; set; }
        //MYSS Komisyon Bilgileri
        public Guid? KomisyonId { get; set; }
        [ForeignKey("KomisyonId")]
        public Komisyonlar Komisyonlar { get; set; }
        //MYSS Komisyon Bilgileri
        public string MYSSPuan { get; set; }
        public string MYSSSonuc { get; set; }
        public string MYSSSonucAck { get; set; }
        public int? MYSSSorulanSoruNo { get; set; }
        //---------------------------
        public string IlMemGorus { get; set; }
        public string Referans { get; set; }
        public string ReferansAck { get; set; }
        //TYS Bilgileri
        public string TYSDurumu { get; set; }
        public string TYSDurumAck { get; set; }
        public string TYSTarih { get; set; }
        public string TYSSaat { get; set; }
        public string TYSYer { get; set; }
        public int? TYSKomisyonSayi { get; set; }
        public string TYSKomisyonAdi { get; set; }
        public string TYSPuan { get; set; }
        public string TYSSonuc { get; set; }
        public string TYSSonucAck { get; set; }
        //--------------------------------
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
        public int? KomisyonSN { get; set; }
        public int? KomisyonGunSN { get; set; }
        public string Sendika { get; set; }
        public string SendikaAck { get; set; }
        //Byte türünde adli sicil kaydı - Ek-1 - 
        [MaxLength]
        public byte[] BelgeEk1 { get; set; }
        public bool? CagriDurum { get; set; }
        public bool? KabulDurum { get; set; }
        public bool? SinavDurum { get; set; }
        //Bağlı tablolar
        public string KaydedenId { get; set; }
        [ForeignKey("KaydedenId")]
        public Kullanici Kullanici { get; set; }
        public ICollection<FotoGaleri> FotoGaleri { get; set; }
        public ICollection<DosyaGaleri> DosyaGaleri { get; set; }
        public List<AdayNot> AdayNot { get; set; }

        //public List<AdayDDK> AdayDDK { get; set; }
        //public List<AdayGorevKaydi> AdayGorevKaydi { get; set; }
        //public List<EPostaAdresleri> EpostaAdresleri { get; set; }
        //public List<Telefonlar> Telefonlar { get; set; }
        //public List<IkametAdresleri> IkametAdresleri { get; set; }
    }
}
