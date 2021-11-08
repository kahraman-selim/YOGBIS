using System.Collections.Generic;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.VModels;
using YOGBIS.Data.DbModels;

namespace YOGBIS.BusinessEngine.Contracts
{
    public interface IKullaniciBE
    {
        Result<List<KullaniciVM>> KullaniciGetir();
        /// <summary>
        /// Seçilen Kullanıcı bilgilerini getiren method
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Result<KullaniciVM> KullaniciGetir(int id);
        Result<KullaniciVM> KullaniciGuncelle(KullaniciVM model);
        Result<List<KullaniciVM>> OnayKullaniciGetir();
    }
}
