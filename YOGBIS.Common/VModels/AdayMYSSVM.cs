using System;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class AdayMYSSVM : BaseVM
    {
        public Guid Id { get; set; }

        public string TC { get; set; }
        public string AdayAdiSoyadi { get; set; }
        public Guid? AdayId { get; set; }
        [Display(Name = "Adaylar")]
        public AdaylarVM Adaylar { get; set; }

        public string MYSSTarih { get; set; }
        public string MYSSSaat { get; set; }
        public string MYSSMulakatYer { get; set; }        
        public string MYSSDurum { get; set; }
        public string MYSSDurumAck { get; set; }

        public Guid? KomisyonId { get; set; }
        public int? MYSSKomisyonSiraNo { get; set; }
        public string MYSSKomisyonAdi { get; set; }       
        public int? KomisyonSN { get; set; }
        public int? KomisyonGunSN { get; set; }
        [Display(Name = "Komisyonlar")]
        public KomisyonlarVM Komisyonlar { get; set; }

        public bool? CagriDurum { get; set; }
        public bool? KabulDurum { get; set; }
        public bool? SinavDurum { get; set; }
        public bool? SinavaGelmedi { get; set; }
        public string SinavaGelmediAck { get; set; }
        public bool? SinavIptal { get; set; }
        public string SinavIptalAck { get; set; }

        public string MYSPuan { get; set; }
        public string MYSSonuc { get; set; }
        public string MYSSonucAck { get; set; }
        public int? MYSSSorulanSoruNo { get; set; }

        public Guid? BransId { get; set; }
        public string BransAdi { get; set; }
        public BranslarVM Branslar { get; set; }

        public Guid? DereceId { get; set; }
        public string DereceAdi { get; set; }
        public SoruDerecelerVM SoruDereceler { get; set; }

        public Guid? UlkeTercihId { get; set; }
        public string UlkeTercihAdi { get; set; }
        public UlkeTercihVM UlkeTercihleri { get; set; }



        public Guid? MulakatId { get; set; }
        public int MulakatYil { get; set; }
        [Display(Name = "Mülakatlar")]
        public MulakatlarVM Mulakatlar { get; set; }


        public string KaydedenId { get; set; }        
        public string KaydedenAdi { get; set; }
        [Display(Name = "Kaydeden")]
        public KullaniciVM Kullanici { get; set; }
    }
}
