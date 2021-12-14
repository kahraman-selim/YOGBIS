using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.ConstantsModels
{
    public enum EnumAylar
    {
        [Display(Name = "Ocak")]
        Ocak = 1,
        [Display(Name = "Şubat")]
        Subat = 2,
        [Display(Name = "Mart")]
        Mart = 3,
        [Display(Name = "Nisan")]
        Nisan = 4,
        [Display(Name = "Mayıs")]
        Mayis = 5,
        [Display(Name = "Haziran")]
        Haziran = 6,
        [Display(Name = "Temmuz")]
        Temmuz = 7,
        [Display(Name = "Ağustos")]
        Agustos = 8,
        [Display(Name = "Eylül")]
        Eylul = 9,
        [Display(Name = "Ekim")]
        Ekim = 10,
        [Display(Name = "Kasım")]
        Kasim = 11,
        [Display(Name = "Aralık")]
        Aralik = 12,
    }
}
