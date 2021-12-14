using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.ConstantsModels
{
    public enum EnumsSoruOnay
    {
        [Display(Name ="Onaya Gönderildi")]
        Onaya_Gonderildi=1,

        [Display(Name = "Onaylandı")]
        Onaylandi =2,

        [Display(Name = "Reddedildi")]
        Reddedildi =3
    }
}
