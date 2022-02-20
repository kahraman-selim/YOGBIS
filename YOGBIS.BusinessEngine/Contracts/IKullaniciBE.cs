using System.Collections.Generic;
using System.Threading.Tasks;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.VModels;

namespace YOGBIS.BusinessEngine.Contracts
{
    public interface IKullaniciBE
    {
        Result<List<KullaniciVM>> KullaniciGetir();
        //Result<KullaniciVM> KullaniciGetir(string Id);
        Result<KullaniciVM> KullaniciGuncelle(KullaniciVM model);
        Task<Result<List<KullaniciVM>>> OnayKullaniciGetir();
        Task<Result<List<KullaniciVM>>> OkulMuduruGetir();
    }
}
