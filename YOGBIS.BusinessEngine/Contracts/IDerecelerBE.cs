using System;
using System.Collections.Generic;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;

namespace YOGBIS.BusinessEngine.Contracts
{
    public interface IDerecelerBE
    {
        Result<List<SoruDerecelerVM>> DereceleriGetir();        
        Result<SoruDerecelerVM> DereceGetir(Guid id);
        Result<string> DereceAdGetir(Guid id);
        Result<SoruDerecelerVM> DereceEkle(SoruDerecelerVM model, SessionContext user);
        Result<SoruDerecelerVM> DereceGuncelle(SoruDerecelerVM model, SessionContext user);
        Result<bool> DereceSil(Guid id);
        Result<List<SoruDerecelerVM>> DereceGetirKullaniciId(string userId);
    }
}
