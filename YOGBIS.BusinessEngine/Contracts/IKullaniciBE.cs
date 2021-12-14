using System.Collections.Generic;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.VModels;

namespace YOGBIS.BusinessEngine.Contracts
{
    public interface IKullaniciBE
    {
        Result<List<KullaniciVM>> KullaniciGetir();
        Result<KullaniciVM> KullaniciGetir(int Id);
        Result<KullaniciVM> KullaniciGuncelle(KullaniciVM model);
        Result<List<KullaniciVM>> OnayKullaniciGetir();
    }
}
