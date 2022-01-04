using System.Collections.Generic;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;

namespace YOGBIS.BusinessEngine.Contracts
{
    public interface IUlkeGruplariBE
    {
        Result<List<UlkeGruplariVM>> UlkeGruplariGetir();
        Result<UlkeGruplariVM> UlkeGrupGetir(int id);
        Result<UlkeGruplariVM> UlkeGrupEkle(UlkeGruplariVM model, SessionContext user);
        Result<UlkeGruplariVM> UlkeGrupGuncelle(UlkeGruplariVM model, SessionContext user);        
        Result<bool> UlkeGrupSil(int id);
        Result<List<UlkeGruplariVM>> UlkeGrupGetirKullaniciId(string userId);
    }
}
