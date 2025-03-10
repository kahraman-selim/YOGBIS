using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Common.VModels
{
    public class KomisyonlarVM : BaseVM
    {
        public Guid KomisyonId { get; set; }

        [Required(ErrorMessage = "Komisyon kullanıcı ID zorunludur")]
        [Display(Name = "Komisyon Kullanıcı ID")]
        public string KomisyonKullaniciId { get; set; }

        [Required(ErrorMessage = "Komisyon sıra no zorunludur")]
        [Display(Name = "Komisyon Sıra No")]
        public int KomisyonSiraNo { get; set; }

        [Required(ErrorMessage = "Komisyon adı zorunludur")]
        [StringLength(100, ErrorMessage = "Komisyon adı en fazla 100 karakter olabilir")]
        [Display(Name = "Komisyon Adı")]
        public string KomisyonAdi { get; set; }

        [Display(Name = "Komisyon Üye Durumu")]
        public string KomisyonUyeDurum { get; set; }

        [Display(Name = "Komisyon Gorev Durumu")]
        public bool KomisyonGorevDurum { get; set; }

        [Display(Name = "Komisyon Üye Sıra No")]
        public int KomisyonUyeSiraNo { get; set; }

        [Required(ErrorMessage = "Komisyon üye görevi zorunludur")]
        [Display(Name = "Komisyon Üye Görevi")]
        public string KomisyonUyeGorevi { get; set; }

        [Required(ErrorMessage = "Komisyon üye adı soyadı zorunludur")]
        [Display(Name = "Komisyon Üye Adı Soyadı")]
        public string KomisyonUyeAdiSoyadi { get; set; }

        [Required(ErrorMessage = "Komisyon üye görev yeri zorunludur")]
        [Display(Name = "Komisyon Üye Görev Yeri")]
        public string KomisyonUyeGorevYeri { get; set; }

        [Display(Name = "Komisyon Üye Statü")]
        public string KomisyoUyeStatu { get; set; }

        [Display(Name = "Komisyon Ülke Grubu")]
        public string KomisyonUlkeGrubu { get; set; }

        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz")]
        [Display(Name = "Komisyon Üye Telefon")]
        public string KomisyoUyeTel { get; set; }

        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
        [Display(Name = "Komisyon Üye E-posta")]
        public string KomisyonUyeEPosta { get; set; }

        [Required(ErrorMessage = "Komisyon görev başlama tarihi zorunludur")]
        [Display(Name = "Görev Başlama Tarihi")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime KomisyonGorevBaslamaTarihi { get; set; }

        [Required(ErrorMessage = "Komisyon görev bitiş tarihi zorunludur")]
        [Display(Name = "Görev Bitiş Tarihi")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime KomisyonGorevBitisTarihi { get; set; }

        [Required(ErrorMessage = "Kaydeden kişi zorunludur")]
        public Guid? MulakatId { get; set; }
        public int MulakatYil { get; set; }
        public string MulakatDonemi { get; set; }
        public virtual MulakatlarVM Mulakatlar { get; set; }


        [Required(ErrorMessage = "Kaydeden kişi zorunludur")]
        public string KaydedenId { get; set; }

        [Display(Name = "Kaydeden")]
        public string KaydedenAdi { get; set; }

        public KullaniciVM Kullanici { get; set; }
    }
}
