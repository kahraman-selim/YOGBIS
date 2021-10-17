using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YOGBIS.Common.ConstantsModels
{
    public enum EnumsKullaniciRolleri
    {
        [Display(Name = "Sistem Yöneticisi")]
        Administrator,
        [Display(Name = "Yönetici")]
        Manager,
        [Display(Name = "Alt Yöneticisi")]
        SubManager,
        [Display(Name = "Personel")]
        Follower,
        [Display(Name = "Okutman")]
        Lecturer,
        [Display(Name = "Öğretmen")]
        Teacher
    }
}
