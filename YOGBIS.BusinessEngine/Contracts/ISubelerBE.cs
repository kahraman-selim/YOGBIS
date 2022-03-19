using System;
using System.Collections.Generic;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;

namespace YOGBIS.BusinessEngine.Contracts
{
    public interface ISubelerBE
    {
        Result<List<SubelerVM>> SubeleriGetir();
        Result<SubelerVM> SubeEkle(SubelerVM model, SessionContext user);
        Result<SubelerVM> SubeGetir(Guid id);
        Result<List<SubelerVM>> SubeleriGetirOkulId(Guid OkulId);
        Result<SubelerVM> SubeGuncelle(SubelerVM model, SessionContext user);
        Result<bool> SubeSil(Guid id);
        Result<List<SubelerVM>> SubeGetirKullaniciId(string userId);
        Result<string> SubeAdGetir(Guid id);
    }
}
