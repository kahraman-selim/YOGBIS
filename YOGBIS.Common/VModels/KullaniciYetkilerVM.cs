using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class KullaniciYetkilerVM : BaseVM
    {
        public string Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> EnumsKullaniciRolleri { get; set; }
        public string KaydedenAdi { get; set; }
    }
}
