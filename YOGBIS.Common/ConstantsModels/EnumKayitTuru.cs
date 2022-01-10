using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.ConstantsModels
{
    public enum EnumKayitTuru
    {
        [Display(Name = "İlk Kayıt")]
        Kayit =1,
        [Display(Name = "Güncelleme")]
        Guncelleme =2,
        [Display(Name = "Kayıt Silme")]
        Silme =3
        
    }
}
