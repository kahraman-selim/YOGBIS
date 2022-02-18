using System.Collections.Generic;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;

namespace YOGBIS.BusinessEngine.Contracts
{
    public interface IUlkelerBE
    {
        Result<List<UlkelerVM>> UlkeleriGetir();
        Result<List<UlkelerVM>> UlkeGetirKullaniciId(string userId);
        Result<UlkelerVM> UlkeEkle(UlkelerVM model, SessionContext user);        
        Result<UlkelerVM> UlkeGetir(int id);
        Result<UlkelerVM> UlkeGetirUlkeKodu(string ulkeKodu);
        Result<UlkelerVM> UlkeGuncelle(UlkelerVM model, SessionContext user);        
        Result<bool> UlkeSil(int id);
        Result<bool> UlkeFotoSil(int id);
        Result<string> UlkeBayrakURLGetir(int id);
        Result<string> UlkeAdGetir(int id);
        Result<int> UlkeIdGetir(string ulkeKodu);
    }
}
