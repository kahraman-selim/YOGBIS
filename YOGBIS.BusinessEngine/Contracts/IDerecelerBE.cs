using System.Collections.Generic;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;
using YOGBIS.Data.DbModels;

namespace YOGBIS.BusinessEngine.Contracts
{
    public interface IDerecelerBE
    {
        Result<List<DerecelerVM>> DereceleriGetir();
        Result<DerecelerVM> DereceEkle(DerecelerVM model, SessionContext user);

        Result<DerecelerVM> DereceGetir(int id);

        Result<DerecelerVM> DereceGuncelle(DerecelerVM model, SessionContext user);
        
        Result<bool> DereceSil(int id);

        Result<List<DerecelerVM>> DereceGetirKullaniciId(string userId);
    }
}
