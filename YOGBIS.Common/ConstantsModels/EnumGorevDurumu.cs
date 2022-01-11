using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.ConstantsModels
{
    public enum EnumGorevDurumu
    {
        [Display(Name = "Sırada Bekliyor")]
        Bekliyor =1,
        [Display(Name = "Görevlendirme İşlemleri Devam Ediyor")]
        Gorevlendirildi =2,
        [Display(Name = "Görevde")]
        Gorevde =3,
        [Display(Name = "Geri Çekildi")]
        GeriCekildi =4,
        [Display(Name = "Görevden Vazgeçti")]
        Vazgecti =5,
        [Display(Name = "Kendi İsteğiyle Geri Döndü")]
        KendiIstegi =6
    }
}
