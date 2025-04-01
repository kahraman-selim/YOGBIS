using System.Collections.Generic;
using System.Threading.Tasks;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.VModels;

namespace YOGBIS.BusinessEngine.Contracts
{
    public interface IKullaniciBE
    {
        Result<List<KullaniciVM>> KullaniciGetir();
        Result<string> KullaniciAdSoyadGetir(string UserId);
        Result<KullaniciVM> KullaniciGuncelle(KullaniciVM model);
        Task<Result<List<KullaniciVM>>> OnayKullaniciGetir();
        Task<Result<List<KullaniciVM>>> OkulMuduruGetir();
        Task<Result<List<KullaniciVM>>> KomisyonGetir();
        Result<string> KomisyonKullaniciIDGetir(string KomisyonUserName);
        Task<Result<List<KullaniciVM>>> KomisyonSorumluGetir();
        Task<Result<List<KullaniciVM>>> KomisyonYardimciGetir();
        Result<List<KullaniciVM>> KomisyonBaskanlariniGetir();
    }
}
