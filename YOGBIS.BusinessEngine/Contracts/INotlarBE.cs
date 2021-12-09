using System.Collections.Generic;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;
using YOGBIS.Data.DbModels;

namespace YOGBIS.BusinessEngine.Contracts
{
    public interface INotlarBE
    {
        Result<List<NotlarVM>> NotlariGetir();
        Result<NotlarVM> NotEkle(NotlarVM model, SessionContext user);
        Result<NotlarVM> NotGetir(int id);
        Result<NotlarVM> NotGuncelle(NotlarVM model, SessionContext user);        
        Result<bool> NotSil(int id);
        Result<List<NotlarVM>> NotGetirKullaniciId(string userId);
    }
}
