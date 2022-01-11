using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.ConstantsModels
{
    public enum EnumsKullaniciRolleri
    {
        [Display(Name = "Sistem Yöneticisi")]
        Administrator,
        [Display(Name = "Yönetici")]
        Manager,
        [Display(Name = "Temsilci")]
        Representative,
        [Display(Name = "Okul Yöneticisi")]
        SubManager,
        [Display(Name = "Personel")]
        Follower,
        [Display(Name = "Okutman")]
        Lecturer,
        [Display(Name = "Öğretmen")]
        Teacher,
        [Display(Name = "Komisyon Üyesi")]
        Commissioner,
        [Display(Name = "Komisyon Başkanı")]
        CommissionerHead
    }
}
