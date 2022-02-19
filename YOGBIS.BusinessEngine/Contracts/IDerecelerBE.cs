using YOGBIS.Common.ResultModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;

namespace YOGBIS.BusinessEngine.Contracts
{
    public interface ISSSBE
    {
        //Result<List<SoruDerecelerVM>> DereceleriGetir();
        Result<SSSVM> SSSEkle(SSSVM model, SessionContext user);
        //Result<SoruDerecelerVM> DereceGetir(int id);
        //Result<string> DereceAdGetir(int id);
        //Result<SoruDerecelerVM> DereceGuncelle(SoruDerecelerVM model, SessionContext user);        
        //Result<bool> DereceSil(int id);
        //Result<List<SoruDerecelerVM>> DereceGetirKullaniciId(string userId);
    }
}
