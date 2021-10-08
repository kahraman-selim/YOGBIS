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
        [Display(Name = "Yurt Dışı Yöneticisi")]
        SubManager,
        [Display(Name = "Sistem Kullanıcısı")]
        Follower,
        [Display(Name = "Okutman")]
        Lecturer,
        [Display(Name = "Öğretmen")]
        Teacher
    }
}
