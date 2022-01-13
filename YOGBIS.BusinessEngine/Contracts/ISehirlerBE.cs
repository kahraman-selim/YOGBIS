using System.Collections.Generic;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;

namespace YOGBIS.BusinessEngine.Contracts
{
    public interface ISehirlerBE
    {
        Result<List<SehirlerVM>> SehirleriGetir();
        Result<SehirlerVM> SehirEkle(SehirlerVM model, SessionContext user);
        Result<SehirlerVM> SehirGetir(int id);
        Result<SehirlerVM> SehirGuncelle(SehirlerVM model, SessionContext user);        
        Result<bool> SehirSil(int id);
        Result<List<SehirlerVM>> SehirGetirKullaniciId(string userId);
    }
}
