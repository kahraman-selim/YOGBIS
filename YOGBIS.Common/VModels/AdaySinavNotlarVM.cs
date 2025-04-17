using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class AdaySinavNotlarVM : BaseVM
    {
        public Guid Id { get; set; }        
        public List<KomisyonUyelerVM> komisyonUyelerVm { get; set; }

        public Guid KomisyonId { get; set; }
        //public int KomisyonUyeSiraId { get; set; }
        public string KomisyonAdi { get; set; }
        public string KomisyonKullaniciId { get; set; }
        [Display(Name = "Komisyon")]
        public KomisyonlarVM Komisyon { get; set; }  
      
        public Guid? NotKategoriId1 { get; set; }
        public string KategoriAdi1 { get; set; }
        public SoruKategorilerVM NotKategori1 { get; set; }
        public int Not1 { get; set; }

        public Guid? NotKategoriId2 { get; set; }
        public string KategoriAdi2 { get; set; }
        public SoruKategorilerVM NotKategori2 { get; set; }
        public int Not2 { get; set; }

        public Guid? NotKategoriId3 { get; set; }
        public string KategoriAdi3 { get; set; }
        public SoruKategorilerVM NotKategori3 { get; set; }
        public int Not3 { get; set; }

        public Guid? NotKategoriId4 { get; set; }
        public string KategoriAdi4 { get; set; }
        public SoruKategorilerVM NotKategorisi4 { get; set; }
        public int Not4 { get; set; }

        public Guid? NotKategoriId5 { get; set; }
        public string KategoriAdi5 { get; set; }
        public SoruKategorilerVM NotKategorisi5 { get; set; }
        public int Not5 { get; set; }
        public int Not6 { get; set; }
        public int Not7 { get; set; }
        public int Not8 { get; set; }
        public int Not9 { get; set; }
        public int Not10 { get; set; }
        public int Not11 { get; set; }
        public int Not12 { get; set; }
        public int Not13 { get; set; }
        public int Not14 { get; set; }
        public int Not15 { get; set; }
  

        public int Toplam1 { get; set; }
        public int Toplam2 { get; set; }
        public int Toplam3 { get; set; }
        public double Ortalama { get; set; }
        public double MYSPuan { get; set; }
        public string MYSSonuc { get; set; }

        public Guid AdayId { get; set; }
        public string TC { get; set; }
        public string AdayAdSoyad { get; set; }
        public string AdayMYYSPuan { get; set; }
        [Display(Name = "Aday")]
        public AdaylarVM Adaylar { get; set; }

        public Guid BransId { get; set; }
        public string BransAdi { get; set; }
        [Display(Name = "Branş")]
        public BranslarVM Branslar { get; set; }

        public Guid UlkeTercihId { get; set; }
        public string UlkeAdi { get; set; }
        [Display(Name = "Ülke Tercih")]
        public UlkeTercihVM UlkeTercihler { get; set; }

        public Guid? MulakatId { get; set; }
        [Display(Name = "Mülakat")]
        public MulakatlarVM Mulakatlar { get; set; }

       
        public string KaydedenId { get; set; }
        [Display(Name = "Kaydeden")]
        public string KaydedenAdi { get; set; }
        public KullaniciVM Kullanici { get; set; }
    }
}
