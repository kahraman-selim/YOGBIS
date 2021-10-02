using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YOGBIS.Common.ConstantsModels
{
    public enum Enums
    {
        [Display(Name ="Onaya Gönderildi")]
        Onaya_Gonderildi=1,
        [Display(Name = "Onaylandı")]
        Onaylandi =2,
        [Display(Name = "Reddedildi")]
        Reddedildi =3
    }
}
