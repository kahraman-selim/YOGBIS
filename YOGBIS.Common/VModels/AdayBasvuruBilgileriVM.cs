using System;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class AdayBasvuruBilgileriVM : BaseVM
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "TC Kimlik No zorunludur")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "TC Kimlik No 11 haneli olmalıdır")]
        [Display(Name = "TC Kimlik No")]
        public string TC { get; set; }

        public Guid? AdayId { get; set; }
        
        public string DogumYeri { get; set; }
        public int Yas { get; set; }
        public string IkametBilgisi { get; set; }

        [Display(Name = "Askerlik Durumu")]
        public string Askerlik { get; set; }

        [Display(Name = "Kurum Kodu")]
        public string KurumKod { get; set; }

        [Display(Name = "Kurum Adı")]
        public string KurumAdi { get; set; }

        [Display(Name = "Öğrenim Durumu")]
        public string Ogrenim { get; set; }

        [Display(Name = "Mezun Okul Kodu")]
        public string MezunOkulKodu { get; set; }

        [Display(Name = "Mezuniyet")]
        public string Mezuniyet { get; set; }

        [Display(Name = "Hizmet Yılı")]
        public string HizmetYil { get; set; }

        [Display(Name = "Hizmet Ayı")]
        public string HizmetAy { get; set; }

        [Display(Name = "Hizmet Günü")]
        public string HizmetGun { get; set; }

        [Display(Name = "Derece")]
        public string Derece { get; set; }

        [Display(Name = "Kademe")]
        public string Kademe { get; set; }

        [Display(Name = "En Az 5 Yıl")]
        public string Enaz5Yil { get; set; }

        [Display(Name = "Daha Önce Yurtdışı Görevi")]
        public string DahaOnceYDGorev { get; set; }

        [Display(Name = "Yurtdışı Görev Başlangıç Tarihi")]
        public string YIciGorevBasTar { get; set; }

        [Display(Name = "Yabancı Dil Başvurusu")]
        public string YabanciDilBasvuru { get; set; }

        [Display(Name = "Yabancı Dil")]
        public string YabanciDilAdi { get; set; }

        [Display(Name = "Yabancı Dil Türü")]
        public string YabanciDilTuru { get; set; }

        [Display(Name = "Yabancı Dil Sınav Tarihi")]
        public string YabanciDilTarihi { get; set; }

        [Display(Name = "Yabancı Dil Puanı")]
        public string YabanciDilPuan { get; set; }

        [Display(Name = "Yabancı Dil Seviyesi")]
        public string YabanciDilSeviye { get; set; }

        [Display(Name = "1. İl Tercihi")]
        public string IlTercihi1 { get; set; }

        [Display(Name = "2. İl Tercihi")]
        public string IlTercihi2 { get; set; }

        [Display(Name = "3. İl Tercihi")]
        public string IlTercihi3 { get; set; }

        [Display(Name = "4. İl Tercihi")]
        public string IlTercihi4 { get; set; }

        [Display(Name = "5. İl Tercihi")]
        public string IlTercihi5 { get; set; }

        [Display(Name = "Başvuru Tarihi")]
        public string BasvuruTarihi { get; set; }

        [Display(Name = "Son Değişiklik Tarihi")]
        public string SonDegisiklikTarihi { get; set; }

        [Display(Name = "Onay Durumu")]
        public string OnayDurumu { get; set; }

        [Display(Name = "Onay Durumu Açıklaması")]
        public string OnayDurumuAck { get; set; }

        [Display(Name = "MYYS Tarihi")]
        public string MYYSTarihi { get; set; }

        [Display(Name = "MYYS Sınav Tedbiri")]
        public string MYYSSinavTedbiri { get; set; }

        [Display(Name = "MYYS Tedbir Açıklaması")]
        public string MYYSTedbirAck { get; set; }

        [Display(Name = "MYYS Puanı")]
        public string MYYSPuan { get; set; }

        [Display(Name = "MYYS Sonucu")]
        public string MYYSSonuc { get; set; }

        [Display(Name = "MYSS Durumu")]
        public string MYSSDurum { get; set; }

        [Display(Name = "MYSS Durum Açıklaması")]
        public string MYSSDurumAck { get; set; }

        [Display(Name = "İl MEM Görüşü")]
        public string IlMemGorus { get; set; }

        [Display(Name = "Referans")]
        public string Referans { get; set; }

        [Display(Name = "Referans Açıklaması")]
        public string ReferansAck { get; set; }

        [Display(Name = "Görev İptal Açıklaması")]
        public string GorevIptalAck { get; set; }

        [Display(Name = "Görev İptal Branşı")]
        public string GorevIptalBrans { get; set; }

        [Display(Name = "Görev İptal Yılı")]
        public string GorevIptalYil { get; set; }

        [Display(Name = "Görev İptal BAOK")]
        public string GorevIptalBAOK { get; set; }

        [Display(Name = "İlk Görev Kaydı")]
        public string IlkGorevKaydi { get; set; }

        [Display(Name = "Almanca")]
        public string YabanciDilALM { get; set; }

        [Display(Name = "İngilizce")]
        public string YabanciDilING { get; set; }

        [Display(Name = "Fransızca")]
        public string YabanciDilFRS { get; set; }

        [Display(Name = "Diğer Yabancı Dil")]
        public string YabanciDilDiger { get; set; }

        public string YLisans { get; set; }
        public string Doktora { get; set; }

        [Display(Name = "Görevden Uzaklaştırma")]
        public string GorevdenUzaklastirma { get; set; }

        [Display(Name = "E-Durumu")]
        public string EDurum { get; set; }

        [Display(Name = "M-Durumu")]
        public string MDurum { get; set; }

        [Display(Name = "P-Durumu")]
        public string PDurum { get; set; }
        public string GenelDurum { get; set; }

        [Display(Name = "Sendika")]
        public string Sendika { get; set; }

        [Display(Name = "Sendika Açıklaması")]
        public string SendikaAck { get; set; }

        [Display(Name = "MYYS Soru İtirazı")]
        public string MYYSSoruItiraz { get; set; }

        [Display(Name = "MYYS Sonuç İtirazı")]
        public string MYYSSonucItiraz { get; set; }

        [Display(Name = "Başvuru Branşı")]
        public string BasvuruBrans { get; set; }

        [Display(Name = "Adli Sicil Belgesi")]
        public byte[] AdliSicilBelge { get; set; }

        [Display(Name = "Adli Sicil Belgesi Var Mı?")]
        public bool HasAdliSicilBelge { get; set; }
        
        [Display(Name = "BilgiFormu")]
        public byte[] BilgiFormu { get; set; }

        public Guid? BransId { get; set; }
        public string BransAdi { get; set; }
        [Display(Name = "Branş")]
        public BranslarVM Brans { get; set; }

        public Guid? DereceId { get; set; }
        public string DereceAdi { get; set; }
        [Display(Name = "Derece")]
        public SoruDerecelerVM SoruDerece { get; set; }

        [Display(Name = "Ünvan")]
        public string Unvan { get; set; }

        public Guid? UlkeTercihId { get; set; }
        [Display(Name = "Ülke Tercihi")]
        public string UlkeTercihAdi { get; set; }
        public UlkeTercihVM UlkeTercih { get; set; }

        public Guid? MulakatId { get; set; }

        [Display(Name = "Mülakat")]
        public MulakatlarVM Mulakat { get; set; }

        [Display(Name = "Mülakat Onay No")]
        public string MulakatOnayNo { get; set; }

        [Display(Name = "Mülakat Yılı")]
        public int MulakatYil { get; set; }

        [Required(ErrorMessage = "Kaydeden kişi zorunludur")]
        public string KaydedenId { get; set; }

        [Display(Name = "Kaydeden")]
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
    }
}
