using System.Collections.Generic;

namespace YOGBIS.Common.VModels
{
    public class KullaniciYetkilerVM
    {
        public string Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> EnumsKullaniciRolleri { get; set; }
    }
}
