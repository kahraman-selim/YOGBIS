using System.Collections.Generic;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;
using YOGBIS.Data.DbModels;

namespace YOGBIS.BusinessEngine.Contracts
{
    public interface IUlkelerBE
    {
        Result<List<UlkelerVM>> UlkeleriGetir();
        Result<List<UlkelerVM>> UlkeGetirKullaniciId(string userId);
        Result<UlkelerVM> UlkeEkle(UlkelerVM model, SessionContext user, string uniqueFileName);        
        Result<UlkelerVM> UlkeGetir(int id);
        Result<UlkelerVM> UlkeGuncelle(UlkelerVM model, SessionContext user, string uniqueFileName);        
        Result<bool> UlkeSil(int id);        
    }
}
