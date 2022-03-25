using System;
using System.Collections.Generic;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;

namespace YOGBIS.BusinessEngine.Contracts
{
    public interface IUlkelerBE
    {
        Result<List<UlkelerVM>> UlkeleriGetir();
        Result<List<UlkelerVM>> UlkeleriGetirUlkeId(Guid ulkeId);
        Result<List<UlkelerVM>> UlkeleriGetirViewComponent();
        Result<List<UlkelerVM>> UlkeGetirKullaniciId(string userId);
        Result<UlkelerVM> UlkeEkle(UlkelerVM model, SessionContext user);        
        Result<UlkelerVM> UlkeGetir(Guid id);
        Result<UlkelerVM> UlkeGetirUlkeKodu(string ulkeKodu);
        Result<UlkelerVM> UlkeGuncelle(UlkelerVM model, SessionContext user);        
        Result<bool> UlkeSil(Guid id);
        Result<bool> UlkeFotoSil(Guid id);
        Result<string> UlkeBayrakURLGetir(Guid id);
        Result<string> UlkeAdGetir(Guid id);
        Result<Guid> UlkeIdGetir(Guid id);
        Result<Guid> UlkeIdGetir(string ulkeKodu);
    }
}
